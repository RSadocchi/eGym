using AutoMapper;
using eGym.Application.DTO;
using eGym.Application.Services;
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
        readonly IMapper _mapper;
        readonly ITodoService _todoService;

        public DashboardController(
            IMapper mapper,
            ITodoService todoService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _todoService = todoService ?? throw new ArgumentNullException(nameof(todoService));
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("todo")]
        public async Task<IActionResult> Todo() => View();

        [HttpPost("todo-list")]
        public async Task<IActionResult> TodoList(TodoFilter filter)
        {
            var entities = await _todoService.ListAsync(statuses: filter?.Statuses, searchString: filter?.SearchString);
            var dtos = _mapper.Map(entities, new List<TodoDTO>());
            return PartialView("TodoList", dtos);
        }

        [HttpPost("todo-save")]
        public async Task<IActionResult> TodoSave(TodoDTO dto)
        {
            return Ok();
        }
    }
}
