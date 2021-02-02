using eGym.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGym.MVC.Areas.Admin.Models
{
    public class TodoFilter
    {
        public int[] Statuses { get; set; } = EN_TodoStatus.GetAll().Select(t => (int)t.ID).ToArray();
        public string SearchString { get; set; } = null;
        public bool Important { get; set; } = false;
    }
}
