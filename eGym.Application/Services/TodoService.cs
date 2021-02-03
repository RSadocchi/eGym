using AutoMapper;
using eGym.Application.DTO;
using eGym.Core.Domain;
using eGym.Core.SeedWork.NSpecifications;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace eGym.Application.Services
{
    public interface ITodoService
    {
        Task<IQueryable<Todo_Master>> ListAsync(short[] statuses = null, string searchString = null);
        Task<Todo_Master> FindAsync(int todoId);
        Task<TodoDTO> SaveAsync(TodoDTO dto = null, Todo_Master entity = null);
        Task<bool> ToggleImportantAndPriorityAsync(int todoId, bool? isImportant = null, int? priorityId = null, bool? isDone = null);
    }

    public class TodoService : ITodoService
    {
        readonly ITodoRepository _repository;
        readonly IMapper _mapper;

        public TodoService(
            ITodoRepository todoRepository,
            IMapper mapper)
        {
            _repository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Todo_Master> FindAsync(int todoId)
            => await _repository.FindAsync(todoId);

        public async Task<IQueryable<Todo_Master>> ListAsync(short[] statuses = null, string searchString = null)
        {
            var spec = Spec.Any<Todo_Master>();
            if (statuses?.Length > 0) spec &= TodoSpecifications.ByStatusIDs(statuses);
            if (!string.IsNullOrWhiteSpace(searchString)) spec &= TodoSpecifications.BySearchString(searchString);
            var query = _repository.GetBySpecification(spec);
            return await Task.FromResult(query);
        }

        public async Task<TodoDTO> SaveAsync(TodoDTO dto = null, Todo_Master entity = null)
        {
            if (dto == null && entity == null) throw new Exception($"{nameof(dto)} and {nameof(entity)} they were both null.");
            if (dto != null && entity == null)
            {
                if (dto.TD_ID > 0) entity = await FindAsync(todoId: dto.TD_ID);
                entity ??= new Todo_Master();
                _mapper.Map(dto, entity);
            }

            if (dto?.TD_ID <= 0 || entity.TD_ID <= 0) entity = await _repository.AddAsync(entity);
            else entity = await _repository.UpdateAsync(entity);
            await _repository.UnitOfWork.SaveEntitiesAsync();
            
            dto ??= new TodoDTO();
            _mapper.Map(entity, dto);
            return dto;
        }

        public async Task<bool> ToggleImportantAndPriorityAsync(int todoId, bool? isImportant = null, int? priorityId = null, bool? isDone = null)
        {
            if (!isImportant.HasValue && !priorityId.HasValue && !isDone.HasValue) return false;

            var todo = await FindAsync(todoId: todoId);
            if (todo == null) return false;

            todo.TD_Important = isImportant ?? todo.TD_Important;
            todo.TD_Priority = priorityId.HasValue ? (TodoPriorityEnum)priorityId.Value : todo.TD_Priority;
            todo.TD_StatusID = isDone.HasValue ?
                (isDone.Value == true ? EN_TodoStatus.Completed.ID : EN_TodoStatus.Scheduled.ID) :
                todo.TD_StatusID;
            todo.TD_StatusDate = isDone.HasValue ? DateTime.Now : todo.TD_StatusDate;

            await _repository.UpdateAsync(todo);
            await _repository.UnitOfWork.SaveEntitiesAsync();

            return true;
        }
    }
}
