using eGym.Application.Model;
using eGym.Core.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace eGym.MVC.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        readonly ILogger<AuthController> _logger;
        readonly IIdentityService _identity;

        public AuthController(
            ILogger<AuthController> logger,
            IIdentityService identity)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        [HttpGet("/sign-in")]
        public IActionResult SignIn() => View();

        [HttpPost("/sign-in")]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _identity.GetUserAsync(username: model.Username, getByEmail: true, getByUsername: true, getByPhoneNumber: false);
            if (user == null) throw new Exception("User not found");

            if (!await _identity.CheckPasswordAsync(user, model.Password))
                throw new Exception("Invalid credetials");

            var loggedIn = false;
            if (!user.Disabled && user.PasswordExpirationDateTime.GetValueOrDefault(DateTime.MaxValue) > DateTime.Now)
            {
                await _identity.LoginAsync(user, true);
            }

            return Ok(new { loggedIn, redirect = "", disabled = user.Disabled, passwordExpired = user.PasswordExpirationDateTime.GetValueOrDefault(DateTime.MaxValue) <= DateTime.Now });

        }

        [HttpGet("/sign-up")]
        public IActionResult SignUp() => View();

        [HttpPost("/sign-up")]
        public async Task<IActionResult> SignUp(SignInModel model)
        {
            return BadRequest();
        }

        [Authorize]
        [HttpGet("/sign-out")]
        public IActionResult SignOut()
        {
            _identity.UserLogoutAsync().Wait();
            return Redirect("/");
        }

        [HttpGet("/access-denied")]
        public IActionResult AccessDenied() => View();
    }
}
