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

namespace MyWebsite.Controllers
{
    public class TopMenuController : Controller
    {
        //
        // GET: /TopMenu/
        private readonly IMenuManager _menuManager;
        public TopMenuController(IMenuManager menuManager)
        {
            _menuManager = menuManager;
        }
        public TopMenuController()
        {
            _menuManager = new MenuManager();
        }
        private TopMenuViewModel Entity
        {
            get
            {
                if (Session[Constants.TopMenuSessionKey] == null)
                {
                    Session[Constants.TopMenuSessionKey] = new TopMenuViewModel
                                                               {
                                                                   Carts = new List<CartItemResult>(),
                                                                   CompareItems = new List<CompareItemResult>()
                                                               };
                }
                else
                {
                    Session[Constants.TopMenuSessionKey] = new TopMenuViewModel
                    {
                        Carts = new List<CartItemResult>(),
                        CompareItems = new List<CompareItemResult>()
                    };
                }

                return (TopMenuViewModel)Session[Constants.TopMenuSessionKey];
            }
        }

        public ActionResult Index()
        {
            return PartialView(Entity);
        }

        public JsonResult GetMenu(string url)
        {
            var menu = String.Empty;
            var currentUrl = url;
            var listMenuLevel1 = _menuManager.SelectActive().Where(x => x.ParentMenuId == 0).ToList();
            for (int i = 0; i < listMenuLevel1.Count; i++)
            {
                var listMenuLevel2 =
                    _menuManager.SelectActive().Where(x => x.ParentMenuId == listMenuLevel1[i].MenuId).ToList();
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
                if (listMenuLevel1[i].MenuLink == currentUrl)
                {
                    addionalClass += Constants.ActiveClass;
                }
                menu += String.Format("{0}{1}{2}{3}{4}{5}{6}",
                    "<li class=\"level0 level-top",
                    addionalClass,
                    "\"><a href=\"",
                    listMenuLevel1[i].MenuLink,
                    "\"><span>",
                    listMenuLevel1[i].MenuText,
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
                            _menuManager.SelectActive().Where(x => x.ParentMenuId == listMenuLevel2[j].MenuId).ToList();
                        if (listMenuLevel3.Count > 0)
                        {
                            addionalClass += Constants.ParentClass;
                        }
                        if (listMenuLevel2[j].MenuLink == currentUrl)
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
                            listMenuLevel2[j].MenuLink,
                            "\"><span>",
                            listMenuLevel2[j].MenuText,
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
                                if (listMenuLevel3[k].MenuLink == currentUrl)
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
                                    listMenuLevel3[k].MenuLink,
                                    "\"><span>",
                                    listMenuLevel3[k].MenuText,
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
        private string GenerateMenuLevel3(MenuResult currentItem, string url)
        {
            var menu = String.Empty;
            var currentUrl = url;
            var listMenu = _menuManager.SelectActive().Where(x => x.ParentMenuId == currentItem.MenuId).ToList();
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
                    if (listMenu[k].MenuLink == currentUrl)
                    {
                        addionalClass += Constants.ActiveClass;
                    }
                    menu += String.Format("{0}{1}{2}{3}{4}{5}{6}",
                        "<li class=\"level2 ",
                        addionalClass,
                        "\"><a href=\"",
                        listMenu[k].MenuLink,
                        "\"><span>",
                        listMenu[k].MenuText,
                        "</span></a>");
                    menu += GenerateMenuLevel3(listMenu[k],currentUrl);
                    menu += "</li>";
                }
                menu += "</ul></div>";
            }
            return menu;
        }

        public JsonResult GetCart()
        {
            var carts = Entity.Carts.Aggregate(String.Empty, (current, cart) => current + 
                String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}", 
                "<li class=\"item clearfix\">", 
                "<a href=\"" + cart.Link + "\" class=\"product-image\">", 
                "<img src=\"" + cart.ImageUrl + "\" height=\"50\" width=\"50\"/>", 
                "<span></span></a><div class=\"product-details\">", 
                "<a class=\"btn-remove\" onclick=\"TopMenu.RemoveCart(this)\" productId=\"" + cart.ProductId + "\"", 
                RsCommon.RemoveCart, 
                "</a><a href=\"" + cart.Link + "\" title=\"Edit item\" class=\"btn-edit\">", 
                RsCommon.EditCart, 
                "</a><p class=\"product-name\"><a href=\"" + cart.Link + "\">", 
                cart.ProductName, 
                "</a></p><strong>", 
                cart.Quantity, 
                "</strong> x<span class=\"price\">", 
                RsCommon.Currency, 
                cart.Price, 
                "</span></div></li>"));
            return Json(carts, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCompare()
        {
            var compares = Entity.CompareItems.Aggregate(String.Empty, (current, compareItem) => current +
                String.Format("{0}{1}{2}{3}{4}{5}",
                "<li class=\"item\">",
                "<a class=\"btn-remove\" onclick=\"TopMenu.RemoveCompare(this)\" productId=\"" + compareItem.ProductId + "\"",
                RsCommon.RemoveCart,
                "</a><p class=\"product-name\"><a href=\"" + compareItem.Link + "\">",
                compareItem.ProductName,
                "</a></p></li>"));
            return Json(compares, JsonRequestBehavior.AllowGet);
        }
    }
}
