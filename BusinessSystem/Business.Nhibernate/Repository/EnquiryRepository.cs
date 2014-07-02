using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Core;
using Business.Nhibernate.Base;
using Business.Nhibernate.IRepository;

namespace Business.Nhibernate.Repository
{
    public class EnquiryRepository:Repository<Enquiry>,IEnquiryRepository
    {
        public IList<Enquiry> GetEnquirysByStatus(long managerId, long languageId, long intentId, long useDefinedId, HandlerStatusEnum handlerStatus, int pageindex, int pageSize, out int totalCount)
        {
            using (var session = GetSession())
            {
                var query = session.QueryOver<Enquiry>().Where(m => m.IsDeleted == Utils.CoreDefaultValue.False)
                    .And(m => m.ReceiverId == managerId || m.HandlerId == managerId)
                    .And(m => m.HandlerStatus == handlerStatus);
                if (languageId != 0)
                {
                     //query=query.And(m=>m)
                }
                if (intentId != 0)
                {
                    query = query.And(m => m.IntentionId == intentId);
                }
                if (useDefinedId != 0)
                {
                    query = query.And(m => m.UserDefinedId == useDefinedId);
                }
                totalCount = query.RowCount();
                return query.OrderBy(m => m.Id).Desc.Take(pageSize)
                    .Skip((pageindex - 1)*pageSize)
                    .List();
            }
        }

        public IList<Enquiry> GetRecycledEnquirysByStatus(long managerId,long languageId, long intentId, long useDefinedId,
            int pageindex, int pageSize, out int totalCount)
        {
            using (var session = GetSession())
            {
                var query = session.QueryOver<Enquiry>().Where(m => m.IsDeleted == Utils.CoreDefaultValue.True)
                    .And(m => m.ReceiverId == managerId || m.HandlerId == managerId);
                if (intentId != 0)
                {
                    query = query.And(m => m.IntentionId == intentId);
                }
                if (useDefinedId != 0)
                {
                    query = query.And(m => m.UserDefinedId == useDefinedId);
                }
                totalCount = query.RowCount();
                return query.OrderBy(m=>m.Id).Desc.Take(pageSize)
                    .Skip((pageindex - 1)*pageSize)
                    .List();
            }
        }


        public int GetReadEnquiryCount(long managerId, HandlerStatusEnum handlerStatus)
        {
            using (var session = GetSession())
            {
                return session.QueryOver<Enquiry>().Where(m => m.IsDeleted == Utils.CoreDefaultValue.False)
                    .And(m => m.HandlerStatus == handlerStatus)
                    .And(m => m.ReceiverId == managerId || m.HandlerId == managerId).RowCount();
            }
        }

        public int GetUnReadEmailEnquiryCount(long managerId, EmailStatusEnum  emailStatus)
        {
            using (var session = GetSession())
            {
                return session.QueryOver<Enquiry>().Where(m => m.IsDeleted == Utils.CoreDefaultValue.False)
                    .And(m => m.EmailStatus == emailStatus)
                    .And(m => m.ReceiverId == managerId || m.HandlerId == managerId).RowCount();
            }
        }


        public IList<Enquiry> GetEnquiriesById(long managerId, long intentId, long useDefinedId, int pageIndex, int pageSize, out int totalCount)
        {
            using (var session = GetSession())
            {
                var query =
                    session.QueryOver<Enquiry>()
                        .Where(
                            m =>
                                m.EmailStatus == EmailStatusEnum.UnRead &&
                                (m.ReceiverId == managerId || m.HandlerId == managerId));
                if (intentId != 0)
                {
                    query = query.And(m => m.IntentionId == intentId);
                }
                if (useDefinedId != 0)
                {
                    query = query.And(m => m.UserDefinedId == useDefinedId);
                }
                totalCount = query.RowCount();
                return query.OrderBy(m => m.Id).Desc
                    .Take(pageSize)
                    .Skip((pageIndex - 1)*pageSize).List();
            }
        }

        public IList<Enquiry> GetUnReadEmailEnquirys(long managerId, long languageId, long intentId, long useDefinedId, EmailStatusEnum emailStatus,int pageindex, int pageSize, out int totalCount)
        {
            using (var session = GetSession())
            {
                var query = session.QueryOver<Enquiry>().Where(m => m.IsDeleted == Utils.CoreDefaultValue.False)
                    .And(m => m.ReceiverId == managerId || m.HandlerId == managerId)
                    .And(m => m.EmailStatus == emailStatus);
                if (languageId != 0)
                {
                    //query=query.And(m=>m)
                }
                if (intentId != 0)
                {
                    query = query.And(m => m.IntentionId == intentId);
                }
                if (useDefinedId != 0)
                {
                    query = query.And(m => m.UserDefinedId == useDefinedId);
                }
                totalCount = query.RowCount();
                return query.OrderBy(m => m.Id).Desc.Take(pageSize)
                    .Skip((pageindex - 1)*pageSize)
                    .List();
            }
        }


        public int GetEnquiryTimesByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return 0;
            }
            using (var session = GetSession())
            {
                return session.QueryOver<Enquiry>().Where(m => m.PurchaserEmail == email).RowCount();
            }
        }
    }
} 
