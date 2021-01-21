using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<int?> GetRoleIdByRoleName(string roleName)
        {
            var id = this.Roles.FirstOrDefault(t => t.Name == roleName)?.Id;
            return await Task.FromResult(id);
        }

        public async Task<List<Role>> GetRolesByClaimTypes(string[] claimTypes)
        {
            var result = new List<Role>();
            foreach (var role in this.Roles)
            {
                var claims = await this.GetClaimsAsync(role);
                if (claims.Where(t => claimTypes.Contains(t.Type)).Select(t => t.Type).OrderBy(t => t).ToList().SequenceEqual(claimTypes.OrderBy(t => t)))
                    result.Add(role);
            }
            return result;
        }
    }
}