using AutoMapper;
using eGym.Core.Domain;
using eGym.Core.SeedWork.NSpecifications;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace eGym.Application.Services
{
    public interface ITodoService
    {
        Task<IQueryable<Todo_Master>> ListAsync(int[] statuses = null, string searchString = null);
        Task<Todo_Master> FindAsync(int todoId);
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

        public async Task<IQueryable<Todo_Master>> ListAsync(int[] statuses = null, string searchString = null)
        {
            var spec = Spec.Any<Todo_Master>();
            if (statuses?.Length > 0) spec &= TodoSpecifications.ByStatusIDs(statuses);
            if (!string.IsNullOrWhiteSpace(searchString)) spec &= TodoSpecifications.BySearchString(searchString);
            var query = _repository.GetBySpecification(spec);
            return await Task.FromResult(query);
        }
    }
}
