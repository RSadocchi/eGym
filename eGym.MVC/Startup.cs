using AutoMapper;
using AutoMapper.EquivalencyExpression;
using eGym.Application.DTO;
using eGym.Application.Option;
using eGym.Application.Services;
using eGym.Core.Domain;
using eGym.Core.Localization;
using eGym.Core.Log;
using eGym.Core.Security;
using eGym.Core.Security.Identity;
using eGym.Core.SeedWork;
using eGym.MVC.Binders;
using eGym.MVC.Middleware;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.ResponseCompression;
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
        private string _localizationConnectionString { get; set; }
        private string _logConnectionString { get; set; }
        private string _secretKey { get; set; }

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

            DefaultCulture = new CultureInfo(Configuration.GetValue<string>($"{nameof(ApplicationOptions)}:{nameof(DefaultCulture)}") ?? "it");
            _secretKey = Configuration.GetValue<string>($"{nameof(ApplicationOptions)}:{nameof(ApplicationOptions.SecretKey)}");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            #region OPTIONS
            services.Configure<ApplicationOptions>(Configuration.GetSection(nameof(ApplicationOptions)));
            services.Configure<EmailOptions>(Configuration.GetSection(nameof(EmailOptions)));
            services.Configure<ApplicationResources>(Configuration.GetSection(nameof(ApplicationResources)));
            services.Configure<DocRepositoryOptions>(Configuration.GetSection(nameof(DocRepositoryOptions)));
            #endregion

            #region DB CONTEXT

#if DEBUG
            switch (System.Environment.MachineName)
            {
                case "BUBBLES":
                case "RSADO":
                    _securityConnectionString = string.Format(Configuration.GetConnectionString("Security"), $"{System.Environment.MachineName}\\SQLEXPRESS");
                    _applicationConnectionString = string.Format(Configuration.GetConnectionString("Default"), $"{System.Environment.MachineName}\\SQLEXPRESS");
                    _localizationConnectionString = string.Format(Configuration.GetConnectionString("Localization"), $"{System.Environment.MachineName}\\SQLEXPRESS");
                    _logConnectionString = string.Format(Configuration.GetConnectionString("Log"), $"{System.Environment.MachineName}\\SQLEXPRESS");
                    break;
                default:
                    _securityConnectionString = string.Format(Configuration.GetConnectionString("Security"), "(local)");
                    _applicationConnectionString = string.Format(Configuration.GetConnectionString("Default"), "(local)");
                    _localizationConnectionString = string.Format(Configuration.GetConnectionString("Localization"), "(local)");
                    _logConnectionString = string.Format(Configuration.GetConnectionString("Log"), "(local)");
                    break;
            }
#else
                    _securityConnectionString = sConfiguration.GetConnectionString("Security");
                    _applicationConnectionString = Configuration.GetConnectionString("Default");
                    _localizationConnectionString = Configuration.GetConnectionString("Localization");
                    _logConnectionString = Configuration.GetConnectionString("Localization");
