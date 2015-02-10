using System;
using System.Collections.Generic;
using MyWebsite.Domain;
using MyWebsite.Utils;

namespace MyWebsite.Business
{
    public interface IMenuManager
    {
        DataReturn<MenuResult> Create(MenuResult entity);
        DataReturn<MenuResult> Edit(MenuResult entity);
        DataReturn<MenuResult> Delete(MenuResult entity);
        DataReturn<MenuResult> Delete(int id);
        MenuResult Details(int id);
        IEnumerable<MenuResult> SelectAll();
        IEnumerable<MenuResult> SelectActive();
        IEnumerable<MenuResult> SelectDeactive();
        List<MenuResult> SelectMenu();
    }
}
