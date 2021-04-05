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
            string query = "SELECT * FROM Corsi";
            DataSet corsi = db.Query(query);
            throw new System.NotImplementedException();
        }

        public CorsoDetailViewModel GetCorso(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}