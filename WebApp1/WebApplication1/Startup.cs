using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1
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
            string transactionConnection = Configuration.GetConnectionString("TransactionConnection");
            if(transactionConnection != null)
            {
                services.AddDbContext<Models.TransactionContext>(options => options.UseSqlServer(transactionConnection));
            }
            else
            {
                throw new ArgumentNullException("No TransactionConnection field in configuration");
            }

            string authorizationConnection = Configuration.GetConnectionString("AuthorizationConnection");

            if(authorizationConnection != null)
            {
                services.AddDbContext<Models.Authorization.AuthorizationContext>(options => options.UseSqlServer(authorizationConnection));
            }
            else
            {
                throw new ArgumentNullException("No AuthorizationConnection field in configuration");
            }


            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<Models.Authorization.AuthorizationContext>();

            services.AddControllersWithViews();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
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
