using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Business.Core;
using Business.Nhibernate.IRepository;
using Business.Nhibernate.Repository;

namespace Business.Serives
{
    public class TranslationService
    {
        private static IEmailFollowRepository emailFollowRepository = new EmailFollwRepository();
        private static IEmailTranslationRepository emailTranslationRepository = new EmailTranslationRepository();
        private static IIntentionRepository intentionRepository = new IntentionRepository();
        private static IManagerRepository managerRepository = new ManagerRepository();
        private static IEnquiryRepository enquiryRepository = new EnquiryRepository();

        public static string SaveTranslation(EmailTranslation emailTranslation, EmailFollow emailFollow)
        {
            if (string.IsNullOrEmpty(emailTranslation.Theme))
            {
                return ResponseCode.Translation.EmailThemeNullOrEmpty;
            }
            emailTranslationRepository.Save(emailTranslation);
            emailFollow.EmailTransId = emailTranslation.Id;
            emailFollowRepository.Save(emailFollow);
            return ResponseCode.Ok;
        }

        public static PageModel<EmailTranslation> GetEmailTranslations(ManagerTypeEnum managerType,
            EmailStatusEnum emailStatus, short isDeleted, long receiveId,
            long intentionId, int pageIndex, int pageSize)
        {
            int totalCount = 0;
            IList<EmailTranslation> emailTranslations =
                emailTranslationRepository.GetEmailTranslations(managerType, emailStatus, isDeleted, receiveId,
                    intentionId, pageIndex, pageSize, out totalCount);
            return new PageModel<EmailTranslation>(emailTranslations, pageIndex, pageSize, totalCount);
        }


        public static PageModel<EmailTranslation> GetRecycledTranslationList(ManagerTypeEnum managerType, long receiveId,
            int pageIndex, int pageSize)
        {
            int totalCount = 0;
            IList<EmailTranslation> emailTranslations =
                emailTranslationRepository.RecycledTranslationList(managerType, receiveId,
                    pageIndex, pageSize, out totalCount);
            return new PageModel<EmailTranslation>(emailTranslations, pageIndex, pageSize, totalCount);
        }

        /// <summary>
        /// 移动到意向客户
        /// </summary>
        /// <param name="id"></param>
        public static void MoveToIntentionCustom(long translationId, long intentId)
        {
            EmailTranslation emailTranslation = emailTranslationRepository.GetById(translationId);
            emailTranslation.IntentionId = (int) intentId;
            emailTranslation.IntentionDescription = intentionRepository.GetById(intentId).Description;
            emailTranslationRepository.Save(emailTranslation);
        }

        /// <summary>
        ///返回给总账号 
        /// </summary>
        public static void BackTranslationToMain(long translationId, Manager currentManager)
        {
            if (currentManager.ParentId == 0)
            {
                return;
            }
            EmailTranslation emailTranslation = emailTranslationRepository.GetById(translationId);
            Manager mainManager = managerRepository.GetById(currentManager.ParentId);
            if (currentManager.ManagerType == ManagerTypeEnum.Super)
            {
                emailTranslation.HandlerManagerId = mainManager.Id;
                emailTranslation.HandlerManagerName = mainManager.RealName;

            }
            if (currentManager.ManagerType == ManagerTypeEnum.Common)
            {
                emailTranslation.FollowId = mainManager.Id;
                emailTranslation.FollowName = mainManager.RealName;
            }

            emailTranslationRepository.Save(emailTranslation);
        }

        public static string DeleteTranslation(long translationId, long managerId)
        {
            EmailTranslation emailTranslation = emailTranslationRepository.GetById(translationId);
            if (emailTranslation.FollowId == managerId || emailTranslation.ReceiverId == managerId ||
                emailTranslation.SenderId == managerId)
            {
                emailTranslation.IsDeleted = Utils.CoreDefaultValue.True;
                emailTranslationRepository.Save(emailTranslation);
                return ResponseCode.Ok;
            }
            return ResponseCode.Translation.NoPermissionDeleteTrans; //无权
        }

        public static IList<EmailFollow> GetEmailFollows(long translationId)
        {
            return emailFollowRepository.GetEmailFollowsByTransId(translationId);
        }

        public static EmailTranslation GeEmailTranslationById(long translationId)
        {
            return emailTranslationRepository.GetById(translationId);
        }

