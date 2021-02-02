using AutoMapper;
using AutoMapper.EquivalencyExpression;
using eGym.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGym.Application.DTO
{

    public class TodoDTO
    {
        public int TD_ID { get; set; }
        public DateTime TD_CreationDate { get; set; }
        public int TD_Priority { get; set; }
        public bool TD_Important { get; set; }
        public int TD_Deadline { get; set; }
        public DateTime? TD_DeadlineDate { get; set; }
        public TimeSpan? TD_DeadlineTime { get; set; }
        public string TD_Title { get; set; }
        public string TD_Content { get; set; }
        public string TD_Note { get; set; }
        public short TD_StatusID { get; set; }
        public DateTime? TD_StatusDate { get; set; }

        public string PriorityCss { get; set; }
        public string StatusCss { get; set; }

        public class ProfileConfig : Profile
        {
            public ProfileConfig() : base(nameof(ProfileConfig))
            {
                AllowNullCollections = true;
                AllowNullDestinationValues = true;

                CreateMap<Todo_Master, TodoDTO>()
                    //.ForMember(m => m.TD_Priority, o => o.MapFrom(s => (int)s.TD_Priority))
                    //.ForMember(m => m.TD_Deadline, o => o.MapFrom(s => (int)s.TD_Deadline))
                    .ForMember(m => m.PriorityCss, o => o.MapFrom(s => s.TD_Priority.ToCssPriority()))
                    .ForMember(m => m.StatusCss, o => o.MapFrom(s => EN_TodoStatus.FromID(s.TD_StatusID).ToCssStatus()));

                CreateMap<TodoDTO, Todo_Master>()
                    .EqualityComparison((s, d) => s.TD_ID != 0 && s.TD_ID == d.TD_ID)
                    .ForMember(m => m.TD_ID, o => o.Ignore());
                    //.ForMember(m => m.TD_Priority, o => o.MapFrom(s => (TodoPriorityEnum)s.TD_Priority))
                    //.ForMember(m => m.TD_Deadline, o => o.MapFrom(s => (TodoDeadlineEnum)s.TD_Deadline));
            }
        }
    }
}
