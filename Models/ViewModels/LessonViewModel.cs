using System;
using System.Data;

namespace ElearningDemo.Models.ViewModels
{
    public class LessonViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }

        internal static LessonViewModel FromDataRow(DataRow item)
        {
            LessonViewModel lezioneVM = new LessonViewModel{
                Id = Convert.ToInt32(item["Id"]),
                Title = Convert.ToString(item["Title"]),
                Description = Convert.ToString(item["Description"]),
                Duration = TimeSpan.Parse(Convert.ToString(item["Duration"]))
            };
            return lezioneVM;
        }
    }
}