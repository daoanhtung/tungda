using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using MyWebsite.DataAccess.IRepositories;
using MyWebsite.DataAccess.Models;
using MyWebsite.Domain;
using MyWebsite.Utils;

namespace MyWebsite.DataAccess.Repositories
{
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
        private readonly MyWebsiteDataContext _myWebsite;

        public MenuRepository()
        {
            _myWebsite =
                new MyWebsiteDataContext(
                    ConfigurationManager.ConnectionStrings["MyWebsiteConnectionString"].ConnectionString);
        }

        public List<SelectMenuResult> SelectMenu()
        {
            return _myWebsite.Menu_SelectAll().ToList().Select(ModelMapping.Map<SelectMenuResult>).ToList();
        }
    }
}
