using eGym.Core.Security.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace eGym.Core.Security
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly SignInManager _signInManager;
        private readonly UrlEncoder _urlEncoder;
        private readonly ILogger<IdentityService> _logger;
        private readonly IConfiguration _configuration;
        //private readonly ISMSService _smsService;
        //private readonly IEmailService _emailService;
        //private readonly ITranslateService<IdentityResources> _translateService;

        public IdentityService(
            UserManager userManager,
            RoleManager roleManager,
            SignInManager signInManager,
            ILogger<IdentityService> logger,
            UrlEncoder urlEncoder,
            IConfiguration configuration)//,
            //ISMSService smsService,
            //IEmailService emailService,
            //ITranslateService<IdentityResources> translateService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _urlEncoder = urlEncoder ?? throw new ArgumentNullException(nameof(urlEncoder));
            //_smsService = smsService ?? throw new ArgumentNullException(nameof(smsService));
            //_emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            //_translateService = translateService ?? throw new ArgumentNullException(nameof(translateService));
        }


        #region BASIC METHODS
        public async Task<User> GetUserAsync(string username, bool getByEmail = true, bool getByUsername = true, bool getByPhoneNumber = true)
        {
            User user = null;
            if (getByUsername) user = await _userManager.FindByNameAsync(username);
            if (user != null) return user;
            if (getByEmail) user = await _userManager.FindByEmailAsync(username);
            if (user != null) return user;
            if (getByPhoneNumber) user = await _userManager.FindByPhoneNumberAsync(username);
            return user;
        }

        public async Task<int> GetCurrentUserIDAsync()
            => _userManager.UserID;

        public async Task<User> GetCurrentUserAsync()
            => await _userManager.FindByIdAsync((await GetCurrentUserIDAsync()).ToString());

        public async Task<User> FindByIDAsync(int ID)
            => await _userManager.FindByIdAsync(ID.ToString());

        public async Task<User> FindByIDAsync(string ID)
             => await _userManager.FindByIdAsync(ID);

        //public async Task<int?> GetAnagID()
        //{
        //    int.TryParse(_userManager.User.FindFirst(En_ClaimAnagType.ANAGID.ToString())?.Value, out int ID); //Anagrafica dell'utente loggato correntemente
        //    return await Task.FromResult((ID > 0) ? (int?)ID : null);
        //}

        //public async Task<int?> GetRoleAnagID()
        //{
        //    int.TryParse(_userManager.User.FindFirst(En_ClaimAnagType.ROLE_ANAGID.ToString())?.Value, out int ID); //Anagrafica del cliente / dealer (anagrafica con ang_role) selezionato al login
        //    return await Task.FromResult((ID > 0) ? (int?)ID : null);
        //}

        //public async Task<int?> GetRoleGroupID()
        //{
        //    int.TryParse(_userManager.User.FindFirst("", out int ID); //Anagrafica del gruppo collegato all'utente loggato correntemente, selezionata al login
        //    return await Task.FromResult((ID > 0) ? (int?)ID : null);
        //}

        public async Task<Role> GetCurrentUserRole()
        {
            string roleName = _userManager.User.FindFirst("UserClaimsRole")?.Value;
            return await _roleManager.FindByNameAsync(roleName);
        }

        public async Task<IdentityResult> CreateAsync(User user, string password, bool checkUniqueEmail = true, bool checkUniqueUsername = true, bool checkUniquePhoneNumber = true)
        {
            // Controllo che non esista già un utente
            if (checkUniqueEmail)
            {
                var existingUser = await this.GetUserAsync(user.Email, getByEmail: true, getByUsername: false, getByPhoneNumber: false);
                if (existingUser != null) throw new Exception(("USER_EXISTING_EMAIL"));
            }
            if (checkUniqueUsername)
            {
                var existingUser = await this.GetUserAsync(user.UserName, getByEmail: false, getByUsername: true, getByPhoneNumber: false);
                if (existingUser != null) throw new Exception(("USER_EXISTING_USERNAME"));
            }
            if (checkUniquePhoneNumber)
            {
                var existingUser = await this.GetUserAsync(user.PhoneNumber ?? "noPhoneNumberProvided", getByEmail: false, getByUsername: false, getByPhoneNumber: true);
                if (existingUser != null) throw new Exception(("USER_EXISTING_PHONE"));
            }
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> UpdateAsync(User user, bool checkUniqueEmail = true, bool checkUniqueUsername = true, bool checkUniquePhoneNumber = true)
        {
            // Controllo che non esista già un utente
            if (checkUniqueEmail)
            {
                var existingUser = await this.GetUserAsync(user.Email, getByEmail: true, getByUsername: false, getByPhoneNumber: false);
                if (existingUser != null && existingUser.Id != user.Id) throw new Exception(("USER_EXISTING_EMAIL"));
            }
            if (checkUniqueUsername)
            {
                var existingUser = await this.GetUserAsync(user.UserName, getByEmail: false, getByUsername: true, getByPhoneNumber: false);
                if (existingUser != null && existingUser.Id != user.Id) throw new Exception(("USER_EXISTING_USERNAME"));
            }
            if (checkUniquePhoneNumber && !string.IsNullOrWhiteSpace(user.PhoneNumber))
            {
                var existingUser = await this.GetUserAsync(user.PhoneNumber, getByEmail: false, getByUsername: false, getByPhoneNumber: true);
                if (existingUser != null && existingUser.Id != user.Id) throw new Exception(("USER_EXISTING_PHONE"));
            }
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteAsync(User user)
            => await _userManager.DeleteAsync(user);

        public async Task<IdentityResult> AddClaimsAsync(User user, Claim claim)
            => await _userManager.AddClaimAsync(user, claim);

        public async Task<IdentityResult> UpdateClaimAsync(User user, Claim claim)
        {
            if ((await _userManager.GetUserClaimsAsync(user: user, getUserClaims: true, getRoleClaims: false))?.Where(t => t.Type == claim.Type)?.Count() > 0)
                await _userManager.RemoveClaimAsync(user, claim);
            return await _userManager.AddClaimAsync(user, claim);
        }

        public async Task<IList<Claim>> GetClaimsByRoleId(string roleId) => await _userManager.GetClaimsByRoleId(roleId);

        public async Task<IList<Claim>> GetClaimsByRoleName(string roleName) => await _userManager.GetClaimsByRoleName(roleName);

        public async Task<IList<Claim>> GetUserClaimsIncludeRoleClaimsAsync(User user)
            => await _userManager.GetUserClaimsAsync(user, roleName: null, getUserClaims: true, getRoleClaims: true);

        public async Task<bool> UserHasClaimAsync(User user, Claim claim, string roleName = null)
        {
            var userClaims = await _userManager.GetUserClaimsAsync(user, roleName: roleName, getUserClaims: true, getRoleClaims: true);
            return userClaims.Any(t => t.Type == claim.Type && t.Value == claim.Value);
        }

        public async Task<bool> CurrentUserHasClaimAsync(Claim claim)
        {
            return await Task.FromResult(_userManager.User.FindFirst(claim.Type)?.Value == claim.Value);
        }

        public async Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles)
            => await _userManager.AddToRolesAsync(user, roles);

        public async Task<IdentityResult> RemoveFromRolesAsync(User user, IEnumerable<string> roles)
            => await _userManager.RemoveFromRolesAsync(user, roles);

        public async Task<IEnumerable<User>> GetUsersInRoleAsync(string roleName)
            => await _userManager.GetUsersInRoleAsync(roleName);
        public async Task<IEnumerable<User>> GetUsersInRolesAsync(params string[] roleNames)
        {
            var users = new List<User>();
            if (roleNames?.Count() > 0)
                foreach (var role in roleNames.Where(t => !string.IsNullOrWhiteSpace(t)))
                    users.AddRange(await _userManager.GetUsersInRoleAsync(role));
            return users?.Distinct()?.OrderBy(t => t.Id);
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(User user)
            => await _userManager.GetRolesAsync(user);

        public async Task<int?> GetRoleIdByRoleName(string roleName)
            => await _roleManager.GetRoleIdByRoleName(roleName);

        public async Task<List<Role>> GetRolesByClaimTypes(string[] claimNames)
            => await _roleManager.GetRolesByClaimTypes(claimNames);

        public async Task<bool> SetCultureAsync(User user, string culture)
            => await _userManager.SetCultureAsync(user, culture);

        public async Task<bool> CheckPasswordAsync(User user, string password)
            => await _userManager.CheckPasswordAsync(user, password);

        public async Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
            => await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        public async Task<IdentityResult> ChangePinAsync(User user, string currentPin, string newPin)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> ChangePhoneNumberAsync(User user, string phoneNumber, string token)
            => await _userManager.ChangePhoneNumberAsync(user, phoneNumber, token);

        public async Task<string> GenerateChangePhoneNumberTokenAsync(User user, string phoneNumber)
            => await _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);

        public async Task<Role> RoleFindByIDAsync(string ID) => await _roleManager.FindByIdAsync(ID);

        #endregion

        #region LOGIN
        //public async Task<LoginResponse> UserLoginAPIAsync(
        //    string username, string password, bool rememberMe = false, string rememberValue = null, string culture = null,
        //    bool getByEmail = true, bool getByUsername = true, bool getByPhoneNumber = true)
        //{
        //    var user = await GetUserAsync(username, getByEmail, getByUsername, getByPhoneNumber) ?? throw new ValidationException(_translateService.Instant("USER_NOT_FOUND"));

        //    if (await _userManager.IsLockedOutAsync(user))
        //        throw new ValidationException(_translateService.Instant("USER_LOCKEDOUT"));
        //    if (!await _userManager.CheckPasswordAsync(user, password))
        //    {
        //        if (await _userManager.GetLockoutEnabledAsync(user)) await _userManager.AccessFailedAsync(user);
        //        throw new ValidationException(_translateService.Instant("USER_CREDENTIALS"));
        //    }
        //    else
        //    {
        //        if (user.Disabled)
        //        {
        //            if (!user.EmailConfirmed) throw new ValidationException(_translateService.Instant("USER_EMAIL_UNCONFIRMED"));
        //            else throw new ValidationException(_translateService.Instant("USER_DISABLED"));
        //        }
        //        if (user.TwoFactorEnabled)
        //        {
        //            if (rememberMe && !string.IsNullOrEmpty(rememberValue) && rememberValue.Equals(user.Id.ToString(), StringComparison.InvariantCultureIgnoreCase))
        //                return await UserTkenizer(user, culture);
        //            else return new LoginResponse() { Token2FA = "2FA-ENABLED" };
        //        }
        //        else return await UserTkenizer(user, culture);
        //    }
        //    throw new ValidationException(_translateService.Instant("USER_CREDENTIALS"));
        //}

        ///// <summary>
        ///// TODO Verificare coretto funzionamento della GetUserAsync considerando le univocità impostate su identity
        ///// (Es email non univoca)
        ///// </summary>
        //public async Task<LoginResponse> UserLogin2FAAsync(
        //    string username, string provider, string code, string culture = null,
        //    bool getByEmail = true, bool getByUsername = true, bool getByPhoneNumber = true)
        //{
        //    var user = await GetUserAsync(username, getByEmail, getByUsername, getByPhoneNumber) ?? throw new ValidationException(_translateService.Instant("USER_NOT_FOUND"));
        //    if (await _userManager.VerifyTwoFactorTokenAsync(user, provider, code))
        //        return await UserTkenizer(user, culture);
        //    throw new ValidationException(_translateService.Instant("USER_2FA_CODE"));
        //}

        ///// <summary>
        ///// TODO Verificare coretto funzionamento della GetUserAsync considerando le univocità impostate su identity
        ///// (Es email non univoca)
        ///// </summary>
        //public async Task<LoginResponse> UserLoginPINAsync(
        //    string username, string pin, string culture = null,
        //    bool getByEmail = true, bool getByUsername = true, bool getByPhoneNumber = true)
        //{
        //    var user = await GetUserAsync(username, getByEmail, getByUsername, getByPhoneNumber) ?? throw new ValidationException(_translateService.Instant("USER_NOT_FOUND"));
        //    if (culture != null && user?.Culture != culture) await _userManager.SetCultureAsync(user, culture);

        //    if (await _userManager.IsLockedOutAsync(user)) throw new ValidationException(_translateService.Instant("USER_LOCKEDOUT"));
        //    if (await _userManager.CheckPINAsync(user, pin))
        //        return await UserTkenizer(user, culture);
        //    if (await _userManager.GetLockoutEnabledAsync(user))
        //        await _userManager.AccessFailedAsync(user);

        //    throw new ValidationException(_translateService.Instant("USER_CREDENTIALS"));
        //}

        //public async Task<LoginResponse> UserLoginWithRole(User user, string role = null, IEnumerable<Claim> claims = null)
        //{
        //    return await UserTkenizer(user, user.Culture, role, true, true, claims);
        //}

        public async Task UserLogoutAsync()
        {
            await _signInManager.SignOutAsync();
            //var user = await GetCurrentUserAsync();
            //await _userManager.RemoveAuthenticationTokenAsync(user, _options.Issuer, _options.Audience);
        }
        #endregion

        #region PRIVATE
        //private async Task<LoginResponse> UserTkenizer(User user, string culture = null, string roleName = null, bool getUserClaims = true, bool getRoleClaims = false, IEnumerable<Claim> claims = null)
        //{
        //    await _userManager.ResetAccessFailedCountAsync(user);
        //    //if (await _userManager.IsPasswordExpiredAsync(user))
        //    //    return new LoginResponse()
        //    //    {
        //    //        Message = "PASSWORD_EXPIRED",
        //    //        TokenRecoverPassword = user.Id.ToString() // TODO: Va cryptato ma senza richiamare la cipherService
        //    //    };

        //    // Check user disabled
        //    if (user.Disabled && user.EmailConfirmed == true) throw new ValidationException(_translateService.Instant("USER_DISABLED"));
        //    if (user.Disabled && user.EmailConfirmed == false) throw new ValidationException(_translateService.Instant("USER_EMAIL_UNCONFIRMED"));

        //    _logger.LogInformation("User login {0} - {1}", user.UserName, user.Email);
        //    if (culture != null && user?.Culture != culture) await _userManager.SetCultureAsync(user, culture);

        //    //Genero un token generico di accesso senza ruoli, il ruolo viene scelto successivamente in base ai ruoli definiti nel collegamento Anag_AnagXAnagLink
        //    var jwtIssuerOptionsNotStatic = new JwtIssuerOptions();
        //    IList<Claim> userClaims = new List<Claim>();
        //    //aggiunta dei claim personalizzati se presenti
        //    if (claims != null)
        //    {
        //        foreach (Claim c in claims)
        //            userClaims.Add(c);
        //    }
        //    foreach (Claim c in await _userManager.GetUserClaimsAsync(user, roleName: roleName, getRoleClaims: getRoleClaims, getUserClaims: getUserClaims))
        //    {
        //        //lo aggiunge solo se non c'è già
        //        if (userClaims.Where(uc => uc.Type == c.Type).Count() == 0)
        //            userClaims.Add(c);
        //    }
        //    userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, jwtIssuerOptionsNotStatic.IssuedAt.ToUnixEpochDate().ToString(), ClaimValueTypes.Integer64));
        //    userClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, jwtIssuerOptionsNotStatic.IssuedAt.ToUnixEpochDate().ToString(), ClaimValueTypes.Integer64));
        //    userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserName));
        //    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        //    userClaims.Add(new Claim("culture", user.Culture ?? "", ClaimValueTypes.String));


        //    var jwt = new JwtSecurityToken(
        //        issuer: _options.Issuer,
        //        audience: _options.Audience,
        //        claims: userClaims,
        //        notBefore: jwtIssuerOptionsNotStatic.NotBefore,
        //        expires: jwtIssuerOptionsNotStatic.Expiration,
        //        signingCredentials: _options.SigningCredentials);

        //    return new LoginResponse()
        //    {
        //        Token = new JwtSecurityTokenHandler().WriteToken(jwt),
        //        needRoleSelection = !(userClaims.Where(c => c.Type == "UserClaimsRole" && !string.IsNullOrEmpty(c.Value)).Count() > 0)
        //    };
        //}
        #endregion

        #region TOKEN
        //public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        //    => await _userManager.GenerateEmailConfirmationTokenAsync(user);

        //public async Task<string> GeneratePasswordResetTokenAsync(User user)
        //    => await _userManager.GeneratePasswordResetTokenAsync(user);

        //public async Task<string> GenerateTwoFactorTokenAsync(User user, string provider)
        //    => await _userManager.GenerateTwoFactorTokenAsync(user, provider);

        //public async Task<Login2FAViewModel> GenerateAuthQRCodeUriXUserAsync(Login2FAViewModel model, User user, string applicationName)
        //{
        //    var unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
        //    if (string.IsNullOrWhiteSpace(unformattedKey))
        //    {
        //        await _userManager.ResetAuthenticatorKeyAsync(user);
        //        unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
        //    }

        //    model.TwoFactorProvider = TwoFactorProviders.Authenticator;
        //    model.QRCodeModel = new AuthApp2FAQRCodeViewModel
        //    {
        //        SharedKey = _formatKey(unformattedKey),
        //        AuthenticatorUri = string.Format(TwoFactorProviders.AuthenticatorUriFormat, _urlEncoder.Encode(applicationName), _urlEncoder.Encode(user.UserName), unformattedKey)
        //    };

        //    string _formatKey(string source)
        //    {
        //        var result = new StringBuilder();
        //        int currentPosition = 0;
        //        while (currentPosition + 4 < source.Length)
        //        {
        //            result.Append(source.Substring(currentPosition, 4)).Append(" ");
        //            currentPosition += 4;
        //        }
        //        if (currentPosition < source.Length)
        //            result.Append(source.Substring(currentPosition));
        //        return result.ToString().ToLowerInvariant();
        //    }
        //    return model;
        //}

        //public async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
        //    => await _userManager.ConfirmEmailAsync(user, token);

        //public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
        //    => await _userManager.ResetPasswordAsync(user, token, newPassword);

        //public async Task<bool> VerifyTwoFactorTokenAsync(User user, string tokenProvider, string token)
        //    => await _userManager.VerifyTwoFactorTokenAsync(user, tokenProvider, token);
        #endregion
    }
}
