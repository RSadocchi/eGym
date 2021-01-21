using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eGym.Core.Security.Identity
{
    public class UserManager : Microsoft.AspNetCore.Identity.UserManager<eGym.Core.Security.Identity.User>
    {
        private const int PASSWORD_HISTORY_LIMIT = 5;
        private readonly RoleManager _roleManager;
        private readonly IHttpContextAccessor _context;

        public UserManager(
            RoleManager roleManager,
            IUserStore<User> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<User>> logger,
            IHttpContextAccessor context)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ClaimsPrincipal User => _context.HttpContext?.User;
        public int UserID
        {
            get
            {
                var claims = User?.FindAll(ClaimTypes.NameIdentifier);
                foreach (var c in claims)
                    if (int.TryParse(c?.Value, out int ID)) return ID;
                return -1;
            }
        }

        public async Task<bool> SetCultureAsync(User user, string culture)
        {
            user.Culture = culture;
            var result = await UpdateAsync(user);
            return (result.Succeeded);
        }

        public async Task<User> FindByPhoneNumberAsync(string phoneNumber)
            => await Task.FromResult(Users.FirstOrDefault(t => t.PhoneNumber == phoneNumber));

        public async Task<bool> IsPasswordExpiredAsync(User user)
            => await Task.FromResult(user.PasswordExpirationDateTime <= DateTime.Now);

        public async Task<bool> CheckPINAsync(User user, string pin)
            => await Task.FromResult(PasswordHasher.VerifyHashedPassword(user, user.PINHash, pin) == PasswordVerificationResult.Success);

        public async Task<IList<Claim>> GetClaimsByRoleId(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role != null) return await _roleManager.GetClaimsAsync(role);
            return null;
        }

        public async Task<IList<Claim>> GetClaimsByRoleName(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null) return await _roleManager.GetClaimsAsync(role);
            return null;
        }

        public async Task<IList<Claim>> GetUserClaimsAsync(User user, string roleName = null, bool getUserClaims = true, bool getRoleClaims = true)
        {
            var result = new List<Claim>();
            // Aggiungo i CLAIM dell'utente
            if (getUserClaims)
            {
                var userClaims = await base.GetClaimsAsync(user);
                foreach (var item in userClaims)
                    if (result.Find(t => t.Type == item.Type) == null)
                        result.Add(item);
            }

            // Aggiungo i CLAIM dei ruoli dell'utente
            if (getRoleClaims && SupportsUserRole)
            {
                var roles = await base.GetRolesAsync(user);
                foreach (var rName in roles)
                {
                    if (roleName == null || roleName != null && roleName == rName)
                    {
                        result.Add(new Claim("UserClaimsRole", rName));
                        if (_roleManager.SupportsRoleClaims)
                        {
                            var roleClaims = await this.GetClaimsByRoleName(rName);
                            foreach (var roleClaim in roleClaims ?? new List<Claim>())
                                if (result.Find(t => t.Type == roleClaim.Type) == null)
                                    result.Add(roleClaim);
                        }
                    }
                }
            }
            return result;
        }
    }
}