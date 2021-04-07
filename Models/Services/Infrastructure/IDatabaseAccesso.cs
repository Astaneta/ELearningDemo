using System;
using System.Data;
using System.Threading.Tasks;

namespace Elearningfake.Models.Services.Infrastructure
{
    public interface IDatabaseAccesso
    {
        Task<DataSet> QueryAsync(FormattableString query);
    }
}