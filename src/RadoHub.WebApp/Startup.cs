using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using RadoHub.WebApp.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RadoHub.Data.Models;
using RadoHub.Services.Contracts;
using RadoHub.Services.Implementation;
using RadoHub.WebApp.Middlewares;
using RadoHub.Data.Repositories.Contracts;
using RadoHub.Data.Repositories.Implementation;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Razor;
using RadoHub.Services.Constants;
using System.Linq;

namespace RadoHub.WebApp
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
            services.AddDbContext<RadoHubDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<RadoHubUser, IdentityRole>(options =>
              {
                  options.Password.RequireDigit = false;
                  options.Password.RequiredUniqueChars = 0;
                  options.Password.RequireLowercase = false;
                  options.Password.RequireNonAlphanumeric = false;
                  options.Password.RequireUppercase = false;
              })
                .AddEntityFrameworkStores<RadoHubDbContext>()
            .AddDefaultTokenProviders()
            .AddDefaultUI();

            services.Configure<CloudinaryConfigs>(Configuration.GetSection("CloudinaryAccountCredits"));

            services.AddAutoMapper(typeof(Startup));

            // Background services at startup
            services.AddHostedService<WebPingService>();

            // Data repositories
            services.AddScoped<ICookingRecipeRepository, CookingRecipeRepository>();
            services.AddScoped<IInspirationRepository, InspirationRepository>();

            // Application services
            services.AddScoped<IUserAccountService, UserAccountService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ICookingRecipeService, CookingRecipeService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            var supportedCultures = SupportedCultures.GetAll.Select(culture => culture.IsoCode).ToArray();
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);


            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //auto seeds userRoles and default Admin user at application startup
            app.UseSeedDataMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
