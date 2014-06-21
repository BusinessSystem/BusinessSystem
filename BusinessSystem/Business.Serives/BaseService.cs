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


         public static void SaveUserDefined(UserDefined userDefined)
         {
             userDefinedRepository.Save(userDefined);
         }

         public static UserDefined GetUserDefinedById(long id)
         {
             return userDefinedRepository.GetById(id);
         }

         public static void DeleteUserDefined(long id)
         {
             UserDefined userDefined = userDefinedRepository.GetById(id);
             if (userDefined != null)
             {
                 userDefinedRepository.Delete(userDefined);
             }
         }

         public static IList<UserDefined> GetUserDefineds()
         {
             return userDefinedRepository.GetAllUserDefineds();
         }
     }
}
