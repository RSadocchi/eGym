using eGym.Core.Domain;

namespace eGym.Application
{
    public static class TodoExtensions
    {
        public static string ToCssPriority(this TodoPriorityEnum priority)
            => priority switch
            {
                TodoPriorityEnum.Hight => " danger ",
                TodoPriorityEnum.Medium => " warning ",
                _ => " primary ",
            };

        public static string ToCssStatus(this EN_TodoStatus status)
            => status.Code switch
            {
                nameof(EN_TodoStatus.Completed) => " all-list todo-task-done",
                nameof(EN_TodoStatus.Deleted) => " todo-task-trash ",
                nameof(EN_TodoStatus.Expired) => " all-list ",
                nameof(EN_TodoStatus.Suspensed) => " all-list ",
                _ => " all-list ",
            };
    }
}
