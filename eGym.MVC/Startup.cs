using eGym.Core.Domain;
using eGym.Core.Security.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace eGym.MVC
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public static CultureInfo DefaultCulture { get; private set; }
        public static CultureInfo[] SupportedCultures => new CultureInfo[]
        {
            new CultureInfo("it"),
            new CultureInfo("it-IT"),
            new CultureInfo("en"),
            new CultureInfo("en-GB"),
            new CultureInfo("en-US")
        };

        private string _securityConnectionString { get; set; }
        private string _applicationConnectionString { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Console.WriteLine($" === ENVIRONMENT: {environment.EnvironmentName}");
            Environment = environment;
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            DefaultCulture = new CultureInfo(Configuration.GetValue<string>(nameof(DefaultCulture)) ?? "it");
        }

        public void ConfigureServices(IServiceCollection services)
        {

            #region LOCALIZATION
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(Startup.DefaultCulture);
                options.SupportedCultures = Startup.SupportedCultures;
                options.SupportedUICultures = Startup.SupportedCultures;
            });
            #endregion

            #region DB CONTEXT
            _applicationConnectionString = Configuration.GetConnectionString("Default");
            Console.WriteLine($" === ApplicationConString: {_applicationConnectionString}");
            services
                .AddDbContext<ApplicationDbContext>(o =>
                    o.UseSqlServer(_applicationConnectionString, opt => opt
                        .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name)
                        .CommandTimeout(60)));
            #endregion

            #region SECURITY & IDENTITY
            services
                .AddIdentity<User, Role>(identityOptions =>
                {
                    identityOptions.Password.RequireDigit = Configuration.GetValue<bool>("security:identityOptions:Password:RequireDigit");
                    identityOptions.Password.RequiredLength = Configuration.GetValue<int>("security:identityOptions:Password:RequiredLength");
                    identityOptions.Password.RequiredUniqueChars = Configuration.GetValue<int>("security:identityOptions:Password:RequiredUniqueChars");
                    identityOptions.Password.RequireLowercase = Configuration.GetValue<bool>("security:identityOptions:Password:RequireLowercase");
                    identityOptions.Password.RequireNonAlphanumeric = Configuration.GetValue<bool>("security:identityOptions:Password:RequireNonAlphanumeric");
                    identityOptions.Password.RequireUppercase = Configuration.GetValue<bool>("security:identityOptions:Password:RequireUppercase");
                    identityOptions.User.RequireUniqueEmail = Configuration.GetValue<bool>("security:identityOptions:User:RequireUniqueEmail");
                    identityOptions.SignIn.RequireConfirmedEmail = Configuration.GetValue<bool>("security:identityOptions:SignIn:RequireConfirmedEmail");
                    identityOptions.SignIn.RequireConfirmedPhoneNumber = Configuration.GetValue<bool>("security:identityOptions:SignIn:RequireConfirmedPhoneNumber");
                })
                .AddUserManager<UserManager>()
                .AddRoleManager<RoleManager>()
                .AddSignInManager<SignInManager>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<CustomIdentityErrorDescriber_IT>();
            #endregion

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
