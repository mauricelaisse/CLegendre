using Nwd.BackOffice.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class CatalogController : Controller
    {
        //
        // GET: /Catalog/

        public ActionResult Index()
        {
            CatalogViewModel model = new CatalogViewModel();
            model.Albums = new Collection<WebSite.Models.Album>();

            using (var ctx = new NwdMusikEntities())
            {
                foreach ( var album in ctx.Albums.Include( "Artist" ) )
                {
                    string imagePath = album.CoverImagePath;
                    if (imagePath == null) imagePath = @"~\Mock\t.jpg";

                    model.Albums.Add(
                        new Models.Album { 
                            AlbumId = album.Id,
                            Artist = album.Artist.Name,
                            Thumbnail = new FileInfo( imagePath ),
                            Title = album.Title
                        });
                }
            }

            return View(model);
        }

        public ActionResult Album(int albumId)
        {
            //if(!AlbumExists(id)) return RedirectTo404

            AlbumViewModel model = new AlbumViewModel();
            model.Tracks = new Collection<Models.Track>();

            using (var ctx = new NwdMusikEntities())
            {
                var album = ctx.Albums.Include( "Tracks" ).Include( "Tracks.Song" ).Single( x => x.Id == albumId );

                string imagePath = album.CoverImagePath;
                if (imagePath == null) imagePath = @"~\Mock\t.jpg";

                model.AlbumId = album.Id;
                model.Title = album.Title;
                model.ArtistName = album.Artist.Name;
                model.ReleaseDate = album.ReleaseDate;
                model.Thumbnail = new FileInfo( imagePath );

                foreach (var track in album.Tracks)
                {
                    model.Tracks.Add( 
                        new Models.Track {
                        TrackId = track.Song_Id.Value,
                        Name = track.Song.Name,
                        Duration = track.Duration
                    });
                }
            }

            return View( model );
        }
    }
}
