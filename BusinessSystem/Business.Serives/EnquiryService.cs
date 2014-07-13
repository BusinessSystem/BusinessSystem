using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using Business.Core;
using Business.Nhibernate.IRepository;
using Business.Nhibernate.Repository;
using Business.Utils;

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
        private static IManagerProductRepository managerProductRepository = new ManagerProductRepository();
        private static IManagerMainSiteRepository managerMainSiteRepository = new ManagerMainSiteRepository();
      

        public static IList<EnquiryTransFollow> GeEnquiryTransFollowsByEnquiryId(long enquiryId)
        {
            return enquiryTransFollowRepository.GetEnquiryTransFollowsByEnquiryId(enquiryId);
        }

        public static PageModel<Enquiry> GetEnquiryPages(string email,long managerId,long languageId, long intentId, long useDefinedId,
            HandlerStatusEnum handlerStatus, int pageindex, int pageSize)
        {
            int totalCount = 0;
            IList<Enquiry> enquiries = enquiryRepository.GetEnquirysByStatus(email,managerId, languageId, intentId, useDefinedId,
                handlerStatus, pageindex, pageSize, out totalCount);
            return new PageModel<Enquiry>(enquiries, pageindex, pageSize, totalCount);
        }

        public static PageModel<Enquiry> GetRecycledEnquiryPages(long managerId,long languageId, long intentId, long useDefinedId,
            int pageindex, int pageSize)
        {
            int totalCount = 0;
            IList<Enquiry> enquiries = enquiryRepository.GetRecycledEnquirysByStatus(managerId,languageId, intentId, useDefinedId,
                pageindex, pageSize, out totalCount);
            return new PageModel<Enquiry>(enquiries, pageindex, pageSize, totalCount);
        }

        public static int GetReadEnquiryCount(long managerId, HandlerStatusEnum handlerStatus)
        {
            return enquiryRepository.GetReadEnquiryCount(managerId, handlerStatus);
        }

        public static int GetUnReadEmailEnquiryCount(long managerId, EmailStatusEnum emailStatus)
        {
            return enquiryRepository.GetUnReadEmailEnquiryCount(managerId, emailStatus);
        }

        public static void MoveEnquiryToIntention(long enquiryId, long intentionId)
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
                enquiryRepository.Save(enquiry);
                ;
            }
        }

        public static void MoveEnquiryToUseDefine(long enquiryId, long useDefineId)
        {
            Enquiry enquiry = enquiryRepository.GetById(enquiryId);
            if (enquiry != null)
            {
                enquiry.UserDefinedId = 0;
                enquiry.UserDefinedName = string.Empty;
                UserDefined userDefined = userDefinedRepository.GetById(useDefineId);
                if (userDefined != null)
                {
                    enquiry.UserDefinedId = userDefined.Id;
                    enquiry.UserDefinedName = userDefined.Description;
                }
                enquiryRepository.Save(enquiry);
            }
        }
        
        public static string DeleteEnquiryById(long enquiryId,Manager currentManager)
        {
            Enquiry enquiry = enquiryRepository.GetById(enquiryId);
            if (currentManager.Id == enquiry.ReceiverId || currentManager.Id == enquiry.HandlerId)
            {
                if (enquiry != null)
                {
                    enquiry.IsDeleted = Utils.CoreDefaultValue.True;
                    enquiryRepository.Save(enquiry);
                    return ResponseCode.Ok;
                }
            }
            return ResponseCode.NotFoundData;
        }

        /// <summary>
        /// 系统管理员看到的用户的询盘
        /// </summary>
        /// <param name="email"></param>
        /// <param name="languageId"></param>
        /// <param name="pageindex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PageModel<Enquiry> GetEnquirysBySuperManager(string email, long languageId, int pageindex, int pageSize)
        {
            int totalCount = 0;
            IList<Enquiry> enquiries = enquiryRepository.GetEnquirysBySuperManager(email, languageId, pageindex,
                pageSize, out totalCount);
            PageModel<Enquiry> pageModel = new PageModel<Enquiry>(enquiries, pageindex, pageSize, totalCount);
            return pageModel;
        }

        public static string RecoveryEnquiry(long enquiryId, Manager currentManager)
        {
            Enquiry enquiry = enquiryRepository.GetById(enquiryId);
            if (currentManager.Id == enquiry.ReceiverId || currentManager.Id == enquiry.HandlerId)
            {
                if (enquiry != null)
                {
                    enquiry.IsDeleted = Utils.CoreDefaultValue.False;
                    enquiryRepository.Save(enquiry);
                    return ResponseCode.Ok;
                }
            }
            return ResponseCode.NotFoundData;
        }

        public static string IssueEnquiryToChild(long childManagerId, long enquiryId)
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
            enquiryRepository.Save(enquiry);
            return ResponseCode.Ok;
        }

        public static string ReplyEnquiryTransFollow()
        {
            return ResponseCode.Ok;
        }

        public static PageModel<Enquiry> GetEnquiryUnReadEmailPages(long managerId, long languageId, long intentId,
            long useDefinedId,
            EmailStatusEnum emailStatus, int pageindex, int pageSize)
        {
            int totalCount = 0;
            IList<Enquiry> enquiries = enquiryRepository.GetUnReadEmailEnquirys(managerId, languageId, intentId,
                useDefinedId,
                emailStatus, pageindex, pageSize, out totalCount);
            return new PageModel<Enquiry>(enquiries, pageindex, pageSize, totalCount);
        }

        public static Enquiry GetEnquiryById(long enquiryId)
        {
            return enquiryRepository.GetById(enquiryId);
        }

        public static void ChangeEnquiryStatus(Manager currentManager, Enquiry enquiry)
        {
            if (enquiry.HandlerId == currentManager.Id)
            {
                enquiry.HandlerStatus = HandlerStatusEnum.HasRead;
                enquiryRepository.Save(enquiry);
            }
        }

        //:TODO 询盘写入数据
        public static void EnquirySave(string ipString, string email, string content, string productName,
            string yourName, string company, string tel, string msn, string language, string country, string recievedId)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(content))
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(tel))
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(yourName))
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(company))
            {

            }
            if (string.IsNullOrWhiteSpace(msn))
            {
                return;
            }
            if (!CheckTools.IsValidEmail(email))
            {
                return;
            }
            if (!CheckTools.IsAllNumber(tel))
            {
                return;
            }

            //add by luoyaqi 20140712
            Manager manger = managerRepository.GetManagerByUserName(recievedId);
            if (manger == null)
            {
                return;
            }
            
            
            //comment by luoyaqi 20140712
            long languageId = 0;
            ManagerProduct managerProduct = managerProductRepository.GetManagerProductByUrl(productName);
            if (managerProduct != null)
            {
                ManagerMainSite managerMainSite = managerMainSiteRepository.GetById(managerProduct.ManagerMainSiteId);
                if (managerMainSite != null)
                {
                    languageId = managerMainSite.LanguageId;
                    language = managerMainSite.LanguageName;
                }
            }


            //Enquiry enquiry = EnquiryFactory.Create(ipString, email, content, productName, yourName, company, tel, msn,
            //    managerMainSite.LanguageName, managerMainSite.LanguageId, "未知", managerMainSite.ManagerId,
            //    managerMainSite.ManagerName);
            
            //add by luoyaqi 20140712 (此处的语言还需要处理)
            Enquiry enquiry = EnquiryFactory.Create(ipString, email, content, productName, yourName, company, tel, msn,
                language, languageId, country, manger.Id, manger.UserName);
            enquiry.EnquiryTimes = enquiryRepository.GetEnquiryTimesByEmail(email) + 1;
            enquiryRepository.Save(enquiry);
        }
    }
}
