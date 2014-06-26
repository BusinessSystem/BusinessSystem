using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Core;
using Business.Nhibernate.IRepository;
using Business.Nhibernate.Repository;

namespace Business.Serives
{
    public class EnquiryService
    {
        private static IEnquiryRepository enquiryRepository = new EnquiryRepository();
        private static IEnquiryTransFollowRepository enquiryTransFollowRepository = new EnquiryTransFollowRepository();

        public static IList<EnquiryTransFollow> GeEnquiryTransFollowsByEnquiryId(long enquiryId)
        {
            return enquiryTransFollowRepository.GetEnquiryTransFollowsByEnquiryId(enquiryId);
        }


        public static PageModel<Enquiry> GetEnquiryPages(long managerId, long intentId, long useDefinedId,
            HandlerStatusEnum handlerStatus, int pageindex, int pageSize)
        {
            int totalCount = 0;
            IList<Enquiry> enquiries = enquiryRepository.GetEnquirysByStatus(managerId, intentId, useDefinedId,
                handlerStatus, pageindex, pageSize, out totalCount);
            return new PageModel<Enquiry>(enquiries, pageindex, pageSize, totalCount);
        }

        public static PageModel<Enquiry> GetRecycledEnquiryPages(long managerId, long intentId, long useDefinedId,
            int pageindex, int pageSize)
        {
            int totalCount = 0;
            IList<Enquiry> enquiries = enquiryRepository.GetRecycledEnquirysByStatus(managerId, intentId, useDefinedId,
                pageindex, pageSize, out totalCount);
            return new PageModel<Enquiry>(enquiries, pageindex, pageSize, totalCount);
        }

        public static int GetReadEnquiryCount(long managerId, HandlerStatusEnum handlerStatus)
        {
            return enquiryRepository.GetReadEnquiryCount(managerId, handlerStatus);
        }
    }
}
