using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ElearningDemo.Models.Options;
using ElearningDemo.Models.Services.Infrastructure;
using ElearningDemo.Models.ViewModels;
using ELearningDemo.Models.Exceptions;
using ELearningDemo.Models.InputModels;
using ELearningDemo.Models.ValueType;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ElearningDemo.Models.Services.Application
{
    public class AdoNetCorsiService : ICorsoService
    {
        private readonly ILogger<AdoNetCorsiService> logger;
        private readonly IDatabaseAccesso db;
        private readonly IOptionsMonitor<CoursesOptions> courseOption;

        public AdoNetCorsiService(ILogger<AdoNetCorsiService> logger, IDatabaseAccesso db, IOptionsMonitor<CoursesOptions> courseOption)
        {
            this.logger = logger;
            this.db = db;
            this.courseOption = courseOption;
        }

        public async Task<List<CorsiViewModel>> GetBestCourseAsync()
        {
            FormattableString query = $"SELECT Id, Title, ImagePath, Author, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency FROM Courses ORDER BY Rating DESC LIMIT 3";
            DataSet dataSet = await db.QueryAsync(query);
            var dataTable = dataSet.Tables[0];
            var CoursesList = new List<CorsiViewModel>();
            foreach (DataRow item in dataTable.Rows)
            {
                CorsiViewModel corsi = CorsiViewModel.FromDataRow(item);
                CoursesList.Add(corsi);
            }
            return CoursesList;
        }

        public async Task<ListViewModel<CorsiViewModel>> GetCorsiAsync(CoursesListInputModel input)
        {

            logger.LogInformation("Corsi richiesti");

            string orderBy = input.OrderBy == "CurrentPrice" ? "CurrentPrice_Amount" : input.OrderBy;
            string direction = input.Ascending ? "ASC" : "DESC";

            FormattableString query = $@"SELECT Id, Title, ImagePath, Author, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency FROM Courses WHERE Title LIKE {"%"+input.Search+"%"} ORDER BY {(Sql) orderBy} {(Sql) direction} LIMIT {input.Limit} OFFSET {input.Offset};
            SELECT COUNT(*) FROM Courses WHERE Title LIKE {"%"+input.Search+"%"}";
            DataSet dataSet = await db.QueryAsync(query);
            var dataTable = dataSet.Tables[0];
            var CoursesList = new List<CorsiViewModel>();
            foreach (DataRow item in dataTable.Rows)
            {
                CorsiViewModel corsi = CorsiViewModel.FromDataRow(item);
                CoursesList.Add(corsi);
            }
            int totalCount = Convert.ToInt32(dataSet.Tables[1].Rows[0][0]);
            ListViewModel<CorsiViewModel> result = new ListViewModel<CorsiViewModel>
            {
                Result = CoursesList,
                TotalCount = totalCount                
            };

            return result;
        }

        public async Task<CorsoDetailViewModel> GetCorsoAsync(int id)
        {

            logger.LogInformation("Corso {id} richiesto", id);
            
            FormattableString query = $@"SELECT Id, Title, Description, ImagePath, Author, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency FROM Courses WHERE Id={id};
            SELECT Id, CourseId, Title, Description, Duration FROM Lessons WHERE CourseId={id}";

            DataSet dataSet = await db.QueryAsync(query);

            //Corso
            var corsoTable = dataSet.Tables[0];
            if (corsoTable.Rows.Count != 1)
            {
                logger.LogWarning("Il corso {id} non esiste", id);
                throw new CorsoNonTrovatoException(id);
            }
            var corsoRow = corsoTable.Rows[0];
            var corsoDetailViewModel = CorsoDetailViewModel.FromDataRow(corsoRow);

            //Lezioni corso
            var lezioniDataTable = dataSet.Tables[1];
            foreach (DataRow item in lezioniDataTable.Rows)
            {
                LezioneViewModel lezioneViewModel = LezioneViewModel.FromDataRow(item);
                corsoDetailViewModel.Lessons.Add(lezioneViewModel);
            }
            return corsoDetailViewModel;
        }

        public async Task<List<CorsiViewModel>> GetMostRecentCourseAsync()
        {
            FormattableString query = $"SELECT Id, Title, ImagePath, Author, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency FROM Courses ORDER BY Id DESC LIMIT 3";
            DataSet dataSet = await db.QueryAsync(query);
            var dataTable = dataSet.Tables[0];
            var CoursesList = new List<CorsiViewModel>();
            foreach (DataRow item in dataTable.Rows)
            {
                CorsiViewModel corsi = CorsiViewModel.FromDataRow(item);
                CoursesList.Add(corsi);
            }
            return CoursesList;
        }
    }
}