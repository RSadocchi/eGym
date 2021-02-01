using eGym.Core.SeedWork;

namespace eGym.Core.Domain
{
    public interface ITodoRepository : IRepository<Todo_Master, int> { }

    public class TodoRepository : BaseRepository<ApplicationDbContext, Todo_Master, int>, ITodoRepository
    {
        public TodoRepository(ApplicationDbContext context) : base(context) { }
    }
}
