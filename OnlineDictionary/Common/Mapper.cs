using System;

namespace OnlineDictionary.Common
{
    public static class Mapper
    {
        public static TRes MapProperties<TRes>(object sourceObj)
        {
            TRes resObj = (TRes)Activator.CreateInstance(typeof(TRes));
            var resPropertiesInfo = typeof(TRes).GetProperties();
            foreach (var resObjProp in resPropertiesInfo)
            {
                var sourcePropInfo = sourceObj.GetType().GetProperty(resObjProp.Name);
                if(sourcePropInfo != null) resObjProp.SetValue(resObj, sourcePropInfo.GetValue(sourceObj, null));
            }
            return resObj;
        }
    }
}