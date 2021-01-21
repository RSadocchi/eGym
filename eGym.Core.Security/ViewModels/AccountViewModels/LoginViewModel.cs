using System.ComponentModel.DataAnnotations;

namespace eGym.Core.Security.Identity.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Culture { get; set; }
        public bool RememberMe { get; set; }
    }

    public class LoginPINViewModel
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string PIN { get; set; }
        public string Culture { get; set; }
    }
}