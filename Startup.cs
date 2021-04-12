using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Westwind.AspNetCore.LiveReload;
using Elearningfake.Models.Services.Application;
using Elearningfake.Models.Services.Infrastructure;
using elearningfake.Models.Services.Application;
using Microsoft.EntityFrameworkCore;
using ELearningFake.Models.Services.Infrastructure;

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
            //services.AddTransient<ICorsoService, AdoNetCorsiService>();
            services.AddTransient<ICorsoService, EfCoreCorsiService>();
            services.AddTransient<IDatabaseAccesso, SQLiteDatabaseAccesso>();

            //services.AddScoped<MioCorsoDbContext>();
            services.AddDbContextPool<MioCorsoDbContext>(option => 
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                option.UseSqlite("Data Source=Data/MioCorso.db");
            }
            );
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
