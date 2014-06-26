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
        IList<EmailFollow> GetEmailFollowsByTransId(long transId);
        int GetEmailFollowsUnReadByTransId(long transId);
    }

    public interface IEmailTranslationRepository : IRepository<EmailTranslation>
    {

        IList<EmailTranslation> RecycledTranslationList(ManagerTypeEnum managerType, long receiveId,
            int pageIndex, int pageSize, out int totalCount);

        IList<EmailTranslation> GetEmailTranslations(ManagerTypeEnum managerType, EmailStatusEnum emailStatus,
            short isDeleted, long receiveId, long intentionId, int pageIndex, int pageSize, out int totalCount);

        bool HasEnquiryEmailTranslation(long enquiryId);


        IList<EmailTranslation> GetUnReadEmailTranslationsByEnquiryId(ManagerTypeEnum managerType,long enquiryId, EmailStatusEnum emailStatus,
            short isDeleted, long receiveId, long intentionId, int pageIndex, int pageSize, out int totalCount);

        IList<long> GetUnEmailEnquiryIds(long senderId);

    }
}
