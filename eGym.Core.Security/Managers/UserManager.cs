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
        private readonly IHttpContextAccessor _context;
        private readonly RoleManager _roleManager;

        public ClaimsPrincipal User => _context.HttpContext?.User;
        public int UserId
        {
            get
            {
                var ID = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (ID != null) return int.Parse(ID);
                return -1;
            }
        }

        public UserManager(
            IUserStore<User> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<User>> logger,
            IHttpContextAccessor context,
            RoleManager roleManager)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        /// <summary>
        /// Restituisce un array di claims cercandoli nel ruolo e nell'utente, dando priorità a quest'ultimi
        /// </summary>
        public async Task<IList<Claim>> GetClaimsIncludeRoleClaimsAsync(User user)
        {
            List<Claim> result = new List<Claim>();
            if (SupportsUserRole)
            {
                var roles = await GetRolesAsync(user);
                foreach (var roleName in roles)
                {
                    result.Add(new Claim(ClaimTypes.Role, roleName));
                    if (_roleManager.SupportsRoleClaims)
                    {
                        var role = await _roleManager.FindByNameAsync(roleName);
                        if (role != null)
                        {
                            var roleClaims = await _roleManager.GetClaimsAsync(role);
                            foreach (var roleClaim in roleClaims)
                                result.Add(roleClaim);
                        }
                    }
                }
            }

            // sostituisco i claims collegati col ruolo con quelli collegati all'utente se ci sono
            var userClaims = await GetClaimsAsync(user);
            var userClaimTypes = userClaims.Select(t => t.Type);
            result = result.Where(t => !userClaimTypes.Contains(t.Type)).ToList();
            result.AddRange(userClaims);

            return result;
        }

        public async Task<User> UpdatePasswordExpirationAsync(User user, DateTime expDate)
        {
            user.PasswordExpirationDateTime = expDate;
            await UpdateAsync(user);
            return user;
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword, int expirationDays)
        {
            if (await IsPasswordHistory(user, newPassword))
            {
                var errorCode = new IdentityError() { Code = "000001", Description = "you can not use a password previously used" };
                return await Task.FromResult(IdentityResult.Failed(errorCode));
            }

            var result = await base.ChangePasswordAsync(user, currentPassword, newPassword);
            if (result.Succeeded)
            {
                await UpdatePasswordExpirationAsync(user, DateTime.Today.AddDays(expirationDays));
                result = await AddToPasswordHistoryAsync(user, currentPassword);
            }
            return result;
        }

        public async Task<IdentityResult> ChangePINAsync(User user, string PIN)
        {
            user.PINHash = PasswordHasher.HashPassword(user, PIN);
            var result = await base.UpdateAsync(user);
            return result;
        }

        private Task<bool> IsPasswordHistory(User user, string newPassword)
        {
            if (user.PasswordHistory
                .OrderByDescending(o => o.ChangedDateTime)
                .Select(s => s.PasswordHash)
                .Take(PASSWORD_HISTORY_LIMIT)
                .Where(w => PasswordHasher.VerifyHashedPassword(user, w, newPassword) != PasswordVerificationResult.Failed).Any() ||
                // Aggiunto controllo anche password attuale
                PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, newPassword) != PasswordVerificationResult.Failed)
                return Task.FromResult(true);
            return Task.FromResult(false);
        }

        public async Task<IdentityResult> AddToPasswordHistoryAsync(User user, string password)
        {
            user.PasswordHistory.Add(new PasswordHistory()
            {
                User = user,
                UserId = user.Id,
                PasswordHash = PasswordHasher.HashPassword(user, password),
                ChangedDateTime = DateTime.Now
            });

            /// Remove old passwords
            var toDelete = user.PasswordHistory
                .OrderBy(t => t.ChangedDateTime)
                .Skip(PASSWORD_HISTORY_LIMIT);
            foreach (var item in toDelete)
                user.PasswordHistory.Remove(item);

            return await UpdateAsync(user);
        }

        public Task<bool> CheckPINAsync(User user, string pin)
        {
            if (PasswordHasher.VerifyHashedPassword(user, user.PINHash, pin) != PasswordVerificationResult.Failed) return Task.FromResult(true);
            return Task.FromResult(false);
        }

        public Task<User> FindByPhoneNumberAsync(string phoneNumber)
        {
            var _user = Users.Where(U => U.PhoneNumber == phoneNumber);
            if (_user.Count() > 1) return Task.FromResult<User>(null);
            return Task.FromResult(_user.FirstOrDefault());
        }

        /// <summary>
        /// Return a flag indicate if current user password is expired
        /// </summary>
        public Task<bool> GetPasswordExpiredAsync(User user)
        {
            if (user.PasswordExpirationDateTime <= DateTime.Now) return Task.FromResult(true);
            return Task.FromResult(false);
        }

        public async Task<bool> SetCulture(User user, string culture)
        {
            if (!string.IsNullOrWhiteSpace(culture))
            {
                user.Culture = culture;
                await base.UpdateAsync(user);
                return true;
            }
            return false;
        }

        public bool UserHasClaim(string type, string value)
            => (User?.HasClaim(type, value)).Value;

        /// <summary>
        /// Restituisce tutti i claims collegati all'utente passato e a tutti i ruoli a lui associati
        /// </summary>
        public async Task<IList<Claim>> GetUserClaimsIncludeRolesAsync(User user)
        {
            List<Claim> result = new List<Claim>();
            result.AddRange(await GetClaimsAsync(user));
            if (SupportsUserRole)
            {
                var roles = await GetRolesAsync(user);
                foreach (var roleName in roles)
                {
                    if (_roleManager.SupportsRoleClaims)
                    {
                        var role = await _roleManager.FindByNameAsync(roleName);
                        if (role != null) result.AddRange(await _roleManager.GetClaimsAsync(role));
                    }
                }
            }
            return result;
        }

        public async Task<bool> HasUserClaimsIncludeRolesAsync(string type, string value)
            => await HasUserClaimsIncludeRolesAsync(await GetUserAsync(User), type, value);

        /// <summary>
        /// Controlla se l'utente passato ha il claim specificato
        /// </summary>
        public async Task<bool> HasUserClaimsIncludeRolesAsync(User user, string type, string value)
        {
            var claims = await GetUserClaimsIncludeRolesAsync(user);
            return claims.Any(t => t.Type == type && t.Value == value);
        }
    }
}