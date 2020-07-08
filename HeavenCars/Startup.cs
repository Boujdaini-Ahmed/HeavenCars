using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeavenCars.DataAccesLayer.Context;
using HeavenCars.DataAccessLayer.Models.Account;
using HeavenCars.DataAccessLayer.Repositories.Cars;
//using HeavenCars.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HeavenCars
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(
           options =>
                 options.UseSqlServer(Configuration.GetConnectionString("HeavenCarsDBConnection"))
             );



            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;

                options.SignIn.RequireConfirmedEmail = true;

                //options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
            }).AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
            //.AddTokenProvider<CustomEmailConfirmationTokenProvider
            //<ApplicationUser>>("CustomEmailConfirmation");

            //services.Configure<CustomEmailConfirmationTokenProviderOptions>(o =>
            //o.TokenLifespan = TimeSpan.FromHours(5)); // Lors de l'envoie de TOUT les types de token confirmation la durée est de 5 heures avant son expiration

            services.Configure<DataProtectionTokenProviderOptions>(o =>
            o.TokenLifespan = TimeSpan.FromDays(3)); // changes le delai de temps de la confirmation d'addresse mail uniquement

            services.AddControllersWithViews().AddXmlSerializerFormatters();

            services.AddScoped<ICarRepository, CarRepository>();

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "932998242076-20t42dfguqd29a04gutpj6j5ckt1pbns.apps.googleusercontent.com";
                    options.ClientSecret = "2sVC0Qews71oIOajW1QpTjzA";
                   
                })
                .AddFacebook(options =>
                {
                    options.AppId = "1114496678906037";
                    options.AppSecret = "cec2fb15a9f7c66145e7ae180c6179e9";
                });         

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
