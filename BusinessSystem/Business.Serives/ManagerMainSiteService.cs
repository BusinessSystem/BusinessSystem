using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Nhibernate.IRepository;
using Business.Nhibernate.Repository;
using Business.Core;

namespace Business.Serives
{
    public class ManagerMainSiteService
    {
        private static IManagerMainSiteRepository managerMainSiteRepository = new ManagerMainSiteRepository();

        public static List<ManagerMainSite> GetManagerMainSitesByManagerId(long managerId)
        {
            return managerMainSiteRepository.GetManagerMainSitesByManagerId(managerId).ToList<ManagerMainSite>();
        }
    }
}
