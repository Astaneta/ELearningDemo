using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElearningDemo.Models.Options;
using ElearningDemo.Models.Services.Application;
using ElearningDemo.Models.ViewModels;
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
        
        public Task<List<CorsiViewModel>> GetCorsiAsync(string search, int page, string orderBy, bool ascending)
        {
            double cachedMaxtime = configurationOption.GetSection("CachedTime").GetValue<double>("TimeSpanCorsi");
            return memoryCache.GetOrCreateAsync($"Corsi {search}-{page}-{orderBy}-{ascending}", cacheEntry =>
            {
                //cacheEntry.SetSize(1);
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(cachedMaxtime));
                return corsoService.GetCorsiAsync(search, page, orderBy, ascending);
            });
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