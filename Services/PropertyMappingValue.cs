﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManApi.Services
{
    public class PropertyMappingValue
    {
        public IEnumerable<string> DestinationProperties { get; private set; }
        public bool Revert { get; set; } = false;
        public PropertyMappingValue(IEnumerable<string> destinationProperties,bool revert=false)
        {
            DestinationProperties = destinationProperties;
            Revert = revert;
        }
    }
}
