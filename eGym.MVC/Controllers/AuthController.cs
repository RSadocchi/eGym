using eGym.Application.Model;
using eGym.Core.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
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
            {
                return BadRequest(new
                {
                    Username = string.Join(" ", ModelState[nameof(SignInModel.Username)].Errors.Select(t => t.ErrorMessage)),
                    Password = string.Join(" ", ModelState[nameof(SignInModel.Password)].Errors.Select(t => t.ErrorMessage))
                });
            }

            var user = await _identity.GetUserAsync(username: model.Username, getByEmail: true, getByUsername: true, getByPhoneNumber: false);
            if (user == null) throw new Exception("Invalid credetials.");

            if (!await _identity.CheckPasswordAsync(user, model.Password))
                throw new Exception("Invalid credetials.");

            var loggedIn = false;
            string redirect = string.Empty;
            if (!user.Disabled) //&& user.PasswordExpirationDateTime.GetValueOrDefault(DateTime.MaxValue) > DateTime.Now)
            {
                await _identity.LoginAsync(user, true);
                loggedIn = true;

                if (await _identity.UserHasClaimAsync(user, new System.Security.Claims.Claim(Const_ClaimTypes.ADMINISTRATOR, Const_ClaimValues.DefaultValue)))
                    redirect = "/admin/dashboard";
                else
                    redirect = "/";
            }

            return Ok(new 
            { 
                loggedIn, 
                redirect,
                disabled = user.Disabled, 
                passwordExpired = user.PasswordExpirationDateTime.GetValueOrDefault(DateTime.MaxValue) <= DateTime.Now,
                message = ""
            });

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
