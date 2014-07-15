using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Core.VisitRecord
{
    public class PagerOrder
    {
        public bool IsPager { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string OrderByValue { get; set; }

        public string OrderByDesc { get; set; }
    }
}
