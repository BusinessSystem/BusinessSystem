using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public static string SaveTranslation(EmailTranslation emailTranslation,EmailFollow emailFollow)
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

        public static IList<EmailTranslation> GetEmailTranslations(EmailStatusEnum emailStatus,long intentionId,int pageIndex,int pageSize)
        {
           return  emailTranslationRepository.GetEmailTranslations(emailStatus, intentionId, pageIndex, pageSize);
        }

        /// <summary>
        /// 移动到意向客户
        /// </summary>
        /// <param name="id"></param>
        public static void MoveToIntentionCustom(long translationId,long intentId)
        {
            EmailTranslation emailTranslation = emailTranslationRepository.GetById(translationId);
            emailTranslation.IntentionId = (int)intentId;
            emailTranslation.IntentionDescription = intentionRepository.GetById(intentId).Description;
            emailTranslationRepository.Save(emailTranslation);
        }

        /// <summary>
        ///返回给总账号 
        /// </summary>
        public static void BackTranslationToMain(long translationId, long parentId)
        {
            EmailTranslation emailTranslation = emailTranslationRepository.GetById(translationId);
        }
    }
}
