using Nwd.BackOffice.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Areas.Admin.ViewModels
{
    public class AlbumViewModel
    {
        public int Id { get; set; }

        [Required]
        [Remote("IsTitleAvailable", "Album")]
        public string Title { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        public string Type { get; set; }

        public string ImagePath { get; set; }

        [Required]
        public string ArtistName { get; set; }

        public ICollection<Track> Tracks { get; set; }
    }
}