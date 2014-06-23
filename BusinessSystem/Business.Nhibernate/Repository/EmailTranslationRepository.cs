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
        public IList<EmailTranslation> GetEmailTranslations(EmailStatusEnum emailStatus,long receiveId, long intentionId, int pageIndex, int pageSize)
        {
            using (var session = GetSession())
            {
                var query = session.QueryOver<EmailTranslation>();
                query =
                    query.Where(
                        m =>
                            m.SenderStatus == emailStatus &&
                            (m.ReceiverId == receiveId || m.SenderId == receiveId || m.HandlerManagerId == receiveId ||
                             m.FollowId == receiveId));
                if (intentionId != 0)
                {
                    query = query.And(m => m.IntentionId == intentionId);
                }

                return query.Take(pageSize).Skip((pageIndex - 1)*pageSize).List();
            }
        }
    }
}