#endif

            Console.WriteLine($" === SecurityConString: {_securityConnectionString}");
            services
                .AddDbContext<SecurityDbContext>(o =>
                    o.UseSqlServer(_securityConnectionString, opt => opt.MigrationsAssembly(typeof(SecurityDbContext).Assembly.GetName().Name)));

            Console.WriteLine($" === ApplicationConString: {_applicationConnectionString}");
            services
                .AddDbContext<ApplicationDbContext>(o =>
                    o.UseSqlServer(_applicationConnectionString, opt => opt
                        .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name)
                        .CommandTimeout(60)));

            Console.WriteLine($" === LocalizationConString: {_localizationConnectionString}");
            services
                .AddDbContext<LocalizationDbContext>(o =>
                    o.UseSqlServer(_localizationConnectionString, opt => opt.MigrationsAssembly(typeof(LocalizationDbContext).Assembly.GetName().Name)));

            Console.WriteLine($" === LogConString: {_logConnectionString}");
            services
                .AddDbContext<LogDbContext>(o =>
                    o.UseSqlServer(_logConnectionString, opt => opt.MigrationsAssembly(typeof(LogDbContext).Assembly.GetName().Name)));
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
                .AddEntityFrameworkStores<SecurityDbContext>()
                .AddDefaultTokenProviders();
            //services.AddIdentityCore<User>()
            //    .AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<SecurityDbContext>()
            //    .AddSignInManager()
            //    .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.LoginPath = "/";
                options.AccessDeniedPath = "/access-denied";
                options.SlidingExpiration = true;
            });
            
            var properties = typeof(Const_ClaimTypes).GetFields();
            foreach (var property in properties)
            {
                services
                    .AddAuthorization(opt =>
                    {
                        opt.AddPolicy(property.Name, policy => policy.RequireClaim(property.Name, Const_ClaimValues.DefaultValue));
                    });
            }
            #endregion

            #region DEPENDENCY INJECTION
            // REMEMBER ====
            // TRANSIENT:  A new instance is provided to every controller and every service
            // SCOPED:     Are the same within a request, but different across different requests
            // SINGLETON:  Are the same for every object and every request

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IComuniItalianiService, ComuniItalianiService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITaxCodeService, TaxCodeService>();
            services.AddScoped<IAppUtilityService, AppUtilityService>();

            #region Repositories
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<ICMSRepository, CMSRepository>();
            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<IAnagRepository, AnagRepository>();
            #endregion

            #region Services
            services.AddTransient<ITodoService, TodoService>();
            services.AddTransient<IAnagService, AnagService>();
            #endregion

            #endregion

            #region LOCALIZATION
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(Startup.DefaultCulture);
                options.SupportedCultures = Startup.SupportedCultures;
                options.SupportedUICultures = Startup.SupportedCultures;
            });
            #endregion

            #region GZIP
            services.Configure<GzipCompressionProviderOptions>(opt => opt.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression(opt => opt.EnableForHttps = true);
            #endregion

            #region MVC
            services
                .Configure<RazorViewEngineOptions>(options =>
                {
                    options.AreaViewLocationFormats.Clear();
                    options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}.cshtml");
                    options.AreaViewLocationFormats.Add("/Areas/{2}/Views/Shared/{0}.cshtml");
                    options.AreaViewLocationFormats.Add("/Areas/{2}/Views/Dashboard/Partials/{0}.cshtml");
                    options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
                });

            services
                .AddMvc(opt =>
                {
                    opt.ModelBinderProviders.Insert(0, new CustomBinderProvider());
                })
               .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
               .AddDataAnnotationsLocalization()
               .AddFluentValidation(opt =>
               {
                   opt.ImplicitlyValidateChildProperties = true;
               });
            #endregion

            #region AUTOMAPPER
            IMapper mapper = new MapperConfiguration(c =>
            {
                c.AddCollectionMappers();

                c.AddProfile(new TodoDTO.ProfileConfig());
                c.AddProfile(new DynamicRoleDTO.ProfileConfig());
                c.AddProfile(new DocumentDTO.ProfileConfig());
                c.AddProfile(new ContactDTO.ProfileConfig());
                c.AddProfile(new AddressDTO.ProfileConfig());
                c.AddProfile(new AnagDTO.ProfileConfig());
            }).CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            #region FLUENT VALIDATION
            #region DTO

            #endregion

            #region MODEL

            #endregion
            #endregion
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

                using (var serviceScoped = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    using (var ctx = serviceScoped.ServiceProvider.GetService<SecurityDbContext>())
                    {
                        ctx.MigrationConnectionString = _securityConnectionString;
                        ctx.Database.Migrate();
                    }
                    using (var ctx = serviceScoped.ServiceProvider.GetService<ApplicationDbContext>())
                    {
                        ctx.MigrationConnectionString = _applicationConnectionString;
                        ctx.SecurityToken = _secretKey;
                        ctx.Database.Migrate();
                    }
                    using (var ctx = serviceScoped.ServiceProvider.GetService<LocalizationDbContext>())
                    {
                        ctx.MigrationConnectionString = _localizationConnectionString;
                        ctx.Database.Migrate();
                    }
                    using (var ctx = serviceScoped.ServiceProvider.GetService<LogDbContext>())
                    {
                        ctx.MigrationConnectionString = _logConnectionString;
                        ctx.Database.Migrate();
                    }
                }
            }

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(Startup.DefaultCulture),
                SupportedCultures = Startup.SupportedCultures,
                SupportedUICultures = Startup.SupportedCultures
            });

            app.UseRequestLocalization();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseMiddleware<TelemetryGDPR>();
            app.UseMiddleware<ErrorHandlingMiddleware>();

            //app.Use(async (context, next) =>
            //{
            //    context.Request.EnableBuffering();
            //    await next();
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
