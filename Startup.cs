using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Westwind.AspNetCore.LiveReload;
using Elearningfake.Models.Services.Application;
using Elearningfake.Models.Services.Infrastructure;

namespace ELearningfake
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLiveReload();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddTransient<ICorsoService, AdoNetCorsiService>();
            services.AddTransient<IDatabaseAccesso, SQLiteDatabaseAccesso>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseLiveReload();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            //app.UseMvcWithDefaultRoute();
            app.UseMvc(routeBuilder =>
            {
                routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            }
            );
        }
    }
}
