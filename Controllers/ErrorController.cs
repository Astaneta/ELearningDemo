using System;
using ELearningFake.Models.Services.Application;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ELearningFake.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            IExceptionHandlerPathFeature feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            
        }        
    }
}