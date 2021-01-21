using eGym.Core.Security.Identity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eGym.Core.Security
{
    public interface IIdentityService
    {
        #region BASIC METHODS
        Task<User> GetUserAsync(string username, bool getByEmail = true, bool getByUsername = true, bool getByPhoneNumber = true);
        Task<int> GetCurrentUserIDAsync();
        Task<User> GetCurrentUserAsync();
        Task<User> FindByIDAsync(int ID);
        Task<User> FindByIDAsync(string ID);
        //Task<int?> GetAnagID();
        //Task<int?> GetRoleAnagID();
        //Task<int?> GetRoleGroupID();
        Task<Role> GetCurrentUserRole();
        Task<IdentityResult> CreateAsync(User user, string password, bool checkUniqueEmail = true, bool checkUniqueUsername = true, bool checkUniquePhoneNumber = true);
        Task<IdentityResult> UpdateAsync(User user, bool checkUniqueEmail = true, bool checkUniqueUsername = true, bool checkUniquePhoneNumber = true);
        Task<IdentityResult> DeleteAsync(User user);
        Task<IdentityResult> AddClaimsAsync(User user, Claim claim);
        Task<IdentityResult> UpdateClaimAsync(User user, Claim claim);
        Task<IList<Claim>> GetClaimsByRoleId(string roleId);
        Task<IList<Claim>> GetClaimsByRoleName(string roleName);
        Task<IList<Claim>> GetUserClaimsIncludeRoleClaimsAsync(User user);
        Task<bool> UserHasClaimAsync(User user, Claim claim, string roleName = null);
        Task<bool> CurrentUserHasClaimAsync(Claim claim);
        Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles);
        Task<IdentityResult> RemoveFromRolesAsync(User user, IEnumerable<string> roles);
        Task<IEnumerable<User>> GetUsersInRoleAsync(string roleName);
        Task<IEnumerable<User>> GetUsersInRolesAsync(params string[] roleNames);
        Task<IEnumerable<string>> GetUserRolesAsync(User user);
        Task<int?> GetRoleIdByRoleName(string roleName);
        Task<List<Role>> GetRolesByClaimTypes(string[] claimType);
        Task<bool> SetCultureAsync(User user, string culture);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);
        Task<IdentityResult> ChangePinAsync(User user, string currentPin, string newPin);
        Task<string> GenerateChangePhoneNumberTokenAsync(User user, string phoneNumber);
        Task<IdentityResult> ChangePhoneNumberAsync(User user, string phoneNumber, string token);
        Task<Role> RoleFindByIDAsync(string ID);
        #endregion

        #region LOGIN
        //Task<LoginResponse> UserLoginAPIAsync(
        //    string username, string password, bool rememberMe = false, string rememberValue = null, string culture = null,
        //    bool getByEmail = true, bool getByUsername = true, bool getByPhoneNumber = true);
        //Task<LoginResponse> UserLoginPINAsync(
        //    string username, string pin, string culture = null,
        //    bool getByEmail = true, bool getByUsername = true, bool getByPhoneNumber = true);
        //Task<LoginResponse> UserLogin2FAAsync(
        //    string username, string provider, string code, string culture = null,
        //    bool getByEmail = true, bool getByUsername = true, bool getByPhoneNumber = true);
        Task UserLogoutAsync();
        #endregion

        #region TOKEN
        //Task<string> GenerateEmailConfirmationTokenAsync(User user);
        //Task<string> GeneratePasswordResetTokenAsync(User user);
        //Task<string> GenerateTwoFactorTokenAsync(User user, string provider);
        //Task<Login2FAViewModel> GenerateAuthQRCodeUriXUserAsync(Login2FAViewModel model, User user, string applicationName);
        //Task<IdentityResult> ConfirmEmailAsync(User user, string token);
        //Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);
        //Task<bool> VerifyTwoFactorTokenAsync(User user, string tokenProvider, string token);
        //Task<LoginResponse> UserLoginWithRole(User user, string role = null, IEnumerable<Claim> claims = null);
        #endregion
    }
}
