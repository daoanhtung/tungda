using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using MyWebsite.DataAccess.IRepositories;
using MyWebsite.DataAccess.Models;
using MyWebsite.Utils;

namespace MyWebsite.DataAccess.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly MyWebsiteDataContext _MyWebsite;

        protected Repository()
        {
            _MyWebsite = new MyWebsiteDataContext(ConfigurationManager.ConnectionStrings["MyWebsiteConnectionString"].ConnectionString);
        }

        private Table<T> GetTable
        {
            get { return _MyWebsite.GetTable<T>(); }
        }

        public IEnumerable<T> All()
        {
            return GetTable.Cast<T>().ToList();
        }

        public void Submit()
        {
            try
            {
                _MyWebsite.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual DataReturn<T> Create(T entity, bool submit = true)
        {
            try
            {
                GetTable.InsertOnSubmit(entity);
                if (submit)
                {
                    _MyWebsite.SubmitChanges();
                }
                return new DataReturn<T> { Obj = entity, IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new DataReturn<T> { Obj = entity, IsSuccess = false, Message = ex.StackTrace };
            }
        }

        public virtual DataReturn<T> Create(List<T> listEntity, bool submit = true)
        {
            try
            {
                GetTable.InsertAllOnSubmit(listEntity);
                if (submit)
                {
                    _MyWebsite.SubmitChanges();
                }
                return new DataReturn<T> { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new DataReturn<T> { IsSuccess = false, Message = ex.StackTrace };
            }
        }

        public virtual DataReturn<T> Edit(T entity, bool submit = true)
        {
            try
            {
                var dataContext = new MyWebsiteDataContext(ConfigurationManager.ConnectionStrings["MyWebsiteConnectionString"].ConnectionString);
                dataContext.GetTable<T>().Attach(entity);
                dataContext.Refresh(RefreshMode.KeepCurrentValues, entity);
                if (submit)
                {
                    dataContext.SubmitChanges();
                }
                return new DataReturn<T> { Obj = entity, IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new DataReturn<T> { Obj = entity, IsSuccess = false, Message = ex.StackTrace };
            }

        }

        public virtual DataReturn<T> Delete(Func<T, bool> exp, bool submit = true)
        {
            try
            {
                //GetTable.Attach(entity);
                var single = GetTable.Cast<T>().Single(exp);
                GetTable.DeleteOnSubmit(single);
                if (submit)
                {
                    _MyWebsite.SubmitChanges();
                }
                return new DataReturn<T> { Obj = single, IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new DataReturn<T> { IsSuccess = false, Message = ex.StackTrace };
            }
        }

        public virtual DataReturn<T> Delete(T entity, bool submit = true)
        {
            try
            {
                //GetTable.Attach(entity);
                GetTable.DeleteOnSubmit(entity);
                if (submit)
                {
                    _MyWebsite.SubmitChanges();
                }
                return new DataReturn<T> { Obj = entity, IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new DataReturn<T> { Obj = entity, IsSuccess = false, Message = ex.StackTrace };
            }
        }

        public virtual DataReturn<T> Delete(IEnumerable<T> listEntity, bool submit = true)
        {
            try
            {
                //GetTable.AttachAll(listEntity);
                GetTable.DeleteAllOnSubmit(listEntity);
                if (submit)
                {
                    _MyWebsite.SubmitChanges();
                }
                return new DataReturn<T> { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new DataReturn<T> { IsSuccess = false, Message = ex.StackTrace };
            }
        }

        public virtual T Single(Func<T, bool> exp)
        {
            try
            {
                return GetTable.Cast<T>().Single(exp);
            }catch(Exception ex)
            {
                Logger.Log(ex.Message,true);
                return null;
            }
        }

        public T First(Func<T, bool> exp)
        {
            try
            {
                return GetTable.Cast<T>().First(exp);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, true);
                return null;
            }
        }

        public virtual IEnumerable<T> Find(Func<T, bool> exp)
        {
            try
            {
                return GetTable.Cast<T>().Where(exp).ToList();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, true);
                return new List<T>();
            }
        }

        public virtual IEnumerable<T> Find(Func<T, bool> exp, Func<T, IComparable> order, bool desc)
        {
            if (desc)
                return GetTable.Cast<T>().Where(exp).OrderByDescending(order).ToList();
            return GetTable.Cast<T>().Where(exp).OrderBy(order).ToList();
        }
    }
}
