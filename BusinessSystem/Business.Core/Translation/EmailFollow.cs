using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Microsoft.SqlServer.Server;

namespace Business.Core
{
    /// <summary>
    /// 邮件跟踪
    /// </summary>
    public class EmailFollow
    {
        public virtual long Id { get; set; }
        public virtual long EmailTransId { get; set; }
        /// <summary>
        /// 跟踪员(发送方)
        /// </summary>
        public virtual long FollowId { get; set; }
        /// <summary>
        /// 处理方
        /// </summary>
        public virtual long HandleManagerId { get; set; }
        public virtual string OriginalContent { get; set; }
        public virtual string TargetContent { get; set; }
        public virtual string OriginalFilePath { get; set; }
        public virtual string TargetFilePath { get; set; }
        public virtual string OriginalLanguage { get; set; }
        public virtual string TargetLanguage { get; set; }
        public virtual DateTime TransTime { get; set; }
    }

    public class EmailFollowFactory
    {
        public static EmailFollow Create(long emailTransId, string originalContent, string originalLanguage, string originalFilePath)
        {
            return new EmailFollow()
            {
                EmailTransId = emailTransId,
                OriginalContent = originalContent,
                OriginalLanguage = originalLanguage,
                TransTime = DateTime.Now,
                OriginalFilePath = originalContent
            };
        }
        public static EmailFollow Create(long emailTransId, string originalContent, string originalLanguage,string targetLanguage,string originalFilePath)
        {
            return new EmailFollow()
            {
                EmailTransId = emailTransId,
                OriginalContent = originalContent,
                OriginalLanguage = originalLanguage,
                TargetLanguage = targetLanguage,
                TransTime = DateTime.Now,
                OriginalFilePath = originalFilePath
            };
        }
    }
}
