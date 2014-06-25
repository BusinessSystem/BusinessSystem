using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public virtual string HandlerManagerName { get; set; }//对方处理人
        public virtual long  HandlerManagerId { get; set; }//对方处理人
        public virtual long SenderId { get; set; }
        public virtual string LanguageName { get; set; }
        public virtual string SenderName { get; set; }
        public virtual long FollowId { get; set; }//跟踪员
        public virtual string  FollowName { get; set; }//跟踪员
        public virtual DateTime SendTime { get; set; }
        public virtual DateTime ReadTime { get; set; }
        public virtual string Theme { get; set; }
        public virtual string Content { get; set; }
        public virtual EmailStatusEnum ReceiverStatus { get; set; }
        public virtual EmailStatusEnum SenderStatus { get; set; }
        public virtual int IntentionId { get; set; }
        public virtual string IntentionDescription { get; set; }
        public virtual string FilePath { get; set; }
        public virtual short IsDeleted { get; set; }
        public virtual int  FollowTimes { get; set; }
        public virtual string OriginalLanguage { get; set; }
        public virtual string TargetLanguage { get; set; }
    }

    /// <summary>
    /// 邮件状态
    /// </summary>
    public enum EmailStatusEnum : short
    {
        [Description("未读")] UnRead = 1,
        [Description("已读")] HasRead = 2,

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
                HandlerManagerId = receiverId,
                FollowId = senderId,
                SenderStatus = EmailStatusEnum.HasRead,
                ReceiverStatus = EmailStatusEnum.UnRead,
                FilePath = filepath,
                IsDeleted = Utils.CoreDefaultValue.False
            };
        }
        public static EmailTranslation Create(long receiverId, string language,string senderName, long senderId, string theme, string content, string filepath)
        {
            return new EmailTranslation()
            {
                ReceiverId = receiverId,
                SenderId = senderId,
                SendTime = DateTime.Now,
                Theme = theme,
                Content = content,
                HandlerManagerId = receiverId,
                FollowId = senderId,
                SenderStatus = EmailStatusEnum.HasRead,
                ReceiverStatus = EmailStatusEnum.UnRead,
                FilePath = filepath,
                IsDeleted = Utils.CoreDefaultValue.False,
                LanguageName = language,
                SenderName = senderName,
                FollowTimes = 1
            };
        }
    }
}
