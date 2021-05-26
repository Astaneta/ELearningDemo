using System;
using System.Collections.Generic;
using ElearningDemo.Models.ViewModels;
using ElearningDemo.Models.Enums;
using ElearningDemo.Models.ValueType;
using System.Threading.Tasks;
using ElearningDemo.Models.InputModels;
using ELearningDemo.Models.InputModels;

namespace ElearningDemo.Models.Services.Application
{
    public class CourseService : ICourseService
    {
        public Task<CourseDetailViewModel> CreateCourseAsync(CourseCreateInputModel inputModel)
        {
            throw new NotImplementedException();
        }

        public Task<List<CoursesViewModel>> GetBestCourseAsync()
        {
            throw new NotImplementedException();
        }

        public List<CoursesViewModel> GetCourse()
        {
            var CoursesList = new List<CoursesViewModel>();
            var rand = new Random();
            for (int i = 1; i <= 20; i++)
            {
                var price = Convert.ToDecimal(rand.NextDouble() * 10 + 10);
                var corsi = new CoursesViewModel
                {
                    Id = i,
                    Title = $"Course {i}",
                    CurrentPrice = new Money(Currency.EUR, price),
                    FullPrice = new Money(Currency.EUR, (rand.NextDouble() * 10) > 5 ? price : price +2),
                    Author = "Nome&Cognome",
                    Rating = rand.NextDouble() * 5.0,
                    ImagePath = "~/Free_logo.svg"
                };
                CoursesList.Add(corsi);
            }
            return CoursesList;
        }

        public CourseDetailViewModel GetCourse(int id)
        {
            var rand = new Random();
            var price = Convert.ToDecimal(rand.NextDouble() * 10 + 10);
            var corso = new CourseDetailViewModel
            {
                Id = id,
                Title = $"Course {id}",
                CurrentPrice = new Money(Currency.EUR, price),
                FullPrice = new Money(Currency.EUR, (rand.NextDouble() * 10) > 5 ? price : price +2),
                Author = "Nome&Cognome",
                Rating = rand.NextDouble() * 5,
                ImagePath = "~/Free_logo.svg",
                Description = "Qui ci va la descrizione",
                Lessons = new List<LessonViewModel>()
            };

            for (int i = 1; i <= 5; i++)
            {
                var lezioni = new LessonViewModel{
                    Title = $"Lezione {i}",
                    Duration = TimeSpan.FromSeconds(rand.Next(40, 90))
                };
                corso.Lessons.Add(lezioni);
            }

            return corso;
        }

        public Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ListViewModel<CoursesViewModel>> GetCoursesAsync(CoursesListInputModel model)
        {
            throw new NotImplementedException();
        }

        public Task<List<CoursesViewModel>> GetMostRecentCourseAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsTitleAvailableAsync(string title)
        {
            throw new NotImplementedException();
        }
    }
}