using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Core
{
    /// <summary>
    /// 询盘（留言）
    /// </summary>
    public class Enquiry
    {
        public virtual long Id { get; set; }
        public virtual long IpString { get; set; }
        public virtual string PurchaserEmail { get; set; }
        /// <summary>
        /// 询盘内容
        /// </summary>
        public virtual string EnquiryContent { get; set; }

        public virtual string ProductUrl { get; set; }
        public virtual string PurchaserUserName { get; set; }
        public virtual string PurchaserCompany { get; set; }
        public virtual string PurchaserTel { get; set; }
        public virtual string PurchaserMsn { get; set; }
        public virtual string VisitLanguage { get; set; }
        public virtual string PurchaserCountry { get; set; }
        public virtual DateTime EnquiryTime { get; set; }
        public virtual long ReceiverId { get; set; }
        public virtual short IsIssuedChildManager { get; set; }
        public virtual long HandlerId { get; set; }
        public virtual HandlerStatusEnum HandlerStatus { get; set; }
        public virtual DateTime HandlerTime { get; set; }
        public virtual long UserDefinedId { get; set; }
        public virtual long IntentionId { get; set; }
        public virtual int FollowUpTimes { get; set; }
        public virtual short IsDeleted { get; set; }
    }

    public enum HandlerStatusEnum : short
    {
         
    }

    public class EnquiryFactory
    {
         
    }
}
