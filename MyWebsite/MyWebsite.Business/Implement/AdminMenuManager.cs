using MyWebsite.DataAccess.IRepositories;
using MyWebsite.DataAccess.Models;
using MyWebsite.DataAccess.Repositories;
using MyWebsite.Domain;
using MyWebsite.Utils;
using System.Collections.Generic;
using System.Linq;

namespace MyWebsite.Business.Implement
{
    public class AdminMenuManager : IAdminMenuManager
    {
        private readonly IAdminMenuRepository _AdminMenuRepository;

        public AdminMenuManager(IAdminMenuRepository AdminMenuRepository)
        {
            _AdminMenuRepository = AdminMenuRepository;
        }

        public AdminMenuManager()
        {
            _AdminMenuRepository = new AdminMenuRepository();
        }

        #region LocalDB
        public DataReturn<AdminMenuResult> Create(AdminMenuResult entity)
        {
            return ModelMapping.MapDataReturn<AdminMenu, AdminMenuResult>(_AdminMenuRepository.Create(ModelMapping.Map<AdminMenu>(Encryption.Encrypt<AdminMenuResult>(entity))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataReturn<AdminMenuResult> Edit(AdminMenuResult entity)
        {
            return ModelMapping.MapDataReturn<AdminMenu, AdminMenuResult>(_AdminMenuRepository.Edit(ModelMapping.Map<AdminMenu>(Encryption.Encrypt<AdminMenuResult>(entity))));
        }

        public DataReturn<AdminMenuResult> Delete(AdminMenuResult entity)
        {
            return ModelMapping.MapDataReturn<AdminMenu, AdminMenuResult>(_AdminMenuRepository.Delete(ModelMapping.Map<AdminMenu>(entity)));
        }

        public DataReturn<AdminMenuResult> Delete(int id)
        {
            return ModelMapping.MapDataReturn<AdminMenu, AdminMenuResult>(_AdminMenuRepository.Delete(x => x.AdminMenuId.Equals(id)));
        }

        public AdminMenuResult Details(int id)
        {
            return Encryption.Decrypt<AdminMenuResult>(ModelMapping.Map<AdminMenuResult>(_AdminMenuRepository.Single(e => e.AdminMenuId.Equals(id))));
        }

        public IEnumerable<AdminMenuResult> SelectAll()
        {
            return _AdminMenuRepository.All().Select(ModelMapping.Map<AdminMenuResult>).Select(Encryption.Decrypt<AdminMenuResult>);
        }

        public IEnumerable<AdminMenuResult> SelectActive()
        {
            return _AdminMenuRepository.All().Where(x=> x.Status==true).Select(ModelMapping.Map<AdminMenuResult>).Select(Encryption.Decrypt<AdminMenuResult>);
        }

        public IEnumerable<AdminMenuResult> SelectDeactive()
        {
            return _AdminMenuRepository.All().Where(x=> x.Status==false).Select(ModelMapping.Map<AdminMenuResult>).Select(Encryption.Decrypt<AdminMenuResult>);
        }
        #endregion
    }
}
