using Nwd.BackOffice.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Controllers
{
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
            AlbumRepository repo = new AlbumRepository();
            HttpServerUtilityBase server = Mock.Of<HttpServerUtilityBase>();
            var albums = repo.GetAllAlbums();
            //AlbumListViewModel
        }
    }
}
