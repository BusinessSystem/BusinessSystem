using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Business.Core;
using Business.Serives;
using NUnit.Framework;


namespace Business.Test
{
    [TestFixture]
    public class RepositoryTest
    {
        [TestCase(TestName = "测试仓储")]
        public void Repository_Test()
        {
            Manager manager = ManagerFactory.Create("Tianyalang", "123456", "123456");
            ManageService.Save(manager);
        }
    }
}
