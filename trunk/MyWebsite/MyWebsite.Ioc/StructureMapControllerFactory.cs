using System;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace MyWebsite.Ioc
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null) return null;

#pragma warning disable 612,618
            return ObjectFactory.GetInstance(controllerType) as Controller;
#pragma warning restore 612,618
        }
    }
}