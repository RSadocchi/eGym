using eGym.Core.Domain;

namespace eGym.Application
{
    public static class TodoExtensions
    {
        public static string ToCssColor(this TodoPriorityEnum priority)
            => priority switch
            {
                TodoPriorityEnum.Hight => "danger",
                TodoPriorityEnum.Medium => "warning",
                _ => "primary",
            };
    }
}
