using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylShop.Shared.Features
{
    public class AlbumDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public int TimeInMinutes { get; set; }
        public string TimeFormatted => $"{TimeInMinutes / 60}h {TimeInMinutes % 60}m";
        public int Rating { get; set; }
        public IEnumerable<Song> Songs { get; set; } = new List<Song>();

        public class Song
        {
            public int Id { get; set; }
            public int? AlbumId { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Text { get; set; } = string.Empty;
        }
    }
}
