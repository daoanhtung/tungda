using System.Collections.Generic;
using MyWebsite.DataAccess.Models;
using MyWebsite.Domain;

namespace MyWebsite.DataAccess.IRepositories
{
    public interface IMenuRepository : IRepository<Menu>
    {
        List<SelectMenuResult> SelectMenu();
    }
}
