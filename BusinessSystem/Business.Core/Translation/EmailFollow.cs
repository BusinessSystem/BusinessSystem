using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Core
{
    /// <summary>
    /// 邮件跟踪
    /// </summary>
    public class EmailFollow
    {
        public virtual long Id { get; set; }
        public virtual long EmailTransId { get; set; }
        public virtual string OriginalContent { get; set; }
        public virtual string TargetContent { get; set; }
        public virtual string OriginalLanguage { get; set; }
        public virtual string TargetLanguage { get; set; }
        public virtual DateTime TransTime { get; set; }
    }

    public class EmailFollowFactory
    {
        public static EmailFollow Create(long emailTransId, string originalContent, string originalLanguage)
        {
            return new EmailFollow()
            {
                EmailTransId = emailTransId,
                OriginalContent = originalContent,
                OriginalLanguage = originalContent,
                TransTime = DateTime.Now
            };
        }
    }
}
