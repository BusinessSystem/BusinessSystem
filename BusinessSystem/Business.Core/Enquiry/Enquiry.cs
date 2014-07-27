using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public virtual string IpString { get; set; }
        public virtual string PurchaserEmail { get; set; }
        /// <summary>
        /// 询盘内容
        /// </summary>
        public virtual string EnquiryContent { get; set; }
        public virtual string ProductUrl { get; set; }
        public virtual string ProductName { get; set; }//add by luoyaqi 20140723
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
        public virtual string  HandlerName { get; set; }
        public virtual HandlerStatusEnum HandlerStatus { get; set; }
        public virtual DateTime HandlerTime { get; set; }
        public virtual long UserDefinedId { get; set; }
        public virtual string UserDefinedName { get; set; }
        public virtual long IntentionId { get; set; }
        public virtual string IntentionName { get; set; }
        public virtual int FollowUpTimes { get; set; }
        public virtual short IsDeleted { get; set; }
        public virtual EmailStatusEnum EmailStatus { get; set; }
        public virtual long LanguageId { get; set; }
        public virtual int EnquiryTimes { get; set; }
    }

    public enum HandlerStatusEnum : short
    {
        [Description("未读")]
        UnRead = 1,
        [Description("已读")]
        HasRead = 2
    }

    public class EnquiryFactory
    {
        public static Enquiry Create(string ipString, string purchaserEmail, string enquiryContent,
            string productUrl,string productName, string purchaserUserName, string purchaserCompany, string purchaserTel,
            string purchaserMsn, string visitLanguage,long languageId, string purchaserCountry, long receiverId,string receiverName)
        {
            return new Enquiry()
            {
                IpString = ipString,
                PurchaserEmail = purchaserEmail,
                EnquiryContent = enquiryContent,
                ProductUrl = productUrl,
                ProductName = productName,
                PurchaserUserName = purchaserUserName,
                PurchaserCompany = purchaserCompany,
                PurchaserTel = purchaserTel,
                PurchaserMsn = purchaserMsn,
                VisitLanguage = visitLanguage,
                PurchaserCountry = purchaserCountry,
                EnquiryTime = DateTime.Now,
                ReceiverId = receiverId,
                IsIssuedChildManager = Utils.CoreDefaultValue.False,
                HandlerStatus = HandlerStatusEnum.UnRead,
                HandlerTime = Utils.CoreDefaultValue.MinTime,
                FollowUpTimes = 0,
                IsDeleted = Utils.CoreDefaultValue.False,
                HandlerId = receiverId,
                HandlerName = receiverName,
                LanguageId = languageId,
                EmailStatus = EmailStatusEnum.HasRead
            };
        }
    }
}
