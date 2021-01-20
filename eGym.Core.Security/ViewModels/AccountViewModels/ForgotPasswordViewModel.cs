using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eGym.Core.Security.Identity.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Il campo Email è obbligatorio")]
        [EmailAddress]
        public string Email { get; set; }
        //[Required(ErrorMessage = "Il campo Nome è obbligatorio")]
        //[Display(Name = "Nome *")]
        //public string FirstName { get; set; }
        //[Required(ErrorMessage = "Il campo Cognome è obbligatorio")]
        //[Display(Name = "Cognome *")]
        //public string LastName { get; set; }
    }
}
