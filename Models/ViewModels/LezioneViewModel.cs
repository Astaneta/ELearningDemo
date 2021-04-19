using System;
using System.Data;

namespace ElearningDemo.Models.ViewModels
{
    public class LezioneViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }

        internal static LezioneViewModel FromDataRow(DataRow item)
        {
            LezioneViewModel lezioneVM = new LezioneViewModel{
                Id = Convert.ToInt32(item["Id"]),
                Title = Convert.ToString(item["Title"]),
                Description = Convert.ToString(item["Description"]),
                Duration = TimeSpan.Parse(Convert.ToString(item["Duration"]))
            };
            return lezioneVM;
        }
    }
}