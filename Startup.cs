using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using ElearningDemo.Models.Services.Application;
using ElearningDemo.Models.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ELearningDemo.Models.Services.Infrastructure;
using Microsoft.Extensions.Configuration;
using ElearningDemo.Models.Options;
using Microsoft.Extensions.Hosting;
using ELearningDemo.Models.Services.Application;
#if DEBUG
using Westwind.AspNetCore.LiveReload;
#endif

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
            #if DEBUG
            services.AddLiveReload();
            #endif

            services.AddResponseCaching();
            services.AddMvc(option =>
            {
                var homeProfile = new CacheProfile();
                //homeProfile.Duration = Configuration.GetSection("ResponseCache").GetSection("Home").GetValue<int>("Duration");
                //homeProfile.Location = Configuration.GetValue<ResponseCacheLocation>("ResponseCache:Home:Location");
                //homeProfile.VaryByQueryKeys = new string[] { "page" };
                Configuration.Bind("ResponseCache:Home", homeProfile);

                option.CacheProfiles.Add("Home", homeProfile);
            }).SetCompatibilityVersion(CompatibilityVersion.Latest)
            #if DEBUG
            .AddRazorRuntimeCompilation()
            #endif
            ;

            services.AddTransient<ICourseService, EfCoreCourseService>();
            //services.AddTransient<ICourseService, AdoNetCourseService>();
            services.AddTransient<IDatabaseAccesso, SQLiteDatabaseAccesso>();
            services.AddTransient<ICachedCourseService, MemoryCachedCourseService>();

            services.AddSingleton<IErrorViewSelectorService, ErrorViewSelectorService>();

            //services.AddScoped<MioCourseDbContext>();
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
            //TODO: resolve problem with memorycache and EfCore
            //services.Configure<MemoryCacheOptions>(Configuration.GetSection("MemoryCache"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #if DEBUG
            app.UseLiveReload();
            #endif

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            //Endpoint Routing Middleware
            app.UseRouting();

            app.UseResponseCaching();

            //app.UseMvcWithDefaultRoute();

            //Endpoint Middleware
            app.UseEndpoints(routing => {
                routing.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
