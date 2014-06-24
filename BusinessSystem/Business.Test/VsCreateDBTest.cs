using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Business.Nhibernate.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Business.Test
{
    [TestClass]
    public class VsCreateDBTest
    {
        [TestMethod]
        public void BuildTable()
        {
            SessionProvider.RebuildSchema();
        }
    }
}
