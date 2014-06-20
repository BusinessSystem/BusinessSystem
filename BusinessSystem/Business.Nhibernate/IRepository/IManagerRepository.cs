using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Core;
using Business.Nhibernate.Base;

namespace Business.Nhibernate.IRepository
{
    public interface IManagerRepository : IRepository<Manager>
    {

    }

    public interface IManagerProductRepository : IRepository<ManagerProduct>
    {

    }

    public interface IPwdChangeRecordRepository : IRepository<PwdChangeRecord>
    {

    }
}
