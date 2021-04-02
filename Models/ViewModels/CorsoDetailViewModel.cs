using System.Collections.Generic;

namespace Elearningfake.Models.ViewModels
{
    public class CorsoDetailViewModel : CorsiViewModel
    {
        public string Descrizione { get; set; }
        public List<LezioneViewModel> Lezioni {get; set;}
    }
}