using System;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace MyWebsite.Ioc
{
	public class SingletonConvention : DefaultConventionScanner
	{
		public override void Process(Type type, Registry registry)
		{
			if (type.IsAbstract) return;
			Type pluginType = FindPluginType(type);
			if (pluginType != null)
			{
				registry.AddType(pluginType, type);

				ConfigureFamily(registry.For(pluginType).Singleton());
			}
		}
	}
}
