using MyWebsite.Business;
using MyWebsite.Business.Implement;
using StructureMap.Configuration.DSL;

namespace MyWebsite.Ioc
{
    public class UiRegistry : Registry
    {
        public UiRegistry()
        {
            For<IMenuManager>().Use<MenuManager>();
            For<ISliderManager>().Use<SliderManager>();
            For<IAdminMenuManager>().Use<AdminMenuManager>();
        }
    }
}
