using eGym.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGym.MVC.Areas.Admin.Models
{
    public class TodoFilter
    {
        public short[] Statuses { get; set; } = EN_TodoStatus.GetAll().Select(t => t.ID).ToArray();
        public string SearchString { get; set; } = null;
        public bool Important { get; set; } = false;
    }
}
