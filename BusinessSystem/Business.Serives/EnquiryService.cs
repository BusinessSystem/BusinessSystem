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
        private static IIntentionRepository intentionRepository = new IntentionRepository();
        private static IUserDefinedRepository userDefinedRepository = new UserDefinedRepository();
        private static IManagerRepository managerRepository = new ManagerRepository();
        private static IEmailTranslationRepository emailTranslationRepository = new EmailTranslationRepository();

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

        public void MoveEnquiryToIntention(long enquiryId, long intentionId)
        {
            Enquiry enquiry = enquiryRepository.GetById(enquiryId);
            if (enquiry != null)
            {
                enquiry.IntentionId = 0;
                enquiry.IntentionName = string.Empty;
                Intention intention = intentionRepository.GetById(intentionId);
                if (intention != null)
                {
                    enquiry.IntentionId = intention.Id;
                    enquiry.IntentionName = intention.Description;
                }
                enquiryRepository.Save(enquiry);;
            }
        }

        public void MoveEnquiryToUseDefine(long enquiryId, long useDefineId)
        {
            Enquiry enquiry = enquiryRepository.GetById(enquiryId);
            if (enquiry != null)
            {
                enquiry.UserDefinedId = 0;
                enquiry.UserDefinedName = string.Empty;
                UserDefined userDefined = userDefinedRepository.GetById(useDefineId);
                if (userDefined != null)
                {
                    enquiry.IntentionId = userDefined.Id;
                    enquiry.IntentionName = userDefined.Description;
                }
                enquiryRepository.Save(enquiry);
            }
        }

        public string DeleteEnquiryById(long enquiryId)
        {
            Enquiry enquiry = enquiryRepository.GetById(enquiryId);
            if (enquiry != null)
            {
                enquiry.IsDeleted = Utils.CoreDefaultValue.True;
                enquiryRepository.Save(enquiry);
                return ResponseCode.Ok;
            }
            return ResponseCode.NotFoundData;
        }

        public string IssueEnquiryToChild(long childManagerId, long enquiryId)
        {
            Enquiry enquiry = enquiryRepository.GetById(enquiryId);
            if (enquiry == null)
            {
                return ResponseCode.NotFoundData;
            }
            Manager childManager = managerRepository.GetById(childManagerId);
            if (childManager == null)
            {
                return ResponseCode.NotFoundData;
            }
            enquiry.HandlerId = childManager.Id;
            enquiry.HandlerName = childManager.RealName;
            return ResponseCode.Ok;
        }

        public string ReplyEnquiryTransFollow()
        {
            return ResponseCode.Ok;
        }

        public PageModel<Enquiry> GetEnquiryUnReadEmailPages(long managerId, long intentId, long useDefinedId, int pageIndex, int pageSize)
        {
            return null;
        }

    }
}
