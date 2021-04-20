using System;
using System.Threading.Tasks;
using ElearningDemo.Models.Options;
using ELearningDemo.Models.InputModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace ELearningDemo.Customization.ModelBinders
{
    public class CorsiListaInputModelBinder : IModelBinder
    {
        private readonly IOptionsMonitor<CoursesOptions> courseOption;
        public CorsiListaInputModelBinder(IOptionsMonitor<CoursesOptions> courseOption)
        {
            this.courseOption = courseOption;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            // Recuperiamo i valori grazie ai value provider
            string search = bindingContext.ValueProvider.GetValue("Search").FirstValue;
            string orderBy = bindingContext.ValueProvider.GetValue("OrderBy").FirstValue;
            int.TryParse(bindingContext.ValueProvider.GetValue("page").FirstValue, out int page);
            bool.TryParse(bindingContext.ValueProvider.GetValue("ascending").FirstValue, out bool ascending);


            // Creiamo l'istanza di CorsiListaInputModel
            var option = courseOption.CurrentValue;
            var inputModel = new CorsiListaInputModel(search, page, orderBy, ascending, option.PerPagina, option.Order);

            // Impostiamo il risultato per notificare che la conversione è avvenuta con successo
            bindingContext.Result = ModelBindingResult.Success(inputModel);

            // Restituiamo un Task completato
            return Task.CompletedTask;
        }
    }
}