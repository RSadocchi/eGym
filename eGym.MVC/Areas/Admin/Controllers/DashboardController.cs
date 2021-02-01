using eGym.Core.Security;
using eGym.MVC.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGym.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    [Authorize(Policy = Const_ClaimTypes.ADMINISTRATOR)]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("todo")]
        public async Task<IActionResult> Todo() => View();

        [HttpPost("todo-list")]
        public async Task<IActionResult> TodoList(TodoFilter filter)
        {

        }
    }
}
