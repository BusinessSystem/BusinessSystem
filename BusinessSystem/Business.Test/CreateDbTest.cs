using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Nhibernate.Base;
using NUnit.Framework;
using Business.Nhibernate;
using Business.Utils;
namespace Business.Test
{
    [TestFixture]
    public class CreateDbTest
    {
        /// <summary>
        /// 自动生成数据库表
        /// </summary>
        [TestCase(TestName = "自动生成数据库表")]
         
        public void BuildTable()
        {
            LogHelper.Error("", new Exception("ddfsofowefwoifw"));
           // LogHelper.WriteLog(actionExecutedContext.GetType(), actionExecutedContext.Exception); SessionProvider.RebuildSchema();
        }
    }
}
