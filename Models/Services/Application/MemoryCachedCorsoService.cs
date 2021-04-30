using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElearningDemo.Models.InputModels;
using ElearningDemo.Models.Options;
using ElearningDemo.Models.Services.Application;
using ElearningDemo.Models.ViewModels;
using ELearningDemo.Models.InputModels;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ELearningDemo.Models.Services.Application
{
    public class MemoryCachedCourseService : ICachedCourseService
    {
        private readonly ICourseService courseService;
        private readonly IMemoryCache memoryCache;
        private readonly IConfiguration configurationOption;
        private readonly IOptionsMonitor<CachedOption> cachedOption;

        public MemoryCachedCourseService(ICourseService courseService, IMemoryCache memoryCache, IConfiguration configurationOption, IOptionsMonitor<CachedOption> cachedOption)
        {
            this.cachedOption = cachedOption;
            this.configurationOption = configurationOption;
            this.memoryCache = memoryCache;
            this.courseService = courseService;
        }

        public Task<CourseDetailViewModel> CreateCourseAsync(CourseCreateInputModel inputModel)
        {
            return courseService.CreateCourseAsync(inputModel);
        }

        public Task<List<CoursesViewModel>> GetBestCourseAsync()
        {
            double cachedMaxtime = configurationOption.GetSection("CachedTime").GetValue<double>("TimeSpanCourse");
            return memoryCache.GetOrCreateAsync($"BestCourse", cacheEntry =>
            {
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(cachedMaxtime));
                return courseService.GetBestCourseAsync();
            });
        }

        public Task<ListViewModel<CoursesViewModel>> GetCoursesAsync(CoursesListInputModel model)
        {

            // Vengono messe in cache solo le prime 5 pagine in quanto sono le più visitate e viene sfruttata la cache solo se l'utente non ha cercato nulla
            bool canCache = model.Page <= 5 && string.IsNullOrWhiteSpace(model.Search);

            // Se canCdouble cachedMaxtime = configurationOption.GetSection("CachedTime").GetValue<double>("TimeSpanCourse");ache è true, usa il servizio di caching
            if (canCache)
            {
                double cachedMaxtime = configurationOption.GetSection("CachedTime").GetValue<double>("TimeSpanCourse");
                return memoryCache.GetOrCreateAsync($"Course {model.Page}-{model.OrderBy}-{model.Ascending}", cacheEntry =>
                {
                    //cacheEntry.SetSize(1);
                    cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(cachedMaxtime));
                    return courseService.GetCoursesAsync(model);
                });
            }

            // Se canCache è false, usa direttamente l'applicazione di servizio
            return courseService.GetCoursesAsync(model);
            
        }

        public Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            double cachedMaxTime = cachedOption.CurrentValue.TimeSpanDettaglioCourse;
            return memoryCache.GetOrCreateAsync($"Course {id}", cacheEntry =>
            {
                //cacheEntry.SetSize(1);
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(cachedMaxTime));
                return courseService.GetCourseAsync(id);
            });
        }

        public Task<List<CoursesViewModel>> GetMostRecentCourseAsync()
        {
            double cachedMaxtime = configurationOption.GetSection("CachedTime").GetValue<double>("TimeSpanCourse");
            return memoryCache.GetOrCreateAsync($"MostRecentCourses", cacheEntry =>
            {
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(cachedMaxtime));
                return courseService.GetMostRecentCourseAsync();
            });
        }
    }
}