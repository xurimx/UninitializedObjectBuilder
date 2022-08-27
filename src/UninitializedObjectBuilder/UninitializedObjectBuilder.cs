using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using UninitializedObjectBuilder.Extensions;

namespace UninitializedObjectBuilder
{
    public class UninitializedObjectBuilder<T> where T: class
    {
        private readonly Dictionary<MemberInfo, object?> _fields;
        private readonly object _uninitializedObject;
        private readonly Type _type;
        
        public UninitializedObjectBuilder()
        {
            _type = typeof(T);
            _uninitializedObject = FormatterServices.GetSafeUninitializedObject(typeof(T));
            _fields = new Dictionary<MemberInfo, object?>();
        }

        public UninitializedObjectBuilder<T> InjectField<TD>(TD value)
        {
            var valueType = value?.GetType();
            
            var fieldInfo = _type.GetAllFields().Single(x => x.FieldType.IsAssignableFrom(valueType));
        
            _fields.TryAdd(fieldInfo, value);
        
            return this;
        }

        public UninitializedObjectBuilder<T> InjectField<TD>(string fieldName, TD? value)
        {
            var valueType = value?.GetType();

            var fieldInfos = _type.GetAllFields();
            
            var fieldInfo = fieldInfos.Single(x => x.Name == fieldName && x.FieldType.IsAssignableFrom(valueType));
        
            _fields.TryAdd(fieldInfo, value);
        
            return this;
        }
        
        public UninitializedObjectBuilder<T> InjectFields(params object?[] fields)
        {
            var fieldInfos = _type.GetAllFields();
        
            foreach (var field in fields)
            {
                var type = field?.GetType();
                var fieldInfo = fieldInfos.Single(x => x.FieldType.IsAssignableFrom(type));
                _fields.TryAdd(fieldInfo, field);
            }

            return this;
        }
        
        public T Build()
        {
            FormatterServices.PopulateObjectMembers(
                _uninitializedObject, 
                _fields.Keys.ToArray(), 
                _fields.Values.ToArray());
            
            return (T)_uninitializedObject;
        }
    }    
};