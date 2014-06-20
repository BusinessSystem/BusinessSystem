using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Core
{
    /// <summary>
    /// 邮件翻译
    /// </summary>
    public class EmailTranslation
    {
        public virtual long Id { get; set; }
        public virtual long ReceiverId { get; set; }
        public virtual long SenderId { get; set; }
        public virtual DateTime SendTime { get; set; }
        public virtual DateTime ReadTime { get; set; }
        public virtual string Theme { get; set; }
        public virtual string Content { get; set; }
        public virtual EmailStatusEnum EmailStatus { get; set; }
        public virtual string FilePath { get; set; }
        public virtual short IsDeleted { get; set; }
    }

    /// <summary>
    /// 邮件状态
    /// </summary>
    public enum EmailStatusEnum:short
    {
        HasSend=1,
        HasRead=2
    }

    public class EmailTranslationFactory
    {
        public static EmailTranslation Create(long receiverId, long senderId, string theme, string content, string filepath)
        {
            return new EmailTranslation()
            {
                ReceiverId = receiverId,
                SenderId = senderId,
                SendTime = DateTime.Now,
                Theme = theme,
                Content = content,
                EmailStatus = EmailStatusEnum.HasSend,
                FilePath = filepath,
                IsDeleted = Utils.CoreDefaultValue.False
            };
        }
    }
}
