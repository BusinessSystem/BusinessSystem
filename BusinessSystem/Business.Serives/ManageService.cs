﻿using System;
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
         
         public static string Save(Manager manager)
         {
             if (string.IsNullOrEmpty(manager.UserName))
             {
                 return ResponseCode.Managaer.UserNullOrEmpty;
             }
             if (string.IsNullOrEmpty(manager.Password))
             {
                 return ResponseCode.Managaer.PasswordNullOrEmpty;
             }
             if (manager.Language==0)
             {
                 return ResponseCode.Managaer.LanguageNullOrEmpty;
             }
             if (string.IsNullOrEmpty(manager.RealName))
             {
                 return ResponseCode.Managaer.RealNameNullOrEmpty;
             }
             if (string.IsNullOrEmpty(manager.Company))
             {
                 return ResponseCode.Managaer.CompanyNullOrEmpty;
             }
             if (managerRepository.IsExist(manager))
             {
                 return ResponseCode.Managaer.UserNameHasExist;
             }
             managerRepository.Save(manager);
             return ResponseCode.Ok;
         }
     }
}
