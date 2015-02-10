using MyWebsite.DataAccess.IRepositories;
using MyWebsite.DataAccess.Models;
using MyWebsite.DataAccess.Repositories;
using MyWebsite.Domain;
using MyWebsite.Utils;
using System.Collections.Generic;
using System.Linq;

namespace MyWebsite.Business.Implement
{
    public class MenuManager : IMenuManager
    {
        private readonly IMenuRepository _menuRepository;

        public MenuManager(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public MenuManager()
        {
            _menuRepository = new MenuRepository();
        }

        #region LocalDB
        public DataReturn<MenuResult> Create(MenuResult entity)
        {
            return ModelMapping.MapDataReturn<Menu, MenuResult>(_menuRepository.Create(ModelMapping.Map<Menu>(Encryption.Encrypt<MenuResult>(entity))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataReturn<MenuResult> Edit(MenuResult entity)
        {
            return ModelMapping.MapDataReturn<Menu, MenuResult>(_menuRepository.Edit(ModelMapping.Map<Menu>(Encryption.Encrypt<MenuResult>(entity))));
        }

        public DataReturn<MenuResult> Delete(MenuResult entity)
        {
            return ModelMapping.MapDataReturn<Menu, MenuResult>(_menuRepository.Delete(ModelMapping.Map<Menu>(entity)));
        }

        public DataReturn<MenuResult> Delete(int id)
        {
            return ModelMapping.MapDataReturn<Menu, MenuResult>(_menuRepository.Delete(x => x.MenuId.Equals(id)));
        }

        public MenuResult Details(int id)
        {
            return Encryption.Decrypt<MenuResult>(ModelMapping.Map<MenuResult>(_menuRepository.Single(e => e.MenuId.Equals(id))));
        }

        public IEnumerable<MenuResult> SelectAll()
        {
            return _menuRepository.All().Select(ModelMapping.Map<MenuResult>).Select(Encryption.Decrypt<MenuResult>);
        }

        public IEnumerable<MenuResult> SelectActive()
        {
            return _menuRepository.All().Where(x=> x.Status==true).Select(ModelMapping.Map<MenuResult>).Select(Encryption.Decrypt<MenuResult>);
        }

        public IEnumerable<MenuResult> SelectDeactive()
        {
            return _menuRepository.All().Where(x=> x.Status==false).Select(ModelMapping.Map<MenuResult>).Select(Encryption.Decrypt<MenuResult>);
        }

        public List<MenuResult> SelectMenu()
        {
            return _menuRepository.SelectMenu().Select(ModelMapping.Map<MenuResult>).ToList();
        }
        #endregion
    }
}
