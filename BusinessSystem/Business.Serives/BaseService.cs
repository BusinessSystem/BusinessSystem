using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Core;
using Business.Nhibernate.IRepository;
using Business.Nhibernate.Repository;

namespace Business.Serives
{
     public  class BaseService
     {
         private static  IUserDefinedRepository userDefinedRepository=new UserDefinedRepository();
         private static  IIntentionRepository intentionRepository = new IntentionRepository();

         public static void SaveUserDefined(UserDefined userDefined)
         {
             userDefinedRepository.Save(userDefined);
         }
         public static void SaveIntention(Intention intention)
         {
             intentionRepository.Save(intention);
         }

         public static Intention GetIntentionById(long id)
         {
             return intentionRepository.GetById(id);
         }

         public static void DeleteIntention(long id)
         {
             Intention intention = intentionRepository.GetById(id);
             if (intention != null)
             {
                 intentionRepository.Delete(intention);
             }
         }

         public static IList<Intention> GetIntentions()
         {
             return intentionRepository.GetAllIntentions();
         }
     }
}
