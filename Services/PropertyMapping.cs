using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManApi.Services
{
    public class PropertyMapping<TSource, TDestination>:IProperyMapping
    {
        public Dictionary<string, PropertyMappingValue> _mappingDictionary { get; set; }
        public PropertyMapping(Dictionary<string,PropertyMappingValue> mappingDictionary)
        {
            _mappingDictionary = mappingDictionary;
        }
    }
}
