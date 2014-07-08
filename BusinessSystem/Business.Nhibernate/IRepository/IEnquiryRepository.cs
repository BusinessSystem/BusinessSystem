using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Core;
using Business.Nhibernate.Base;

namespace Business.Nhibernate.IRepository
{
    public interface IEnquiryRepository : IRepository<Enquiry>
    {
        IList<Enquiry> GetEnquirysBySuperManager(string email, long languageId, int pageindex, int pageSize, out int totalCount);

        IList<Enquiry> GetEnquirysByStatus(string email,long managerId, long languageId, long intentId, long useDefinedId,
            HandlerStatusEnum handlerStatus, int pageindex, int pageSize, out int totalCount);

        IList<Enquiry> GetRecycledEnquirysByStatus(long managerId, long languageId,long intentId, long useDefinedId,
            int pageindex, int pageSize, out int totalCount);

        int GetReadEnquiryCount(long managerId, HandlerStatusEnum handlerStatus);

        IList<Enquiry> GetEnquiriesById(long managerId, long intentId, long useDefinedId, int pageIndex, int pageSize,
            out int totalCount);

        IList<Enquiry> GetUnReadEmailEnquirys(long managerId, long languageId, long intentId, long useDefinedId,
            EmailStatusEnum emailStatus, int pageindex, int pageSize, out int totalCount);

        int GetUnReadEmailEnquiryCount(long managerId, EmailStatusEnum emailStatus);
       
        int GetEnquiryTimesByEmail(string email);
    }

    public interface IEnquiryTransFollowRepository : IRepository<EnquiryTransFollow>
    {
        IList<EnquiryTransFollow> GetEnquiryTransFollowsByEnquiryId(long enquiryId);
    }
}
