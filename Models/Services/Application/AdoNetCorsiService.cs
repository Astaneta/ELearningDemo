using System.Collections.Generic;
using System.Data;
using Elearningfake.Models.Services.Infrastructure;
using Elearningfake.Models.ViewModels;

namespace Elearningfake.Models.Services.Application
{
    public class AdoNetCorsiService : ICorsoService
    {
        private readonly IDatabaseAccesso db;

        public AdoNetCorsiService(IDatabaseAccesso db)
        {
            this.db = db;
        }
        public List<CorsiViewModel> GetCorsi()
        {
            string query = "SELECT Id, Titolo, ImagePath, Autore, Rating, PrezzoPieno_Cifra, PrezzoPieno_Valuta, PrezzoCorrente_Cifra, PrezzoCorrente_Valuta FROM Corsi";
            DataSet dataSet = db.Query(query);
            var dataTable = dataSet.Tables[0];
            var corsiLista = new List<CorsiViewModel>();
            foreach (DataRow item in dataTable.Rows)
            {
                CorsiViewModel corsi = CorsiViewModel.FromDataRow(item);
                corsiLista.Add(corsi);
            }
            return corsiLista;
        }

        public CorsoDetailViewModel GetCorso(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}