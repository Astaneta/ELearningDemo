using System;
using System.Data;
using System.Threading.Tasks;

namespace ElearningDemo.Models.Services.Infrastructure
{
    public interface IDatabaseAccesso
    {
        Task<DataSet> QueryAsync(FormattableString query);
    }
}