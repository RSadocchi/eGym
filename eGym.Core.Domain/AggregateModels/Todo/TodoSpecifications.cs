using eGym.Core.SeedWork.NSpecifications;
using System.Linq;

namespace eGym.Core.Domain
{
    public static class TodoSpecifications
    {
        public static ASpec<Todo_Master> ByID(int id) => new Spec<Todo_Master>(t => t.TD_ID == id);
        public static ASpec<Todo_Master> ByIDs(params int[] ids) => new Spec<Todo_Master>(t => (ids == null || ids.Count() <= 0) || ids.Contains(t.TD_ID));

        public static ASpec<Todo_Master> ByPriority(TodoPriorityEnum priority) => new Spec<Todo_Master>(t => t.TD_Priority == priority);
        public static ASpec<Todo_Master> ByPriorities(params TodoPriorityEnum[] priorities) => new Spec<Todo_Master>(t => (priorities == null || priorities.Count() <= 0) || priorities.Contains(t.TD_Priority));

        public static ASpec<Todo_Master> ByStatusID(int id) => new Spec<Todo_Master>(t => t.TD_StatusID == id);
        public static ASpec<Todo_Master> ByStatusIDs(params short[] ids) => new Spec<Todo_Master>(t => (ids == null || ids.Count() <= 0) || ids.Contains(t.TD_StatusID));

        public static ASpec<Todo_Master> BySearchString(string search)
            => new Spec<Todo_Master>(t => string.IsNullOrWhiteSpace(search) || t.TD_Title.Contains(search.Trim()) /*|| t.TD_Content.Contains(search.Trim())*/);
    }
}
