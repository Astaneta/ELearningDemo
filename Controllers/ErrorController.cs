using System;
using ELearningFake.Models.Services.Application;
using ELearningFake.Models.ViewData;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ELearningFake.Controllers
{
    public class ErrorController : Controller
    {
        private readonly IErrorViewSelectorService error;

        public ErrorController(IErrorViewSelectorService error)
        {
            this.error = error;
        }
        public IActionResult Index()
        {
            ErrorViewData data = error.GetError(HttpContext.Features.Get<IExceptionHandlerPathFeature>());
            ViewData["Titolo"] = data.Title;
            Response.StatusCode = data.StatusCode;
            return View(data.ViewReturn);
        }
    }
}