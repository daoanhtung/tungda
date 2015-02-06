using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWebsite.Business;
using MyWebsite.Business.Implement;
using MyWebsite.Domain;
using MyWebsite.Models;
using MyWebsite.Utils;

namespace MyWebsite.Controllers
{
    public class BottomMenuController : Controller
    {
        //
        // GET: /BottomMenu/
        private readonly IBottomMenuManager _bottomMenuManager;
        public BottomMenuController(IBottomMenuManager bottomMenuManager)
        {
            _bottomMenuManager = bottomMenuManager;
        }

        public BottomMenuController()
        {
            _bottomMenuManager = new BottomMenuManager();
        }

        private BottomMenuViewModel Entity
        {
            get
            {
                if (Session[Constants.BottomMenuSessionKey] == null)
                {
                    Session[Constants.BottomMenuSessionKey] = new BottomMenuViewModel
                    {
                        Menus = _bottomMenuManager.SelectActive().ToList()
                    };
                }
                else
                {
                    Session[Constants.BottomMenuSessionKey] = new BottomMenuViewModel
                    {
                        Menus = _bottomMenuManager.SelectActive().ToList()
                    };
                }

                return (BottomMenuViewModel)Session[Constants.BottomMenuSessionKey];
            }
        }

        public ActionResult Index()
        {
            return PartialView(Entity);
        }

    }
}
