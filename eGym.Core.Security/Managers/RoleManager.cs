using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace eGym.Core.Security.Identity
{
    public class RoleManager : Microsoft.AspNetCore.Identity.RoleManager<eGym.Core.Security.Identity.Role>
    {
        public RoleManager(
            IRoleStore<Role> store,
            IEnumerable<IRoleValidator<Role>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<RoleManager<Role>> logger)
            : base(store, roleValidators, keyNormalizer, errors, logger)
        { }
    }
}