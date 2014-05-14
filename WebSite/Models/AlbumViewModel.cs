using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class AlbumViewModel
    {
        public Collection<Track> Tracks { get; set; }

        public int AlbumId { get; set; }
        public string ArtistName { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public FileInfo Thumbnail { get; set; }
    }

    public class Track
    {
        public int TrackId { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
    }
}