using System.Reflection;

namespace ModelValidationsExample.Utility
{
    public static class ReflectionUtility
    {
        ///Summary: Returns the datetime value of a given class property
        ///
        public static DateTime GetDateTimeValue(ValidationContext vCtx, string propertyName)
        {
           
            PropertyInfo? propInfo = vCtx.ObjectType.GetProperty(propertyName);
            return Convert.ToDateTime(propInfo.GetValue(vCtx.ObjectInstance));
        }
    }
}
