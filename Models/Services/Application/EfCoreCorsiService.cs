using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elearningfake.Models.Services.Application;
using Elearningfake.Models.ViewModels;
using ELearningFake.Models.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace elearningfake.Models.Services.Application
{
    public class EfCoreCorsiService : ICorsoService
    {
        private readonly MioCorsoDbContext dbContext;

        public EfCoreCorsiService(MioCorsoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<CorsiViewModel>> GetCorsiAsync()
        {
            List<CorsiViewModel> corsi = await dbContext.Corsi.Select(corso => 
            new CorsiViewModel{
                Id = corso.Id,
                Title = corso.Titolo,
                Author = corso.Autore,
                ImagePath = corso.ImagePath,
                Rating = corso.Rating,
                FullPrice = corso.PrezzoPieno,
                CurrentPrice = corso.PrezzoCorrente
            })
            .ToListAsync();

            return corsi;
        }

        public Task<CorsoDetailViewModel> GetCorsoAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}