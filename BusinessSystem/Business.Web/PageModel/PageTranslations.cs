using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business.Core;

namespace Business.Web.PageModel
{
    public class PageTranslations
    {
        public IList<Intention> Intentions { get; set; }
        public IList<EmailTranslation> EmailTranslations { get; set; }
        public Manager CurrentManager { get; set; }
    }
}