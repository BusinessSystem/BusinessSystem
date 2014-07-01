using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business.Core;

namespace Business.Web.PageModel
{
    public class PageEnquiry
    {
        public Enquiry Enquiry { get; set; }
        public EmailTranslation EmailTranslation { get; set; }
        public IList<EmailFollow> EnquiryTransFollows { get; set; }

        public PageEnquiry()
        {
            EnquiryTransFollows = new List<EmailFollow>();
        }
    }
}