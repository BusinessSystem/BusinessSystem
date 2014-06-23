using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business.Core;

namespace Business.Web.PageModel
{
    public class PageManager
    {
        public IList<BaseDictionary> BaseDictionaries { get; set; }
        public Manager ManagerBase { get; set; }
        public Dictionary<ManagerTypeEnum, string> ManagerTypes { get; set; }
    }
}