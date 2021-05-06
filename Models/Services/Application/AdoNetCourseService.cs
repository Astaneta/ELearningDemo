using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using elearningdemo.Models.Exceptions;
using ElearningDemo.Models.InputModels;
using ElearningDemo.Models.Options;
using ElearningDemo.Models.Services.Infrastructure;
using ElearningDemo.Models.ViewModels;
using ELearningDemo.Models.Exceptions;
using ELearningDemo.Models.InputModels;
using ELearningDemo.Models.ValueType;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ElearningDemo.Models.Services.Application
{
    public class AdoNetCourseService : ICourseService
    {
        private readonly ILogger<AdoNetCourseService> logger;
        private readonly IDatabaseAccesso db;
        private readonly IOptionsMonitor<CoursesOptions> courseOption;

        public AdoNetCourseService(ILogger<AdoNetCourseService> logger, IDatabaseAccesso db, IOptionsMonitor<CoursesOptions> courseOption)
        {
            this.logger = logger;
            this.db = db;
            this.courseOption = courseOption;
        }

        public async Task<CourseDetailViewModel> CreateCourseAsync(CourseCreateInputModel inputModel)
        {
            string title = inputModel.Title;
            string author = "Mario Rossi";

            try{
            FormattableString query = $@"INSERT INTO Courses (Title, Author, ImagePath, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency) VALUES ({title},{author}, '/Courses/default.png', 0, 'EUR', 0, 'EUR'); 
            SELECT last_insert_rowid()";
            DataSet dataset = await db.QueryAsync(query);

            int courseId = Convert.ToInt32(dataset.Tables[0].Rows[0][0]);
            CourseDetailViewModel course = await GetCourseAsync(courseId);
            return course;
            }
            catch (SqliteException exc) when (exc.SqliteErrorCode == 19)
            {
                throw new CourseTitleNotAvalaibleException(exc);
            }
        }

        public async Task<List<CoursesViewModel>> GetBestCourseAsync()
        {
            FormattableString query = $"SELECT Id, Title, ImagePath, Author, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency FROM Courses ORDER BY Rating DESC LIMIT 3";
            DataSet dataSet = await db.QueryAsync(query);
            var dataTable = dataSet.Tables[0];
            var CoursesList = new List<CoursesViewModel>();
            foreach (DataRow item in dataTable.Rows)
            {
                CoursesViewModel corsi = CoursesViewModel.FromDataRow(item);
                CoursesList.Add(corsi);
            }
            return CoursesList;
        }

        public async Task<ListViewModel<CoursesViewModel>> GetCoursesAsync(CoursesListInputModel input)
        {

            logger.LogInformation("Course richiesti");

            string orderBy = input.OrderBy == "CurrentPrice" ? "CurrentPrice_Amount" : input.OrderBy;
            string direction = input.Ascending ? "ASC" : "DESC";

            FormattableString query = $@"SELECT Id, Title, ImagePath, Author, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency FROM Courses WHERE Title LIKE {"%"+input.Search+"%"} ORDER BY {(Sql) orderBy} {(Sql) direction} LIMIT {input.Limit} OFFSET {input.Offset};
            SELECT COUNT(*) FROM Courses WHERE Title LIKE {"%"+input.Search+"%"}";
            DataSet dataSet = await db.QueryAsync(query);
            var dataTable = dataSet.Tables[0];
            var CoursesList = new List<CoursesViewModel>();
            foreach (DataRow item in dataTable.Rows)
            {
                CoursesViewModel corsi = CoursesViewModel.FromDataRow(item);
                CoursesList.Add(corsi);
            }
            int totalCount = Convert.ToInt32(dataSet.Tables[1].Rows[0][0]);
            ListViewModel<CoursesViewModel> result = new ListViewModel<CoursesViewModel>
            {
                Result = CoursesList,
                TotalCount = totalCount                
            };

            return result;
        }

        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {

            logger.LogInformation("Course {id} richiesto", id);
            
            /*FormattableString query = $@"SELECT Id, Title, Description, ImagePath, Author, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency FROM Courses WHERE Id={id};
            SELECT Id, CourseId, Title, Description, Duration FROM Lessons WHERE CourseId={id}";*/

            DataSet dataSet = await db.QueryAsync($@"SELECT Id, Title, Description, ImagePath, Author, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency FROM Courses WHERE Id={id};
            SELECT Id, CourseId, Title, Description, Duration FROM Lessons WHERE CourseId={id}");

            //Course
            var corsoTable = dataSet.Tables[0];
            if (corsoTable.Rows.Count != 1)
            {
                logger.LogWarning("Il corso {id} non esiste", id);
                throw new CourseNotFoundException(id);
            }
            var corsoRow = corsoTable.Rows[0];
            var corsoDetailViewModel = CourseDetailViewModel.FromDataRow(corsoRow);

            //Lezioni corso
            var lezioniDataTable = dataSet.Tables[1];
            foreach (DataRow item in lezioniDataTable.Rows)
            {
                LessonViewModel lezioneViewModel = LessonViewModel.FromDataRow(item);
                corsoDetailViewModel.Lessons.Add(lezioneViewModel);
            }
            return corsoDetailViewModel;
        }

        public async Task<List<CoursesViewModel>> GetMostRecentCourseAsync()
        {
            FormattableString query = $"SELECT Id, Title, ImagePath, Author, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency FROM Courses ORDER BY Id DESC LIMIT 3";
            DataSet dataSet = await db.QueryAsync(query);
            var dataTable = dataSet.Tables[0];
            var CoursesList = new List<CoursesViewModel>();
            foreach (DataRow item in dataTable.Rows)
            {
                CoursesViewModel corsi = CoursesViewModel.FromDataRow(item);
                CoursesList.Add(corsi);
            }
            return CoursesList;
        }
    }
}