using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Core
{
    public class EnquiryTransFollow
    {
        public virtual long Id { get; set; }
        public virtual long EnquiryId { get; set; }
        public virtual string OriginalContent { get; set; }
        public virtual string TargetContent { get; set; }
        public virtual string OriginalLanguage { get; set; }
        public virtual string TargetLanguage { get; set; }
        public virtual HandlerStatusEnum SenderStatus { get; set; }
        public virtual HandlerStatusEnum HandlerStatus { get; set; }
        public long SenderId { get; set; }
        public string SenderName { get; set; }
        public long HandlerId { get; set; }
        public string HandlerName { get; set; }
        public string HandlerTime { get; set; }
        public virtual DateTime TransTime { get; set; }
    }

    public class EnquiryTransFollowFactory
    {
        public static EnquiryTransFollow Create(long enquiryId, string originalContent, string targetContent,
            string originalLanguage, string targetLanguage)
        {
            return new EnquiryTransFollow()
            {
                EnquiryId = enquiryId,
                OriginalContent = originalContent,
                TargetContent = targetContent,
                OriginalLanguage = originalLanguage,
                TargetLanguage = targetLanguage,
                TransTime = DateTime.Now

            };
        }
    }
}
