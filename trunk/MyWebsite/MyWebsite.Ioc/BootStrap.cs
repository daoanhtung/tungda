using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;

namespace MyWebsite.Ioc
{
    public static class BootStrap
	{
		public static IContainer Initialize()
		{
#pragma warning disable 612,618
			ObjectFactory.Initialize(registry =>
#pragma warning restore 612,618
			{
				registry.AddRegistry<BusinessRegistry>(); 
				registry.AddRegistry<UiRegistry>();
			});

#pragma warning disable 612,618
			ObjectFactory.Container.AssertConfigurationIsValid();
#pragma warning restore 612,618

#pragma warning disable 612,618
			return ObjectFactory.Container;
#pragma warning restore 612,618
		}
	}
}
