using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGym.MVC.Areas.Admin.Models
{
    public class TodoFilter
    {
        public int[] Statuses { get; set; }
        public string SearchString { get; set; }
    }
}
