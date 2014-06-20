using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Core;
using Business.Nhibernate.IRepository;
using NHibernate.Hql.Ast.ANTLR.Util;
using Business.Nhibernate.Base;

namespace Business.Nhibernate.Repository
{
    public class VisitorRecordRepository:Repository<VisitorRecord>,IVisitorRecordRepository
    {
    }
}
