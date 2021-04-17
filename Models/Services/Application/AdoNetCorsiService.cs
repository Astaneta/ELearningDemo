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

            FormattableString query = $"SELECT Id, Titolo, ImagePath, Autore, Rating, PrezzoPieno_Cifra, PrezzoPieno_Valuta, PrezzoCorrente_Cifra, PrezzoCorrente_Valuta FROM Corsi";
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
            
            FormattableString query = $@"SELECT Id, Titolo, Descrizione, ImagePath, Autore, Rating, PrezzoPieno_Cifra, PrezzoPieno_Valuta, PrezzoCorrente_Cifra, PrezzoCorrente_Valuta FROM Corsi WHERE Id={id};
            SELECT Id, CorsoId, Titolo, Descrizione, Durata FROM Lezioni WHERE CorsoId={id}";

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
                corsoDetailViewModel.Lezioni.Add(lezioneViewModel);
            }
            return corsoDetailViewModel;
        }
    }
}