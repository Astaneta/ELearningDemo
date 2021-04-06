using System;
using System.Data;

namespace Elearningfake.Models.Services.Infrastructure
{
    public interface IDatabaseAccesso
    {
        DataSet Query(FormattableString query);
    }
}