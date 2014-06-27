using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Core
{
    /// <summary>
    /// 商家主站
    /// </summary>
    public class ManagerMainSite
    {
        public virtual long Id { get; set; }

        public virtual long ManagerId { get; set; }
        public virtual string ManagerName { get; set; }
        public virtual string SiteName { get; set; }

        public virtual string SiteUrl { get; set; }

        public virtual string LanguageName { get; set; }

        public virtual long LanguageId { get; set; }

        public virtual DateTime OperateTime { get; set; }

        public virtual string Operator { get; set; }
       

    }

    public class ManagerMainSiteFactory
    {
        public static ManagerMainSite Create(long managerId, string managerName, string siteName, string siteUrl,
            long languageId, string languageName,string operate)
        {
            return new ManagerMainSite()
            {
                ManagerId = managerId,
                ManagerName=managerName,
                SiteName = siteName,
                SiteUrl = siteUrl,
                LanguageName = languageName,
                LanguageId = languageId,
                OperateTime = DateTime.Now,
                Operator = operate
            };
        }
    }
}
