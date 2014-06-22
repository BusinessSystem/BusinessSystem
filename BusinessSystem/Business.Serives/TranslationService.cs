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
    }
}
