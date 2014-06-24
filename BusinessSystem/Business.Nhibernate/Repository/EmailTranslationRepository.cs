using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Core;
using Business.Nhibernate.Base;
using Business.Nhibernate.IRepository;

namespace Business.Nhibernate.Repository
{
    public class EmailTranslationRepository:Repository<EmailTranslation>,IEmailTranslationRepository
    {
        public IList<EmailTranslation> GetEmailTranslations(EmailStatusEnum emailStatus, short isDeleted,long receiveId, long intentionId, int pageIndex, int pageSize, out int totalCount)
        {
            using (var session = GetSession())
            {
                var query = session.QueryOver<EmailTranslation>();
                query =
                    query.Where(
                        m =>
                            m.SenderStatus == emailStatus && m.IsDeleted == isDeleted &&
                            (m.HandlerManagerId == receiveId ||
                             m.FollowId == receiveId));
                if (intentionId != 0)
                {
                    query = query.And(m => m.IntentionId == intentionId);
                }
                totalCount = query.RowCount();
                return query.Take(pageSize).Skip((pageIndex - 1)*pageSize).List();
            }
        }

        public IList<EmailTranslation> RecycledTranslationList(short isDeleted, long receiveId, long intentionId,
            int pageIndex, int pageSize, out int totalCount)
        {
            using (var session = GetSession())
            {
                var query = session.QueryOver<EmailTranslation>();
                query = query.Where(m => m.IsDeleted == isDeleted &&
                                         (m.HandlerManagerId == receiveId || m.FollowId == receiveId));
                if (intentionId != 0)
                {
                    query = query.And(m => m.IntentionId == intentionId);
                }
                totalCount = query.RowCount();
                return query.Take(pageSize).Skip((pageIndex - 1)*pageSize).List();
            }
        }
    }
}
