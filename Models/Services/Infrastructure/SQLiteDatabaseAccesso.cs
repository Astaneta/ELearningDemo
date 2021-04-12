using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Elearningfake.Models.Options;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Elearningfake.Models.Services.Infrastructure
{
    public class SQLiteDatabaseAccesso : IDatabaseAccesso
    {
        private readonly ILogger<SQLiteDatabaseAccesso> logger;
        private readonly IOptionsMonitor<ConnectionStringsOptions> connectionStringsOption;

        public SQLiteDatabaseAccesso(ILogger<SQLiteDatabaseAccesso> logger, IOptionsMonitor<ConnectionStringsOptions> connectionStringsOption)
        {
            this.logger = logger;
            this.connectionStringsOption = connectionStringsOption;

        }
        public async Task<DataSet> QueryAsync(FormattableString formattableQuery)
        {

            logger.LogInformation(formattableQuery.Format, formattableQuery.GetArguments());

            var queryArguments = formattableQuery.GetArguments();
            var sqliteParameter = new List<SqliteParameter>();
            for (int i = 0; i < queryArguments.Length; i++)
            {
                var parameter = new SqliteParameter(i.ToString(), queryArguments[i]);
                sqliteParameter.Add(parameter);
                queryArguments[i] = "@" + i;
            }
            string query = formattableQuery.ToString();

            string connectionString = connectionStringsOption.CurrentValue.Default;
            using (var conn = new SqliteConnection(connectionString))
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