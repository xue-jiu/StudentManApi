using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManApi.ResourceParameter
{
    public class StudentParameter
    {
        private const int MaxPageSize = 20;
        public string Keyword { get; set; }//名字中包含的关键字
        public string AddressBelong { get; set; }//根据住址查询
        private int pageNumber=1;
        public string SortBy { get; set; }
        public int PageNumber
        {
            get { return pageNumber; }
            set 
            {
                if (value<1)
                {
                    return;//pageNumber不能小于1
                }
                pageNumber = value; 
            }
        }
        private int pageSize = 5;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value>MaxPageSize)? MaxPageSize:value; }
        }
        public string Fields { get; set; }

    }
}
