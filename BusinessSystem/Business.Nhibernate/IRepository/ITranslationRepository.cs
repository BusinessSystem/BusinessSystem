using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Core;
using Business.Nhibernate.Base;

namespace Business.Nhibernate.IRepository
{
    public interface IEmailFollowRepository : IRepository<EmailFollow>
    {

    }

    public interface IEmailTranslationRepository : IRepository<EmailTranslation>
    {
        IList<EmailTranslation> GetEmailTranslations(EmailStatusEnum emailStatus,long receiveId, long intentionId, int pageIndex, int pageSize,out int totalCount);
    }
}
