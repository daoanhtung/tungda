using System;
using System.Collections.Generic;
using MyWebsite.Domain;
using MyWebsite.Utils;

namespace MyWebsite.Business
{
    public interface IBottomMenuManager
    {
        DataReturn<BottomMenuResult> Create(BottomMenuResult entity);
        DataReturn<BottomMenuResult> Edit(BottomMenuResult entity);
        DataReturn<BottomMenuResult> Delete(BottomMenuResult entity);
        DataReturn<BottomMenuResult> Delete(int id);
        BottomMenuResult Details(int id);
        IEnumerable<BottomMenuResult> SelectAll();
        IEnumerable<BottomMenuResult> SelectActive();
        IEnumerable<BottomMenuResult> SelectDeactive();
    }
}
