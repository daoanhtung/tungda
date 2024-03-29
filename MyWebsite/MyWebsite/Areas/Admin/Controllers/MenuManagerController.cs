﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using MyWebsite.Areas.Admin.Models;
using MyWebsite.Business;
using MyWebsite.Business.Implement;
using MyWebsite.Domain;
using MyWebsite.Resx;
using MyWebsite.Utils;

namespace MyWebsite.Areas.Admin.Controllers
{
    public class MenuManagerController : Controller
    {
        //
        // GET: /Admin/MenuManager/
        private readonly IMenuManager _menuManager;
        public MenuManagerController(IMenuManager menuManager)
        {
            _menuManager = menuManager;
        }
        public MenuManagerController()
        {
            _menuManager = new MenuManager();
        }
        public ActionResult Index()
        {
            var menus = _menuManager.SelectMenu();
            foreach (var menuResult in menus)
            {
                var menuText = String.Empty;
                bool isConcat = false;
                foreach (var character in menuResult.MenuText.ToArray())
                {
                    if (character != Constants.Tab && !isConcat)
                    {
                        menuText += Constants.Space;
                        isConcat = true;
                    }
                    menuText += character;
                }
                menuResult.MenuText = menuText;
                menuText = String.Empty;
                isConcat = false;
                foreach (var character in menuResult.MenuText_VI.ToArray())
                {
                    if (character != Constants.Tab && !isConcat)
                    {
                        menuText += Constants.Space;
                        isConcat = true;
                    }
                    menuText += character;
                }
                menuResult.MenuText_VI = menuText;
            }
            var entity = new TopMenuModel
            {
                Menus = menus
            };
            return View(entity);
        }

        public JsonResult Delete(string menuId)
        {
            try
            {
                var deleteList = GetListDelete(int.Parse(menuId), new List<int>());
                deleteList.Reverse();
                DeleteMenu(deleteList);
                return Json(true, JsonRequestBehavior.AllowGet);
            }catch(Exception ex)
            {
                Logger.Log(ex.Message,false);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        private List<int> GetListDelete(int menuId, List<int> deleteList)
        {
            deleteList.Add(menuId);
            var menus = _menuManager.SelectAll().Where(x => x.ParentMenuId == menuId).ToList();
            foreach (var menuResult in menus)
            {
                GetListDelete((int)menuResult.MenuId, deleteList);
            }
            return deleteList;
        }

        private void DeleteMenu(IEnumerable<int> deleteList)
        {
            foreach (var menu in deleteList)
            {
                _menuManager.Delete(menu);
            }
        }

        public ActionResult Edit(int Id)
        {
            var menu = _menuManager.Details(Id);
            var allMenu = _menuManager.SelectMenu().Where(x => x.MenuId != Id).ToList();
            var defaultMenu = new MenuResult
                                  {
                                      MenuText = "Please select"
                                  };
            allMenu.Insert(0,defaultMenu);
            var entity = new MenuDetailView
                             {
                                 AllMenu = allMenu,
                                 Menu = menu
                             };
            return View(entity);
        }

        [HttpPost]
        public ActionResult Edit(MenuDetailView entity, int Id)
        {
            if (ModelState.IsValid)
            {
                entity.Menu.MenuId = Id;
                var result = _menuManager.Edit(entity.Menu);
                if (result.IsSuccess)
                {
                    Session[Constants.UpdateMenuSuccessKeySession] = true;
                    return RedirectToAction("Index", "MenuManager");
                }
            }
            var allMenu = _menuManager.SelectMenu().Where(x => x.MenuId != Id).ToList();
            var defaultMenu = new MenuResult
            {
                MenuText = "Please select"
            };
            allMenu.Insert(0, defaultMenu);
            entity.AllMenu = allMenu;
            TempData[Constants.UpdateMenuSuccessKeySession] = false;
            return View(entity);
        }
    }
}
