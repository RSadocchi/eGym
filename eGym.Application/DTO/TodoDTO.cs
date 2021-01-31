﻿using AutoMapper;
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
        public TodoPriorityEnum TD_Priority { get; set; }
        public bool TD_Important { get; set; }
        public TodoDeadlineEnum TD_Deadline { get; set; }
        public DateTime? TD_DeadlineDate { get; set; }
        public TimeSpan? TD_DeadlineTime { get; set; }
        public string TD_Title { get; set; }
        public string TD_Content { get; set; }
        public string TD_Note { get; set; }
        public short TD_StatusID { get; set; }
        public DateTime? TD_StatusDate { get; set; }

        public string Priority { get; set; }


        public class ProfileConfig : Profile
        {
            public ProfileConfig() : base(nameof(ProfileConfig))
            {
                AllowNullCollections = true;
                AllowNullDestinationValues = true;

                CreateMap<Todo_Master, TodoDTO>()
                    .ForMember(m => m.Priority, o => o.MapFrom(s => s.TD_Priority.ToCssColor()));

                CreateMap<TodoDTO, Todo_Master>()
                    .EqualityComparison((s, d) => s.TD_ID != 0 && s.TD_ID == d.TD_ID)
                    .ForMember(m => m.TD_ID, o => o.Ignore());
            }
        }
    }
}
