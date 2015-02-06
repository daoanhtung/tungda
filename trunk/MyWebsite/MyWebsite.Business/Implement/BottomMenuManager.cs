using MyWebsite.DataAccess.IRepositories;
using MyWebsite.DataAccess.Models;
using MyWebsite.DataAccess.Repositories;
using MyWebsite.Domain;
using MyWebsite.Utils;
using System.Collections.Generic;
using System.Linq;

namespace MyWebsite.Business.Implement
{
    public class BottomMenuManager : IBottomMenuManager
    {
        private readonly IBottomMenuRepository _bottomMenuRepository;

        public BottomMenuManager(IBottomMenuRepository bottomMenuRepository)
        {
            _bottomMenuRepository = bottomMenuRepository;
        }

        public BottomMenuManager()
        {
            _bottomMenuRepository = new BottomMenuRepository();
        }

        #region LocalDB
        public DataReturn<BottomMenuResult> Create(BottomMenuResult entity)
        {
            return ModelMapping.MapDataReturn<BottomMenu, BottomMenuResult>(_bottomMenuRepository.Create(ModelMapping.Map<BottomMenu>(Encryption.Encrypt<BottomMenuResult>(entity))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataReturn<BottomMenuResult> Edit(BottomMenuResult entity)
        {
            return ModelMapping.MapDataReturn<BottomMenu, BottomMenuResult>(_bottomMenuRepository.Edit(ModelMapping.Map<BottomMenu>(Encryption.Encrypt<BottomMenuResult>(entity))));
        }

        public DataReturn<BottomMenuResult> Delete(BottomMenuResult entity)
        {
            return ModelMapping.MapDataReturn<BottomMenu, BottomMenuResult>(_bottomMenuRepository.Delete(ModelMapping.Map<BottomMenu>(entity)));
        }

        public DataReturn<BottomMenuResult> Delete(int id)
        {
            return ModelMapping.MapDataReturn<BottomMenu, BottomMenuResult>(_bottomMenuRepository.Delete(x => x.BottomMenuId.Equals(id)));
        }

        public BottomMenuResult Details(int id)
        {
            return Encryption.Decrypt<BottomMenuResult>(ModelMapping.Map<BottomMenuResult>(_bottomMenuRepository.Single(e => e.BottomMenuId.Equals(id))));
        }

        public IEnumerable<BottomMenuResult> SelectAll()
        {
            return _bottomMenuRepository.All().Select(ModelMapping.Map<BottomMenuResult>).Select(Encryption.Decrypt<BottomMenuResult>);
        }

        public IEnumerable<BottomMenuResult> SelectActive()
        {
            return _bottomMenuRepository.All().Where(x=> x.Status==true).Select(ModelMapping.Map<BottomMenuResult>).Select(Encryption.Decrypt<BottomMenuResult>);
        }

        public IEnumerable<BottomMenuResult> SelectDeactive()
        {
            return _bottomMenuRepository.All().Where(x=> x.Status==false).Select(ModelMapping.Map<BottomMenuResult>).Select(Encryption.Decrypt<BottomMenuResult>);
        }
        #endregion
    }
}
