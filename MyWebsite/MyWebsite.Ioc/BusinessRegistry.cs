using MyWebsite.DataAccess.IRepositories;
using MyWebsite.DataAccess.Repositories;
using StructureMap.Configuration.DSL;

namespace MyWebsite.Ioc
{
    public class BusinessRegistry : Registry
    {
        public BusinessRegistry()
        {
            For<IMenuRepository>().Use<MenuRepository>();
            For<ISliderRepository>().Use<SliderRepository>();
            For<IAdminMenuRepository>().Use<AdminMenuRepository>();
        }
    }
}
