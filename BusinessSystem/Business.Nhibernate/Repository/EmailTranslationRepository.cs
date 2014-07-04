using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Core;
using Business.Nhibernate.Base;
using Business.Nhibernate.IRepository;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace Business.Nhibernate.Repository
{
    public class EmailTranslationRepository:Repository<EmailTranslation>,IEmailTranslationRepository
    {

        public IList<EmailTranslation> RecycledTranslationList(ManagerTypeEnum managerType,
            long receiveId, int pageIndex, int pageSize, out int totalCount)
        {
            using (var session = GetSession())
            {
                var query = session.QueryOver<EmailTranslation>();
                query = query.Where(m => m.IsDeleted == Utils.CoreDefaultValue.True);
                if (managerType == ManagerTypeEnum.Super)
                {
                    query = query.And(m => m.HandlerManagerId == receiveId || m.ReceiverId == receiveId);
                }
                if (managerType == ManagerTypeEnum.Common)
                {
                    query = query.And(m => m.SenderId == receiveId || m.FollowId == receiveId);
                    ;
                }
                totalCount = query.RowCount();
                return query.Take(pageSize).Skip((pageIndex - 1)*pageSize).List();
            }
        }

        public int GetEmailTranslationsCount(ManagerTypeEnum managerType, EmailStatusEnum emailStatus,long managerId)
        {
            using (var session = GetSession())
            {
                var query = session.QueryOver<EmailTranslation>();
                query = query.Where(m => m.IsDeleted == Utils.CoreDefaultValue.False);

                if (managerType == ManagerTypeEnum.Common)
                {
                    if (emailStatus == EmailStatusEnum.UnRead)
                    {
                        query = query.And(m => m.EnquiryId < 0);
                    }
                    query =
                        query.And(
                            m => m.SenderStatus == emailStatus && (m.SenderId == managerId || m.FollowId == managerId));
                }
                if (managerType == ManagerTypeEnum.Super)
                {
                    query =
                        query.And(
                            m =>
                                m.ReceiverStatus == emailStatus &&
                                (m.HandlerManagerId == managerId || m.ReceiverId == managerId));
                }
                return query.RowCount();
            }
        }

        public IList<EmailTranslation> GetEmailTranslations(ManagerTypeEnum managerType, EmailStatusEnum emailStatus, short isDeleted, long receiveId, long intentionId, int pageIndex, int pageSize, out int totalCount)
        {
            using (var session = GetSession())
            {
                var query = session.QueryOver<EmailTranslation>();
                query = query.Where(m => m.IsDeleted == isDeleted);
                if (managerType == ManagerTypeEnum.Super)
                {
                    query =
                        query.And(
                            m =>
                                m.ReceiverStatus == emailStatus &&
                                (m.ReceiverId == receiveId || m.HandlerManagerId == receiveId));

                }
                if (managerType == ManagerTypeEnum.Common)
                {
                    if (emailStatus == EmailStatusEnum.UnRead)
                    {
                        query = query.And(m => m.EnquiryId > 0);
                    }
                    query =
                        query.And(
                            m =>
                                m.SenderStatus == emailStatus &&
                                (m.SenderId == receiveId || m.FollowId == receiveId));
                }
                if (intentionId != 0)
                {
                    query = query.And(m => m.IntentionId == intentionId);
                }
                totalCount = query.RowCount();
                return query.Take(pageSize).Skip((pageIndex - 1) * pageSize).List();
            }
        }

        public bool HasEnquiryEmailTranslation(long enquiryId)
        {
            using (var session = GetSession())
            {
                return session.QueryOver<EmailTranslation>().Where(m => m.EnquiryId == enquiryId).RowCount() > 0;
            }
        }


        public IList<EmailTranslation> GetUnReadEmailTranslationsByEnquiryId(ManagerTypeEnum managerType, long enquiryId,
            EmailStatusEnum emailStatus, short isDeleted, long receiveId, long intentionId, int pageIndex, int pageSize,
            out int totalCount)
        {
            using (var session = GetSession())
            {
                var query = session.QueryOver<EmailTranslation>();
                query = query.Where(m => m.IsDeleted == isDeleted && m.EnquiryId == enquiryId);
                if (managerType == ManagerTypeEnum.Super)
                {
                    query =
                        query.And(
                            m =>
                                m.ReceiverStatus == emailStatus &&
                                (m.ReceiverId == receiveId || m.HandlerManagerId == receiveId));

                }
                if (managerType == ManagerTypeEnum.Common)
                {
                    query =
                        query.And(
                            m =>
                                m.SenderStatus == emailStatus &&
                                (m.SenderId == receiveId || m.FollowId == receiveId));
                }
                if (intentionId != 0)
                {
                    query = query.And(m => m.IntentionId == intentionId);
                }
                totalCount = query.RowCount();
                return query.Take(pageSize).Skip((pageIndex - 1)*pageSize).List();
            }
        }

        public IList<long> GetUnEmailEnquiryIds(long senderId)
        {
            using (var session = GetSession())
            {
                return
                    session.QueryOver<EmailTranslation>()
                        .Where(
                            m =>
                                m.SenderStatus == EmailStatusEnum.UnRead && m.EnquiryId > 0 &&
                                (m.SenderId == senderId || m.FollowId == senderId))
                        .Select(m => m.EnquiryId).List<long>();
            }
        }


        public EmailTranslation GeEmailTranslationByEnquiryId(long enquiryId)
        {
            using (var session = GetSession())
            {
                if (enquiryId == 0)
                {
                    return null;
                }
                return
                    session.QueryOver<EmailTranslation>().Where(m => m.EnquiryId == enquiryId).Take(1).SingleOrDefault();
            }
        }
    }
}
