using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;

namespace Elearningfake.Models.Services.Infrastructure
{
    public class SQLiteDatabaseAccesso : IDatabaseAccesso
    {
        public DataSet Query(FormattableString formattableQuery)
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

                conn.Open();
                using (var cmd = new SqliteCommand(query, conn))
                {
                    cmd.Parameters.AddRange(sqliteParameter);
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