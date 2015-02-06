using System;
using System.Collections.Generic;
using System.Data.Linq;
using MyWebsite.DataAccess.Models;
using MyWebsite.Utils;

namespace MyWebsite.DataAccess.IRepositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> All();
        void Submit();
        DataReturn<T> Create(T entity, bool submit = true);
        DataReturn<T> Create(List<T> listEntity, bool submit = true);
        DataReturn<T> Edit(T entity, bool submit = true);
        DataReturn<T> Delete(T entity, bool submit = true);
        DataReturn<T> Delete(IEnumerable<T> listEntity, bool submit = true);
        DataReturn<T> Delete(Func<T, bool> exp, bool submit = true);
        T Single(Func<T, bool> exp);
        T First(Func<T, bool> exp);
        IEnumerable<T> Find(Func<T, bool> exp);
        IEnumerable<T> Find(Func<T, bool> exp, Func<T, IComparable> order, bool desc);
    }
}
