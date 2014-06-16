using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Core;
using Business.Nhibernate.IRepository;
using Business.Nhibernate.Repository;

namespace Business.Serives
{
     public  class ManageService
     {
         private static  IManagerRepository managerRepository=new ManagerRepository();

         

         public static  void Save(Manager manager)
         {
             managerRepository.Save(manager);
         }
     }
}
