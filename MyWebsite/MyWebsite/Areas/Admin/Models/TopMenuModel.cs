using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyWebsite.Domain;

namespace MyWebsite.Areas.Admin.Models
{
    public class TopMenuModel
    {
        public List<MenuResult> Menus { get; set; }
    }
}