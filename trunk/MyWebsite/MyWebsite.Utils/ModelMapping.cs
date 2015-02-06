using System;
using System.Linq;
using System.Reflection;

namespace MyWebsite.Utils
{
    public static class ModelMapping
    {
        public static T Map<T>(object fromSource) where T : class, new()
        {
            var ret = new T();
            if (fromSource == null)
            {
                return ret;
            }
            var sourceProps = fromSource.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var destinationProps = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var desProp in destinationProps)
            {
                var sourceProp = sourceProps.FirstOrDefault(m => m.Name == desProp.Name);

                if (sourceProp != null && desProp.SetMethod!=null)
                {
                    desProp.SetValue(ret, sourceProp.GetValue(fromSource, null), null);
                }
            }

             return ret;
        }

        public static DataReturn<TDestination> MapDataReturn<TSource, TDestination>(DataReturn<TSource> fromSource)
            where TSource : class, new()
            where TDestination : class, new()
        {
            if (fromSource == null)
            {
                return null;
            }

            return new DataReturn<TDestination> { Obj = (fromSource.Obj != null) ? Encryption.Decrypt<TDestination>(Map<TDestination>(fromSource.Obj)) : null, IsSuccess = fromSource.IsSuccess, Message = fromSource.Message };
        }
    }
}
