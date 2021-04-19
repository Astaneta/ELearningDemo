using ELearningDemo.Models.ViewData;
using Microsoft.AspNetCore.Diagnostics;

namespace ELearningDemo.Models.Services.Application
{
    public interface IErrorViewSelectorService
    {
        ErrorViewData GetError(IExceptionHandlerPathFeature exc);
    }
}