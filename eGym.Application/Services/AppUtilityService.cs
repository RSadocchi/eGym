using eGym.Application.Option;
using eGym.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Globalization;
using System.Text;

namespace eGym.Application.Services
{
    public class AppUtilityService : IAppUtilityService
    {
        readonly IConfiguration _configuration;
        readonly IHttpContextAccessor _httpContext;
        readonly IWebHostEnvironment _environment;
        readonly ApplicationOptions _appOptions;
        readonly ILogger<AppUtilityService> _logger;

        public AppUtilityService(
            IConfiguration configuration,
            IHttpContextAccessor httpContext,
            IWebHostEnvironment environment,
            IOptions<ApplicationOptions> options,
            ILogger<AppUtilityService> logger)
        {
            _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _appOptions = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region ///COOKIES
        public void Cookie_Append(string name, string value, int expiringDays, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            _httpContext.HttpContext.Response.Cookies.Append(
                name,
                value.ToBase64(encoding),
                new CookieOptions()
                {
                    Expires = DateTime.Now.AddDays(expiringDays),
                    MaxAge = TimeSpan.FromDays(expiringDays),
                    SameSite = SameSiteMode.Lax,
                    Secure = true
                });
        }

        public string Cookie_Get(string name, Encoding encoding = null) => _httpContext.HttpContext.Request.Cookies[name]?.BackFromBase64((encoding ?? Encoding.UTF8));

        public void Cookie_Remove(string name) => _httpContext.HttpContext.Response.Cookies.Delete(name);
        #endregion

        #region ///LANG
        public bool ToggleLanguage(string lang)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(lang)) return false;
                var culture = new CultureInfo(_appOptions.DefaultCulture);
                Cookie_Remove(Const_Cookies.RequestCulture);
                Cookie_Append(name: Const_Cookies.RequestCulture, value: culture.TwoLetterISOLanguageName, 30);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(IAppUtilityService)}.{nameof(ToggleLanguage)} ERROR", ex);
                return false;
            }
        }
        #endregion
    }
}
