using System;
using System.Collections.Generic;
using System.Reflection;

namespace UninitializedObjectBuilder.Extensions
{
    internal static class TypeExtensions
    {
        public static FieldInfo[] GetAllFields(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof (type));
            if (!type.IsClass)
                throw new ArgumentException(
                    $"Type {(object)type} is not a class type. This method supports only classes");
            
            var fieldInfoList = new List<FieldInfo>();
            
            for (var type1 = type; type1 != typeof (object); type1 = type1.BaseType)
            {
                var fields = type1.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
                fieldInfoList.AddRange(fields);
            }
            
            return fieldInfoList.ToArray();
        }
    }
}