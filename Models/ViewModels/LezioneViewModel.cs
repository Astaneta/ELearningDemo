using System;
using System.Data;

namespace Elearningfake.Models.ViewModels
{
    public class LezioneViewModel
    {
        public int Id { get; set; }
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public TimeSpan Durata { get; set; }

        internal static LezioneViewModel FromDataRow(DataRow item)
        {
            LezioneViewModel lezioneVM = new LezioneViewModel{
                Id = Convert.ToInt32(item["Id"]),
                Titolo = Convert.ToString(item["Titolo"]),
                Descrizione = Convert.ToString(item["Descrizione"]),
                Durata = TimeSpan.Parse(Convert.ToString(item["Durata"]))
            };
            return lezioneVM;
        }
    }
}