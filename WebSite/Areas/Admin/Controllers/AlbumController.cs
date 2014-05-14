using Nwd.BackOffice.Impl;
using Nwd.BackOffice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebSite.Areas.Admin.ViewModels;

namespace WebSite.Areas.Admin.Controllers
{
    [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
    public class AlbumController : Controller
    {
        //
        // GET: /Album/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var viewModel = new AlbumViewListModel();
            viewModel.Albums = new AlbumRepository().GetAllAlbums();
            return View("AlbumListView", viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(AlbumViewModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var repo = new AlbumRepository();
                var album = new Album();

                UpdateAlbumFromViewModel(model, album);

                if (file != null && file.ContentLength > 0)
                {
                    album.CoverFile = file;
                }

                repo.CreateAlbum(album, this.Server);
                return RedirectToAction("List");
            }

            // something failed if we get here, so display creation page
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var repo = new AlbumRepository();
            var album = repo.GetAlbumForEdit(id);
            AlbumViewModel model = new AlbumViewModel();

            UpdateViewModelFromAlbum(model, album);
            
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(int id, AlbumViewModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var repo = new AlbumRepository();
                var album = repo.GetAlbumForEdit(id);

                UpdateAlbumFromViewModel(model, album);

                if (file != null && file.ContentLength > 0)
                {
                    album.CoverFile = file;
                }

                repo.EditAlbum(this.Server, album);
                return RedirectToAction("List");
            }

            // something failed if we get here, so display edition page
            return View(model);
        }

        [HttpGet]
        public ActionResult AddTrack()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTrack(AddTrackViewModel model)
        {
            return View();
        }

        [HttpGet]
        public JsonResult IsTitleAvailable(string Title)
        {
            var repo = new AlbumRepository();
            return Json(false, JsonRequestBehavior.AllowGet); 
        }

        #region private methods

        private void UpdateAlbumFromViewModel(AlbumViewModel model, Album album)
        {
            album.Id = model.Id;
            album.Title = model.Title;
            album.Duration = model.Duration;
            album.Type = model.Type;

            if (album.Artist == null)
            {
                album.Artist = new Artist();
            }
            album.Artist.Name = model.ArtistName;

            // fixes runtime error (release date not used in website)
            album.ReleaseDate = DateTime.UtcNow;
        }

        private void UpdateViewModelFromAlbum(AlbumViewModel model, Album album)
        {
            model.Id = album.Id;
            model.Title = album.Title;
            model.Duration = album.Duration;
            model.Type = album.Type;

            if (album.Artist == null)
            {
                album.Artist = new Artist();
            }
            
            model.ArtistName = album.Artist.Name;

            if (album.CoverImagePath != String.Empty)
            {
                model.ImagePath = album.CoverImagePath;
            }
        }

        #endregion
    }
}
