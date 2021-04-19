using System;
using System.Net;
using ELearningDemo.Models.Exceptions;
using ELearningDemo.Models.ViewData;
using Microsoft.AspNetCore.Diagnostics;

namespace ELearningDemo.Models.Services.Application
{
    public class ErrorViewSelectorService : IErrorViewSelectorService
    {
        public ErrorViewData GetError(IExceptionHandlerPathFeature error)
        {
            switch(error?.Error)
            {
                case null:
                    return new ErrorViewData{
                        Title = $"Pagina {error.Path} non trovata",
                        StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                        ViewReturn = "CorsoNonTrovato"
                    };
                case CorsoNonTrovatoException p:
                        return new ErrorViewData{
                        Title = "Corso Non Trovato",
                        StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                        ViewReturn = "CorsoNonTrovato"
                    };
                default:
                        return new ErrorViewData{
                        Title = "Errore",
                        StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                        ViewReturn = "Index"
                    };
            }
        }
    }
}