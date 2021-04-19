using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Westwind.AspNetCore.LiveReload;
using ElearningDemo.Models.Services.Application;
using ElearningDemo.Models.Services.Infrastructure;
using elearningfake.Models.Services.Application;
using Microsoft.EntityFrameworkCore;
using ELearningFake.Models.Services.Infrastructure;
using Microsoft.Extensions.Configuration;
using ElearningDemo.Models.Options;
using Microsoft.Extensions.Logging;
using ELearningFake.Models.Services.Application;
using Microsoft.Extensions.Caching.Memory;

namespace ELearningDemo
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLiveReload();
            services.AddResponseCaching();
            services.AddMvc(option =>
            {
                var homeProfile = new CacheProfile();
                //homeProfile.Duration = Configuration.GetSection("ResponseCache").GetSection("Home").GetValue<int>("Duration");
                //homeProfile.Location = Configuration.GetValue<ResponseCacheLocation>("ResponseCache:Home:Location");
                //homeProfile.VaryByQueryKeys = new string[] { "page" };
                Configuration.Bind("ResponseCache:Home", homeProfile);

                option.CacheProfiles.Add("Home", homeProfile);
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddTransient<ICorsoService, EfCoreCorsiService>();
            services.AddTransient<ICorsoService, AdoNetCorsiService>();
            services.AddTransient<IDatabaseAccesso, SQLiteDatabaseAccesso>();
            services.AddTransient<ICachedCorsoService, MemoryCachedCorsoService>();

            services.AddSingleton<IErrorViewSelectorService, ErrorViewSelectorService>();

            //services.AddScoped<MioCorsoDbContext>();
            services.AddDbContextPool<MyCourseDbContext>(option =>
                {
                    string connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");
                    option.UseSqlite(connectionString);
                }
            );

            //Options
            services.Configure<ConnectionStringsOptions>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<CoursesOptions>(Configuration.GetSection("Courses"));
            services.Configure<CachedOption>(Configuration.GetSection("CachedTime"));
            services.Configure<MemoryCacheOptions>(Configuration.GetSection("MemoryCache"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseLiveReload();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseResponseCaching();

            //app.UseMvcWithDefaultRoute();
            app.UseMvc(routeBuilder =>
            {
                routeBuilder.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
