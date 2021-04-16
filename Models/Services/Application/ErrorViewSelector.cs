using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELearningFake.Models.Services.Application
{
    public class ErrorViewSelector
    {
        public void SelectView(IExceptionHandlerPathFeature feature)
        {
            switch(feature.Error)
            {
                case InvalidOperationException exc:
                    ViewData["Titolo"] = "Corso non trovato";
                    Response.StatusCode = 404;
                    return View("CorsoNonTrovato");

                default: 
                    ViewData["Titolo"] = "Errore";
                    return View();
            }
        }
    }
}