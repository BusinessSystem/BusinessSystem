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
        IList<EmailTranslation> GetHasReadEmailTranslations(ManagerTypeEnum managerType, EmailStatusEnum emailStatus, short isDeleted, long receiveId, long intentionId, int pageIndex, int pageSize, out int totalCount);

        IList<EmailTranslation> RecycledTranslationList(short isDeleted, long receiveId, long intentionId,
            int pageIndex, int pageSize, out int totalCount);
    }
}
