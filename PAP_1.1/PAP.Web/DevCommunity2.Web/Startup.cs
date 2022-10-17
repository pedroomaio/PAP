using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PAP.Business.DbContext;
using PAP.Business.Managers;
using PAP.DataBase.Auth;
using PAP.Business.Repositories;
using PAP.Business.Persistence.Repositories;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Authentication;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevCommunity2.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            services.AddDbContext<ApplicationDatabaseContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("DevCommunity2.Web")));


            services.AddDefaultIdentity<User>()
               .AddRoles<UserRole>()              
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddUserManager<ApplicationUserManager>()
                .AddSignInManager<ApplicatonSignInManager>()
                .AddEntityFrameworkStores<ApplicationDatabaseContext>();

            services.AddAuthentication()
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = "451240372332983";
                    facebookOptions.AppSecret = "c31cb56e88d62c3687388b228e028299";
                })
                .AddGitHub(options =>
                {
                    options.ClientId = "5bd671ecf886a17f4db6";
                    options.ClientSecret = "403cbb7d3193ac6692fbb16976c187be5d8a4173";
                })
                .AddGoogle(googleOptions => {
                    googleOptions.ClientId = "256612312408-9qbfilnst4hejssitaqkdv2p8744oisv.apps.googleusercontent.com";
                    googleOptions.ClientSecret = "yPYXlDIPEbAwwwpERncZNjKD";
                    googleOptions.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
                    googleOptions.SaveTokens = true;

                    googleOptions.Events.OnCreatingTicket = ctx =>
                    {
                        List<AuthenticationToken> tokens = ctx.Properties.GetTokens().ToList();

                        tokens.Add(new AuthenticationToken()
                        {
                            Name = "TicketCreated",
                            Value = DateTime.UtcNow.ToString()
                        });

                        ctx.Properties.StoreTokens(tokens);
                        return Task.CompletedTask;
                    };

                });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                //options.LoginPath = "/Identity/Account/Login";
                //options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                //options.SlidingExpiration = true;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
               
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IPublishAccountRepository, PublishAccountRepository>();
            services.AddTransient<IPublishEventRepository, PublishEventRepository>();

            services.AddTransient<BaseManager>();
            services.AddTransient<ApplicationUserManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
