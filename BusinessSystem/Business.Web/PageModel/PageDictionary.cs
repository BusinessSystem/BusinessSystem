using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business.Core;

namespace Business.Web.PageModel
{
    public class PageDictionary
    {
        public IList<BaseDictionary> BaseDictionaries { get; set; }
        public Dictionary<ValueTypeEnum, string> ValueTypes { get; set; }
    }

    public class EditDictionary
    {
        public BaseDictionary BaseDictionary { get; set; }
        public Dictionary<ValueTypeEnum, string> ValueTypes { get; set; }
    }
}