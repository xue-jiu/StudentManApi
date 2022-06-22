using StudentManApi.Dtos;
using StudentManApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManApi.Services
{
    public class PropertyMappingService:IPropertyMappingService
    {
        private Dictionary<string, PropertyMappingValue> _studentPropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                {"Id",new PropertyMappingValue(new List<string>(){ "Id"} ) },
                {"NationBelong",new PropertyMappingValue(new List<string>(){ "NationBelong" } ) }
            };
        private IList<IProperyMapping> _propertyMappings = new List<IProperyMapping>();//用IPropertyMapping这个接口是为了动态实现<tsource,tdestination>,多态的妙用
        public PropertyMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<StudentDto, Student>(_studentPropertyMapping));
        }
        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()//使用Tsource和TDestination来挑出相应的字典
        {
            var matchMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();
            if (matchMapping.Count()==1)
            {
                return matchMapping.First()._mappingDictionary;
            }
            throw new Exception($"无法找到唯一的映射关系,{typeof(TSource)},{typeof(TDestination)}");
        }
    }
}
