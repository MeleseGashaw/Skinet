using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helper
{
    public class Pagination<T> where T : class
    {
        public Pagination(int pageIndex, int pageSize, int countt, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Countt = countt;
            Data = data;
        }

        public int PageIndex{get; set;}
         public int PageSize {get; set;}
          public int Countt {get; set;}
           public IReadOnlyList<T> Data{get; set;}

    }
}