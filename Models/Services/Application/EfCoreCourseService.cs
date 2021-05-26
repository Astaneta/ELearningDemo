using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElearningDemo.Models.InputModels;
using ElearningDemo.Models.Options;
using ElearningDemo.Models.Services.Application;
using ElearningDemo.Models.ViewModels;
using ELearningDemo.Models.Entities;
using ELearningDemo.Models.InputModels;
using ELearningDemo.Models.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ElearningDemo.Models.Services.Application
{
    public class EfCoreCourseService : ICourseService
    {
        private readonly ILogger<EfCoreCourseService> logger;
        private readonly MyCourseDbContext dbContext;
        private readonly IOptionsMonitor<CoursesOptions> courseOptions;

        public EfCoreCourseService(ILogger<EfCoreCourseService> logger, MyCourseDbContext dbContext, IOptionsMonitor<CoursesOptions> courseOptions)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            this.courseOptions = courseOptions;
        }

        public async Task<List<CoursesViewModel>> GetBestCourseAsync()
        {
            IQueryable<CoursesViewModel> query = dbContext.Courses
                                    .OrderByDescending(course => course.Rating)
                                    .Take(3)
                                    .AsNoTracking()
                                    .Select(course =>
                                    new CoursesViewModel
                                    {
                                        Id = course.Id,
                                        Title = course.Title,
                                        Author = course.Author,
                                        ImagePath = course.ImagePath,
                                        Rating = course.Rating,
                                        CurrentPrice = course.CurrentPrice,
                                        FullPrice = course.FullPrice
                                    });
            List<CoursesViewModel> result = await query.ToListAsync();

            return result;
        }

        public async Task<ListViewModel<CoursesViewModel>> GetCoursesAsync(CoursesListInputModel input)
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

            IQueryable<CoursesViewModel> queryLinq = baseQuery
            .Where(course => EF.Functions.Like(course.Title, $"%{input.Search}%")) //course.Title.Contains(input.Search)) now is case-insensitive
            .AsNoTracking()
            .Select(course => 
            new CoursesViewModel{
                Id = course.Id,
                Title = course.Title,
                Author = course.Author,
                ImagePath = course.ImagePath,
                Rating = course.Rating,
                FullPrice = course.FullPrice,
                CurrentPrice = course.CurrentPrice
            });

            List<CoursesViewModel> corsi = await queryLinq
                        .Skip(input.Offset)
                        .Take(input.Limit)
                        .ToListAsync(); //Questo è il punto dove la querylink viene eseguita
            int totalCount = await queryLinq.CountAsync();

            ListViewModel<CoursesViewModel> result = new ListViewModel<CoursesViewModel>();
            result.Result = corsi;
            result.TotalCount = totalCount;

            return result;
        }

        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            CourseDetailViewModel corsoDettaglio = await dbContext.Courses
                        .AsNoTracking()
                        .Where(course => course.Id == id) // EntityFramework Sanitizza da solo
                        .Select(course => new CourseDetailViewModel{
                            Id = course.Id,
                            Title = course.Title,
                            Description = course.Description,
                            ImagePath = course.ImagePath,
                            Author = course.Author,
                            Rating = course.Rating,
                            FullPrice = course.FullPrice,
                            CurrentPrice = course.CurrentPrice,
                            Lessons = course.Lessons.Select(lesson => new LessonViewModel{
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

        public async Task<List<CoursesViewModel>> GetMostRecentCourseAsync()
        {
            IQueryable<CoursesViewModel> query = dbContext.Courses
                                    .OrderByDescending(course => course.Id)
                                    .Take(3)
                                    .AsNoTracking()
                                    .Select(course =>
                                    new CoursesViewModel
                                    {
                                        Id = course.Id,
                                        Title = course.Title,
                                        Author = course.Author,
                                        ImagePath = course.ImagePath,
                                        Rating = course.Rating,
                                        CurrentPrice = course.CurrentPrice,
                                        FullPrice = course.FullPrice
                                    });
            List<CoursesViewModel> result = await query.ToListAsync();

            return result;
        }

        public async Task<CourseDetailViewModel> CreateCourseAsync(CourseCreateInputModel inputModel)
        {
            //TODO
            var course = new Course(inputModel.Title, "Mario Rossi");
            dbContext.Add(course);
            await dbContext.SaveChangesAsync();

            return CourseDetailViewModel.FromEntity(course);
        }

        public async Task<bool> IsTitleAvailableAsync(string title)
        {
            bool titleExist = await dbContext.Courses.AnyAsync(course => EF.Functions.Like(course.Title, title));
            
            // Ritorna il contrario perché true indica che il titolo è disponibile, invece che già esistente
            return !titleExist;
        }
    }
}