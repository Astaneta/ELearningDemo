using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElearningDemo.Models.Options;
using ElearningDemo.Models.Services.Application;
using ElearningDemo.Models.ViewModels;
using ELearningDemo.Models.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace elearningfake.Models.Services.Application
{
    public class EfCoreCorsiService : ICorsoService
    {
        private readonly ILogger<EfCoreCorsiService> logger;
        private readonly MyCourseDbContext dbContext;
        private readonly IOptionsMonitor<CoursesOptions> courseOptions;

        public EfCoreCorsiService(ILogger<EfCoreCorsiService> logger, MyCourseDbContext dbContext, IOptionsMonitor<CoursesOptions> courseOptions)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            this.courseOptions = courseOptions;
        }

        public async Task<List<CorsiViewModel>> GetCorsiAsync(string search)
        {
            search = search ?? "";
            IQueryable<CorsiViewModel> queryLinq = dbContext.Courses
            .Where(course => course.Title.Contains(search))
            .AsNoTracking()
            .Select(course => 
            new CorsiViewModel{
                Id = course.Id,
                Title = course.Title,
                Author = course.Author,
                ImagePath = course.ImagePath,
                Rating = course.Rating,
                FullPrice = course.FullPrice,
                CurrentPrice = course.CurrentPrice
            });

            List<CorsiViewModel> corsi = await queryLinq.ToListAsync();

            return corsi;
        }

        public async Task<CorsoDetailViewModel> GetCorsoAsync(int id)
        {
            CorsoDetailViewModel corsoDettaglio = await dbContext.Courses
                        .AsNoTracking()
                        .Where(course => course.Id == id) // EntityFramework Sanitizza da solo
                        .Select(course => new CorsoDetailViewModel{
                            Id = course.Id,
                            Title = course.Title,
                            Description = course.Description,
                            ImagePath = course.ImagePath,
                            Author = course.Author,
                            Rating = course.Rating,
                            FullPrice = course.FullPrice,
                            CurrentPrice = course.CurrentPrice,
                            Lessons = course.Lessons.Select(lesson => new LezioneViewModel{
                                Id = lesson.Id,
                                Title = lesson.Title,
                                Description = lesson.Description,
                                Duration = lesson.Duration                             
                            }).ToList()
                        })
                        // .FirstOrDefaultAsync() // Restituisce null se vuoto, non solleva mai un'eccezione
                        //.SingleOrDefaultAsync() // Restituisce il primo elemento dell'elenco. Se l'elenco contiene >1 solleva un'eccezione. Se vuoto restituisce null (o il default del tipo)
                        //.FirstAsync() // Restituisce il primo elemento dell'elenco. Se l'elenco è vuoto, solleva un'eccezione
                        .SingleAsync(); //Restituisce il primo elemento dell'elenco. Se l'elenco contiene 0 o >1 solleva un'eccezione
            return corsoDettaglio;
        }
    }
}