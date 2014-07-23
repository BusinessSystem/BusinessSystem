using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Business.WebApi.Models
{
    public class EnquirySavaQuery
    {
        public string email { get; set; }
        public string content { get; set; }
        public string productUrl { get; set; }
        public string productName { get; set; }
        public string yourName { get; set; }
        public string company { get; set; }
        public string tel { get; set; }
        public string msn { get; set; }
        public string language { get; set; }
        public string recievedId { get; set; }
    }

    public class VisitorRecordSaveQuery
    {
        public string PurchaserProduct { get; set; }
        public string PurchaserDomain { get; set; }
        public string Language { get; set; }
        public string TargetEmail { get; set; }
    }
}