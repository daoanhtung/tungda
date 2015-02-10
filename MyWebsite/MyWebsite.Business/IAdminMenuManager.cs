using System;
using System.Collections.Generic;
using MyWebsite.Domain;
using MyWebsite.Utils;

namespace MyWebsite.Business
{
    public interface IAdminMenuManager
    {
        DataReturn<AdminMenuResult> Create(AdminMenuResult entity);
        DataReturn<AdminMenuResult> Edit(AdminMenuResult entity);
        DataReturn<AdminMenuResult> Delete(AdminMenuResult entity);
        DataReturn<AdminMenuResult> Delete(int id);
        AdminMenuResult Details(int id);
        IEnumerable<AdminMenuResult> SelectAll();
        IEnumerable<AdminMenuResult> SelectActive();
        IEnumerable<AdminMenuResult> SelectDeactive();
    }
}
