using Nwd.BackOffice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Areas.Admin.ViewModels
{
    public class AlbumViewListModel
    {
        public ICollection<Album> Albums {get; set;}
    }
}