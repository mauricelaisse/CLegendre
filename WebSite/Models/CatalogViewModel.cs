using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class CatalogViewModel
    {
        public Collection<Album> Albums { get; set; }
    }

    public class Album
    {
        public int AlbumId { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Thumbnail { get; set; }
    }
}