using System.Linq;
using System.Reflection;

namespace Acme.Helpers.Core.Extensions
{
    /// <exclude/>
    internal static partial class ExtensionMethods
    {
        /// <exclude/>
        public static T MapPropertiesByName<T>(this object source, T target)
        {
            foreach (PropertyInfo sourceProp in source.GetType().GetProperties())
            {
                PropertyInfo targetProp = target.GetType().GetProperties().Where(p => p.Name.Equals(sourceProp.Name)).FirstOrDefault();
                if (targetProp != null && targetProp.GetType().Name.Equals(sourceProp.GetType().Name) && targetProp.CanWrite)
                {
                    targetProp.SetValue(target, sourceProp.GetValue(source));
                }
            }
            return target;
        }

        /// <exclude/>
        public static T MapPropertiesByName<T>(this object source) where T : new()
        {
            T target = new T();
            foreach (PropertyInfo sourceProp in source.GetType().GetProperties())
            {
                PropertyInfo targetProp = target.GetType().GetProperties().Where(p => p.Name == sourceProp.Name).FirstOrDefault();
                if (targetProp != null && targetProp.GetType().Name.Equals(sourceProp.GetType().Name) && targetProp.CanWrite)
                {
                    targetProp.SetValue(target, sourceProp.GetValue(source));
                }
            }
            return target;
        }
    }
}
