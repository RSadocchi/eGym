using eGym.Core.Security;
using eGym.MVC.Models;
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
        public async Task<IActionResult> SignIn(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _identity.GetUserAsync(username: model.Username, getByEmail: true, getByUsername: true, getByPhoneNumber: false);
            if (user == null) throw new Exception("User not found");

            if (!await _identity.CheckPasswordAsync(user, model.Password))
                throw new Exception("Invalid credetials");

            


            return Ok(new { success = true, dashboard = "", disabled = true });

        }

        [HttpGet("/sign-up")]
        public IActionResult SignUp() => View();

        [HttpPost("/sign-up")]
        public async Task<IActionResult> SignUp(LoginModel model)
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
    }
}
