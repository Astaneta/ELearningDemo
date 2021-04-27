using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElearningDemo.Models.Options;
using ElearningDemo.Models.Services.Application;
using ElearningDemo.Models.ViewModels;
using ELearningDemo.Models.Entities;
using ELearningDemo.Models.InputModels;
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

        public Task<List<CorsiViewModel>> GetBestCourseAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ListViewModel<CorsiViewModel>> GetCorsiAsync(CorsiListaInputModel input)
        {
            IQueryable<Course> baseQuery = dbContext.Courses; 

            switch(input.OrderBy)
            {
                case "Title":
                    if (input.Ascending)
                    {
                        baseQuery = baseQuery.OrderBy(course => course.Title);
                    }
                    else
                    {
                        baseQuery = baseQuery.OrderByDescending(course => course.Title);
                    }
                    break;
                case "Rating":
                    if (input.Ascending)
                    {
                        baseQuery = baseQuery.OrderBy(course => course.Rating);
                    }
                    else
                    {
                        baseQuery = baseQuery.OrderByDescending(course => course.Rating);
                    }
                    break;
                case "CurrentPrice":
                    if (input.Ascending)
                    {
                        baseQuery = baseQuery.OrderBy(course => course.CurrentPrice.Amount);
                    }
                    else
                    {
                        baseQuery = baseQuery.OrderByDescending(course => course.CurrentPrice.Amount);
                    }
                    break;
            }

            IQueryable<CorsiViewModel> queryLinq = baseQuery
            .Where(course => EF.Functions.Like(course.Title, $"%{input.Search}%")) //course.Title.Contains(input.Search)) now is case-insensitive
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

            List<CorsiViewModel> corsi = await queryLinq
                        .Skip(input.Offset)
                        .Take(input.Limit)
                        .ToListAsync(); //Questo è il punto dove la querylink viene eseguita
            int totalCount = await queryLinq.CountAsync();

            ListViewModel<CorsiViewModel> result = new ListViewModel<CorsiViewModel>();
            result.Result = corsi;
            result.TotalCount = totalCount;

            return result;
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

        public Task<List<CorsiViewModel>> GetMostRecentCourseAsync()
        {
            throw new NotImplementedException();
        }
    }
}