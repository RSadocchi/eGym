using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eGym.Core.Security.Identity
{
    public class UserClaim : Microsoft.AspNetCore.Identity.IdentityUserClaim<int>
    { }
}