using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWebsite.Business;
using MyWebsite.Business.Implement;
using MyWebsite.Domain;
using MyWebsite.Models;
using MyWebsite.Resx;
using MyWebsite.Utils;

namespace MyWebsite.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        //
        // GET: /Admin/Menu/

        private readonly IAdminMenuManager _adminMenuManager;
        public MenuController(IAdminMenuManager adminMenuManager)
        {
            _adminMenuManager = adminMenuManager;
        }
        public MenuController()
        {
            _adminMenuManager = new AdminMenuManager();
        }

        public ActionResult Index()
        {
            return PartialView();
        }

        public JsonResult GetMenu(string url)
        {
            var menu = String.Empty;
            var currentUrl = url;
            var listMenuLevel1 = _adminMenuManager.SelectActive().Where(x => x.ParentAdminMenuId == 0).ToList();
            for (int i = 0; i < listMenuLevel1.Count; i++)
            {
                var listMenuLevel2 =
                    _adminMenuManager.SelectActive().Where(x => x.ParentAdminMenuId == listMenuLevel1[i].AdminMenuId).ToList();
                var maximumPerColumn = listMenuLevel1[i].MaxPerColumn;
                var addionalClass = String.Empty;
                if (i == 0)
                {
                    addionalClass += Constants.FirstClass;
                }
                else if (i == listMenuLevel1.Count - 1)
                {
                    addionalClass += Constants.LastClass;
                }
                if (listMenuLevel2.Count > 0)
                {
                    addionalClass += Constants.ParentClass;
                }
                if (listMenuLevel1[i].AdminMenuLink == currentUrl)
                {
                    addionalClass += Constants.ActiveClass;
                }
                menu += String.Format("{0}{1}{2}{3}{4}{5}{6}",
                    "<li class=\"level0 level-top",
                    addionalClass,
                    "\"><a href=\"",
                    listMenuLevel1[i].AdminMenuLink,
                    "\"><span>",
                    listMenuLevel1[i].AdminMenuText,
                    "</span></a>");
                if (listMenuLevel2.Count > 0)
                {
                    menu += "<div class=\"sub-wrapper\"><ul class=\"level0\">";
                    for (var j = 0; j < listMenuLevel2.Count; j++)
                    {
                        if (j == 0 || j%maximumPerColumn == 0)
                        {
                            menu += "<li><ol>";
                        }
                        addionalClass = String.Empty;
                        if (j == 0)
                        {
                            addionalClass += Constants.FirstClass;
                        }
                        else if (j == listMenuLevel2.Count - 1)
                        {
                            addionalClass += Constants.LastClass;
                        }
                        var listMenuLevel3 =
                            _adminMenuManager.SelectActive().Where(x => x.ParentAdminMenuId == listMenuLevel2[j].AdminMenuId).ToList();
                        if (listMenuLevel3.Count > 0)
                        {
                            addionalClass += Constants.ParentClass;
                        }
                        if (listMenuLevel2[j].AdminMenuLink == currentUrl)
                        {
                            addionalClass += Constants.ActiveClass;
                        }
                        menu += String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}",
                            "<li class=\"level1 nav-",
                            (i + 1),
                            "-",
                            (j + 1),
                            addionalClass,
                            "\"><a href=\"",
                            listMenuLevel2[j].AdminMenuLink,
                            "\"><span>",
                            listMenuLevel2[j].AdminMenuText,
                            "</span></a>");
                        if (listMenuLevel3.Count > 0)
                        {
                            menu += "<div class=\"sub-wrapper\"><ul class=\"level1\">";
                            for (var k = 0; k < listMenuLevel3.Count; k++)
                            {
                                addionalClass = String.Empty;
                                if (k == 0)
                                {
                                    addionalClass += Constants.FirstClass;
                                }
                                else if (k == listMenuLevel3.Count - 1)
                                {
                                    addionalClass += Constants.LastClass;
                                }
                                if (listMenuLevel3[k].AdminMenuLink == currentUrl)
                                {
                                    addionalClass += Constants.ActiveClass;
                                }
                                menu += String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}",
                                    "<li class=\"level2 nav-",
                                    (i + 1),
                                    "-",
                                    (j + 1),
                                    "-",
                                    (k + 1),
                                    addionalClass,
                                    "\"><a href=\"",
                                    listMenuLevel3[k].AdminMenuLink,
                                    "\"><span>",
                                    listMenuLevel3[k].AdminMenuText,
                                    "</span></a>");
                                menu += GenerateMenuLevel3(listMenuLevel3[k],currentUrl);
                                menu += "</li>";
                            }
                            menu += "</ul></div>";
                        }
                        menu += "</li>";
                        if (j % maximumPerColumn == maximumPerColumn - 1 || j == listMenuLevel2.Count-1)
                        {
                            menu += "</ol></li>";
                        }
                    }
                    menu += "</ul></div>";
                }
                menu += "</li>";
            }
            return Json(menu, JsonRequestBehavior.AllowGet);
        }

        //Recursive generate menu equal or above level 3
        private string GenerateMenuLevel3(AdminMenuResult currentItem, string url)
        {
            var menu = String.Empty;
            var currentUrl = url;
            var listMenu = _adminMenuManager.SelectActive().Where(x => x.ParentAdminMenuId == currentItem.AdminMenuId).ToList();
            if (listMenu.Count > 0)
            {
                menu += "<div class=\"sub-wrapper\"><ul class=\"level1\">";
                for (var k = 0; k < listMenu.Count; k++)
                {
                    var addionalClass = String.Empty;
                    if (k == 0)
                    {
                        addionalClass += Constants.FirstClass;
                    }
                    else if (k == listMenu.Count - 1)
                    {
                        addionalClass += Constants.LastClass;
                    }
                    if (listMenu[k].AdminMenuLink == currentUrl)
                    {
                        addionalClass += Constants.ActiveClass;
                    }
                    menu += String.Format("{0}{1}{2}{3}{4}{5}{6}",
                        "<li class=\"level2 ",
                        addionalClass,
                        "\"><a href=\"",
                        listMenu[k].AdminMenuLink,
                        "\"><span>",
                        listMenu[k].AdminMenuText,
                        "</span></a>");
                    menu += GenerateMenuLevel3(listMenu[k],currentUrl);
                    menu += "</li>";
                }
                menu += "</ul></div>";
            }
            return menu;
        }

        public JsonResult GetSiteMap(string url, string title)
        {
            var siteMap = String.Empty;
            if (url.Contains("/Edit/"))
            {
                var urls = url.Split('/');
                siteMap += String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}",
                "<ul><li class=\"home\"><a href=\"/\" title=\"",
                RsCommon.HomePage,
                "\">",
                RsCommon.HomePage,
                "</a><span></span></li><li class=\"home\"><a href=\"/Admin/\" title=\"",
                RsCommon.AdminHomePage,
                "\">",
                RsCommon.AdminHomePage,
                "</a><span></span></li><li class=\"home\"><a href=\"/",
                urls[1],
                "/",
                urls[2],
                "\">",
                Common.ToWord(urls[2]),
                "</a><span></span></li><li>",
                title,
                "</li>");
            }
            else
            {
                siteMap += String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}",
                                         "<ul><li class=\"home\"><a href=\"/\" title=\"",
                                         RsCommon.HomePage,
                                         "\">",
                                         RsCommon.HomePage,
                                         "</a><span></span></li><li class=\"home\"><a href=\"/Admin/\" title=\"",
                                         RsCommon.AdminHomePage,
                                         "\">",
                                         RsCommon.AdminHomePage,
                                         "</a><span></span></li><li>",
                                         title,
                                         "</li>");
            }
            return Json(siteMap, JsonRequestBehavior.AllowGet);
        }
    }
}
