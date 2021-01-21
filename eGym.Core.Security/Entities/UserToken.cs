using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eGym.Core.Security.Identity
{
    public class UserToken : Microsoft.AspNetCore.Identity.IdentityUserToken<int>
    {
        [Key]
        public int Id { get; set; }
    }
}