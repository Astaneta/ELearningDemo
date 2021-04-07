using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace Elearningfake.Models.Services.Infrastructure
{
    public class SQLiteDatabaseAccesso : IDatabaseAccesso
    {
        public async Task<DataSet> QueryAsync(FormattableString formattableQuery)
        {

            var queryArguments = formattableQuery.GetArguments();
            var sqliteParameter = new List<SqliteParameter>();
            for (int i = 0; i < queryArguments.Length; i++)
            {
                var parameter = new SqliteParameter(i.ToString(), queryArguments[i]);
                sqliteParameter.Add(parameter);
                queryArguments[i] = "@" + i;
            }
            string query = formattableQuery.ToString();

            using (var conn = new SqliteConnection("Data Source = Data/MioCorso.db"))
            {

                await conn.OpenAsync();
                using (var cmd = new SqliteCommand(query, conn))
                {
                    cmd.Parameters.AddRange(sqliteParameter);
                    using (var reader = await cmd.ExecuteReaderAsync())
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