        public static string CreateEmailFollow(long translationId,string originalContent,string filePath,string targetLanguage="")
        {
            EmailTranslation emailTranslation = emailTranslationRepository.GetById(translationId);
            if (emailTranslation != null)
            {
                EmailFollow emailFollow = EmailFollowFactory.Create(translationId, originalContent,
                    emailTranslation.OriginalLanguage,emailTranslation.TargetLanguage,filePath);
                emailFollow.FollowId = emailTranslation.FollowId;
                emailFollow.HandleManagerId = emailTranslation.HandlerManagerId;
                emailFollow.TargetLanguage = targetLanguage;
                emailTranslation.ReceiverStatus=EmailStatusEnum.UnRead;
                emailTranslation.TargetLanguage = targetLanguage;
                emailTranslationRepository.Save(emailTranslation);
                emailFollowRepository.Save(emailFollow);
               return ResponseCode.Ok;
            }
            return ResponseCode.NotFoundData;
        }

        public static string ReplyTranslation(long followId,long translationId,string targetContent,Manager currentManager,string filePath)
        {
            EmailFollow emailFollow = emailFollowRepository.GetById(followId);
            EmailTranslation emailTranslation = emailTranslationRepository.GetById(translationId);
            if (emailFollow != null)
            {
                emailFollow.TargetContent = targetContent;
                emailFollow.HandleManagerId = currentManager.Id;
                emailFollow.TargetFilePath = filePath;
                emailTranslation.ReceiverStatus = EmailStatusEnum.HasRead;
                emailTranslation.FollowTimes = emailTranslation.FollowTimes + 1;
                emailTranslation.SenderStatus = EmailStatusEnum.UnRead;
                emailTranslationRepository.Save(emailTranslation);
                emailFollowRepository.Save(emailFollow);
                if (emailTranslation.EnquiryId > 0)
                {
                    Enquiry enquiry = enquiryRepository.GetById(emailTranslation.EnquiryId);
                    if (enquiry != null)
                    {
                        enquiry.FollowUpTimes = enquiry.FollowUpTimes + 1;
                        enquiry.EmailStatus = EmailStatusEnum.UnRead;
                        enquiryRepository.Save(enquiry);
                    }
                }
                return ResponseCode.Ok;
            }
            return ResponseCode.NotFoundData;
        }

        public static  EmailTranslation GeEmailTranslationByEnquiryId(long enquiryId)
        {
            return emailTranslationRepository.GeEmailTranslationByEnquiryId(enquiryId);
        }

        public static void ChangeEmailTranslationStatus(EmailTranslation emailTranslation,Manager currentManager)
        {
            if (emailTranslation != null)
            {
                if (emailTranslation.ReceiverStatus == EmailStatusEnum.UnRead &&
                    currentManager.ManagerType == ManagerTypeEnum.Super && emailTranslation.HandlerManagerId==currentManager.Id)
                {
                    emailTranslation.ReceiverStatus = EmailStatusEnum.HasRead;
                }
                if (emailTranslation.SenderStatus == EmailStatusEnum.UnRead &&
                    currentManager.ManagerType == ManagerTypeEnum.Common &&
                    emailTranslation.FollowId == currentManager.Id)
                {
                    emailTranslation.SenderStatus = EmailStatusEnum.HasRead;
                }
                emailTranslationRepository.Save(emailTranslation);
                if (emailTranslation.EnquiryId > 0)
                {
                    Enquiry enquiry = enquiryRepository.GetById(emailTranslation.EnquiryId);
                    if (enquiry != null)
                    {
                        enquiry.EmailStatus = EmailStatusEnum.HasRead;
                        enquiryRepository.Save(enquiry);
                    }
                }
            }
        }

        public static int GetEmailTranslationsCount(ManagerTypeEnum managerType, EmailStatusEnum emailStatus,
            long managerId)
        {
           return  emailTranslationRepository.GetEmailTranslationsCount(managerType, emailStatus, managerId);
        }

        public static void IssueEmailToChildManager(long translationId,long managerId)
        {
            EmailTranslation emailTranslation = emailTranslationRepository.GetById(translationId);
            Manager childManager = managerRepository.GetById(managerId);
            if (emailTranslation != null && childManager != null)
            {
                emailTranslation.HandlerManagerId = managerId;
                emailTranslation.HandlerManagerName = childManager.RealName;
                emailTranslationRepository.Save(emailTranslation);
            }
        }

        public static void RecoveryEmailTranlations(long translationId)
        {
            EmailTranslation emailTranslation = emailTranslationRepository.GetById(translationId);
            if (emailTranslation != null)
            {
                emailTranslation.IsDeleted = Utils.CoreDefaultValue.False;
                emailTranslationRepository.Save(emailTranslation);
            }
        }
    }
}
