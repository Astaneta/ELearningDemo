using System.Data;

namespace Elearningfake.Models.Services.Infrastructure
{
    public interface IDatabaseAccesso
    {
        DataSet Query(string query);
    }
}