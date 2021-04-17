using ELearningFake.Models.ViewData;
using Microsoft.AspNetCore.Diagnostics;

namespace ELearningFake.Models.Services.Application
{
    public interface IErrorViewSelectorService
    {
        ErrorViewData GetError(IExceptionHandlerPathFeature exc);
    }
}