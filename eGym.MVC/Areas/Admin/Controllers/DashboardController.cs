using AutoMapper;
using eGym.Application.DTO;
using eGym.Application.Services;
using eGym.Core.Domain;
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

        [HttpGet("todo-badge")]
        public async Task<IActionResult> TodoBadgeCalculator()
        {
            var entities = (await _todoService.ListAsync(statuses: EN_TodoStatus.GetAll().Select(t => t.ID).ToArray(), searchString: null)).ToList();
            return Ok(new
            {
                todo = entities.Where(t => EN_TodoStatus.GetAll().Where(t => t.ID != EN_TodoStatus.Completed.ID && t.ID != EN_TodoStatus.Deleted.ID).Select(t => t.ID).Contains(t.TD_StatusID)).Count(),
                important = entities.Where(t => t.TD_Important == true && EN_TodoStatus.GetAll().Where(t => t.ID != EN_TodoStatus.Completed.ID && t.ID != EN_TodoStatus.Deleted.ID).Select(t => t.ID).Contains(t.TD_StatusID)).Count(),
                done = entities.Where(t => t.TD_StatusID == EN_TodoStatus.Completed.ID).Count(),
                trash = entities.Where(t => t.TD_StatusID == EN_TodoStatus.Deleted.ID).Count()
            });
        }

        [HttpGet("todo-add")]
        [HttpGet("todo-edit/{id}")]
        public async Task<IActionResult> TodoEdit(int? id)
        {
            TodoDTO dto = new TodoDTO();
            if (id.HasValue && id.Value > 0)
            {
                var todo = await _todoService.FindAsync(id.Value);
                _mapper.Map(todo, dto);
            }
            return PartialView("TodoEditForm", dto);
        }

        [HttpPost("todo-save")]
        public async Task<IActionResult> TodoSave(TodoDTO dto)
        {
            await _todoService.SaveAsync(dto: dto);
            return Ok(new { success = true });
        }

        [HttpGet("todo-toggle/{todoId}/important/{isImportant}")]
        public async Task<IActionResult> TodoToggleImportant(int todoId, bool isImportant)
        {
            var success = await _todoService.ToggleImportantAndPriorityAsync(todoId: todoId, isImportant: isImportant, priorityId: null, isDone: null);
            return Ok(new { success });
        }

        [HttpGet("todo-toggle/{todoId}/priority/{priorityId}")]
        public async Task<IActionResult> TodoTogglePriority(int todoId, int priorityId)
        {
            var success = await _todoService.ToggleImportantAndPriorityAsync(todoId: todoId, isImportant: null, priorityId: priorityId, isDone: null);
            return Ok(new { success });
        }

        [HttpGet("todo-toggle/{todoId}/done/{isDone}")]
        public async Task<IActionResult> TodoToggleDone(int todoId, bool isDone)
        {
            var success = await _todoService.ToggleImportantAndPriorityAsync(todoId: todoId, isImportant: null, priorityId: null, isDone: isDone);
            return Ok(new { success });
        }

        [HttpGet("todo-change/{todoId}/status/{statusId}")]
        public async Task<IActionResult> TodoChangeStatus(int todoId, short statusId)
        {
            var success = await _todoService.ChangeStatusAsync(todoId: todoId, statusId: statusId);
            return Ok(new { success });
        }
    }
}
