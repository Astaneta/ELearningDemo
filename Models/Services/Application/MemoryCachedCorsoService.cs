using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElearningDemo.Models.Options;
using ElearningDemo.Models.Services.Application;
using ElearningDemo.Models.ViewModels;
using ELearningDemo.Models.InputModels;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ELearningDemo.Models.Services.Application
{
    public class MemoryCachedCorsoService : ICachedCorsoService
    {
        private readonly ICorsoService corsoService;
        private readonly IMemoryCache memoryCache;
        private readonly IConfiguration configurationOption;
        private readonly IOptionsMonitor<CachedOption> cachedOption;

        public MemoryCachedCorsoService(ICorsoService corsoService, IMemoryCache memoryCache, IConfiguration configurationOption, IOptionsMonitor<CachedOption> cachedOption)
        {
            this.cachedOption = cachedOption;
            this.configurationOption = configurationOption;
            this.memoryCache = memoryCache;
            this.corsoService = corsoService;
        }
        
        public Task<List<CorsiViewModel>> GetCorsiAsync(CorsiListaInputModel model)
        {

            // Vengono messe in cache solo le prime 5 pagine in quanto sono le più visitate e viene sfruttata la cache solo se l'utente non ha cercato nulla
            bool canCache = model.Page <= 5 && string.IsNullOrWhiteSpace(model.Search);

            // Se canCache è true, usa il servizio di caching
            if (canCache)
            {
                double cachedMaxtime = configurationOption.GetSection("CachedTime").GetValue<double>("TimeSpanCorsi");
                return memoryCache.GetOrCreateAsync($"Corsi {model.Search}-{model.Page}-{model.OrderBy}-{model.Ascending}", cacheEntry =>
                {
                    //cacheEntry.SetSize(1);
                    cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(cachedMaxtime));
                    return corsoService.GetCorsiAsync(model);
                });
            }

            // Se canCache è false, usa direttamente l'applicazione di servizio
            return corsoService.GetCorsiAsync(model);
            
        }

        public Task<CorsoDetailViewModel> GetCorsoAsync(int id)
        {
            double cachedMaxTime = cachedOption.CurrentValue.TimeSpanDettaglioCorso;
            return memoryCache.GetOrCreateAsync($"Corso {id}", cacheEntry =>
            {
                //cacheEntry.SetSize(1);
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(cachedMaxTime));
                return corsoService.GetCorsoAsync(id);
            });
        }
    }
}