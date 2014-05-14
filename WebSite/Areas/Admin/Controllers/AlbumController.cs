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
        public ActionResult Create(AlbumViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = new AlbumRepository();
                var album = new Album();

                UpdateAlbumFromViewModel(model, album);

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
        public ActionResult Edit(int id, AlbumViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = new AlbumRepository();
                var album = repo.GetAlbumForEdit(id);

                UpdateAlbumFromViewModel(model, album);

                repo.EditAlbum(this.Server, album);
                return RedirectToAction("List");
            }

            // something failed if we get here, so display edition page
            return View(model);
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

            album.ReleaseDate = DateTime.UtcNow;
        }

        private void UpdateViewModelFromAlbum(AlbumViewModel model, Album album)
        {
            model.Id = album.Id;
            model.Title = album.Title;
            model.Duration = album.Duration;
            model.Type = album.Type;
        }

        #endregion
    }
}
