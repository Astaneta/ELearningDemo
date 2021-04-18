using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Elearningfake.Models.Options;
using Elearningfake.Models.Services.Infrastructure;
using Elearningfake.Models.ViewModels;
using ELearningFake.Models.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Elearningfake.Models.Services.Application
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
        public async Task<List<CorsiViewModel>> GetCorsiAsync()
        {

            logger.LogInformation("Corsi richiesti");

            FormattableString query = $"SELECT Id, Title, ImagePath, Author, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency FROM Courses";
            DataSet dataSet = await db.QueryAsync(query);
            var dataTable = dataSet.Tables[0];
            var corsiLista = new List<CorsiViewModel>();
            foreach (DataRow item in dataTable.Rows)
            {
                CorsiViewModel corsi = CorsiViewModel.FromDataRow(item);
                corsiLista.Add(corsi);
            }
            return corsiLista;
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
    }
}