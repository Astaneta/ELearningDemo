using System.Data;
using Microsoft.Data.Sqlite;

namespace Elearningfake.Models.Services.Infrastructure
{
    public class SQLiteDatabaseAccesso : IDatabaseAccesso
    {
        public DataSet Query(string query)
        {
            using (var conn = new SqliteConnection("Data Source = Data/MioCorso.db"))
            {
                conn.Open();
                using (var cmd = new SqliteCommand(query, conn))
                {
                   using (var reader = cmd.ExecuteReader())
                   {
                       var dataSet = new DataSet();
                       do{
                            var dataTable = new DataTable();
                            dataSet.Tables.Add(dataTable);
                            dataTable.Load(reader);
                       } while (!reader.IsClosed);
                       
                       return dataSet;
                   }
                } 
            }
        }
    }
}