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

         public static void Save(UserDefined userDefined)
         {
             userDefinedRepository.Save(userDefined);
         }
     }
}
