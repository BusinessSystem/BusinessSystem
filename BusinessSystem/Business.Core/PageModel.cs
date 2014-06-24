using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Core
{
    public class PageModel<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int VisiblePages { get; set; }
        public int TotalCount { get; set; }
        public IList<T> PageList { get; set; }

        public PageModel(IList<T> pageList, int currentPage, int pageSize, int totalCount)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
            PageList = pageList;
            VisiblePages = totalCount / PageSize;
            if (totalCount % PageSize != 0)
            {
                VisiblePages = VisiblePages + 1;
            }
            if (VisiblePages >= 10)
            {

                VisiblePages = 10;
            }
            if (pageList.Count == 0)
            {
                CurrentPage = 1;
            }
        }
    }

   
}
