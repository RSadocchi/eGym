using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eGym.Core.Security.Identity
{
    public class UserLogin : Microsoft.AspNetCore.Identity.IdentityUserLogin<int>
    {
        [Key]
        public int Id { get; set; }
    }
}