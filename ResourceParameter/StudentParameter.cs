using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManApi.ResourceParameter
{
    public class StudentParameter
    {
        public string Keyword { get; set; }//名字中包含的关键字
        public string AddressBelong { get; set; }//根据住址查询
    }
}
