using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elearningfake.Models.Options;
using Elearningfake.Models.Services.Application;
using Elearningfake.Models.ViewModels;
using ELearningFake.Models.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace elearningfake.Models.Services.Application
{
    public class EfCoreCorsiService : ICorsoService
    {
        private readonly MioCorsoDbContext dbContext;
        private readonly IOptionsMonitor<CoursesOptions> courseOptions;

        public EfCoreCorsiService(MioCorsoDbContext dbContext, IOptionsMonitor<CoursesOptions> courseOptions)
        {
            this.dbContext = dbContext;
            this.courseOptions = courseOptions;
        }

        public async Task<List<CorsiViewModel>> GetCorsiAsync()
        {
            IQueryable<CorsiViewModel> queryLinq = dbContext.Corsi
            .AsNoTracking()
            .Select(corso => 
            new CorsiViewModel{
                Id = corso.Id,
                Title = corso.Titolo,
                Author = corso.Autore,
                ImagePath = corso.ImagePath,
                Rating = corso.Rating,
                FullPrice = corso.PrezzoPieno,
                CurrentPrice = corso.PrezzoCorrente
            });

            List<CorsiViewModel> corsi = await queryLinq.ToListAsync();

            return corsi;
        }

        public async Task<CorsoDetailViewModel> GetCorsoAsync(int id)
        {
            CorsoDetailViewModel corsoDettaglio = await dbContext.Corsi
                        .AsNoTracking()
                        .Where(corso => corso.Id == id) // EntityFramework Sanitizza da solo
                        .Select(corso => new CorsoDetailViewModel{
                            Id = corso.Id,
                            Title = corso.Titolo,
                            Descrizione = corso.Descrizione,
                            ImagePath = corso.ImagePath,
                            Author = corso.Autore,
                            Rating = corso.Rating,
                            FullPrice = corso.PrezzoPieno,
                            CurrentPrice = corso.PrezzoCorrente,
                            Lezioni = corso.Lezioni.Select(lezione => new LezioneViewModel{
                                Id = lezione.Id,
                                Titolo = lezione.Titolo,
                                Descrizione = lezione.Descrizione,
                                Durata = lezione.Durata                              
                            }).ToList()
                        })
                        // .FirstOrDefaultAsync() // Restituisce null se vuoto, non solleva mai un'eccezione
                        //.SingleOrDefaultAsync() // Restituisce il primo elemento dell'elenco. Se l'elenco contiene >1 solleva un'eccezione. Se vuoto restituisce null (o il default del tipo)
                        //.FirstAsync() // Restituisce il primo elemento dell'elenco. Se l'elenco è vuoto, solleva un'eccezione
                        .SingleAsync(); //Restituisce il primo elemento dell'elenco. Se l'elenco contiene 0 o >1 solleva un'eccezione
            return corsoDettaglio;
        }
    }
}