using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using System.Collections;

namespace Business.Nhibernate.Base
{
    public class Repository<TD> : IRepository<TD> where TD : class
    {
        public static ISession GetSession()
        {

            return SessionProvider.SessionFactory.OpenSession();

        }

        public Type GetTD()
        {
            return typeof (TD);
        }


        public TD GetById(int id)
        {

            using (var session = GetSession())
            {

                return session.Get<TD>(id);

            }

        }

        public TD GetById(string id)
        {

            using (var session = GetSession())
            {

                return session.Get<TD>(id);

            }

        }

        public TD GetById(Int64 id)
        {

            using (var session = GetSession())
            {

                return session.Get<TD>(id);

            }

        }

        public TD GetByIntId(int id)
        {

            using (var session = GetSession())
            {

                return session.Get<TD>(id);

            }

        }

        public void Save(TD saveObj)
        {
            //object id = null;
            using (var session = GetSession())
            {

                using (var trans = session.BeginTransaction())
                {

                    session.SaveOrUpdate(saveObj);

                    trans.Commit();
                }

            }
        }

        public void Delete(TD delObj)
        {

            using (var session = GetSession())
            {

                using (var trans = session.BeginTransaction())
                {

                    session.Delete(delObj);

                    trans.Commit();

                }

            }

        }

        public void Delete(string query)
        {

            using (var session = GetSession())
            {

                using (var trans = session.BeginTransaction())
                {

                    session.Delete(query);

                    trans.Commit();

                }

            }

        }

        public IList<TD> Find(string sql)
        {

            using (var session = GetSession())
            {

                using (var trans = session.BeginTransaction())
                {

                    IList<TD> _list = session.CreateQuery(sql)
                        .List<TD>();

                    trans.Commit();

                    return _list;

                }

            }

        }

        public IList<TD> Find(string sql, int top)
        {

            using (var session = GetSession())
            {

                using (var trans = session.BeginTransaction())
                {

                    IList<TD> _list = session.CreateQuery(sql)
                        .SetMaxResults(top)
                        .List<TD>();

                    trans.Commit();

                    return _list;

                }

            }

        }

        public IList<object[]> Query(string sql)
        {
            using (var session = GetSession())
            {

                using (var trans = session.BeginTransaction())
                {

                    IQuery q = session.CreateQuery(sql);

                    IList<object[]> _list = q.List<object[]>();

                    trans.Commit();

                    return _list;

                }

            }
        }

        public IList<object[]> Query(string sql, int top)
        {
            using (var session = GetSession())
            {

                using (var trans = session.BeginTransaction())
                {

                    IQuery q = session.CreateQuery(sql).SetMaxResults(top);

                    IList<object[]> _list = q.List<object[]>();

                    trans.Commit();

                    return _list;

                }

            }
        }

        public IList<TD> GetPageEntites(IList<ICriterion> expressions, IList<Order> orders, int pageSize, int page,
            out long count)
        {
            using (var session = GetSession())
            {

                using (var trans = session.BeginTransaction())
                {

                    IMultiCriteria mulicrit = session.CreateMultiCriteria();

                    ICriteria queryCrit = session.CreateCriteria(typeof (TD));
                    ICriteria countCrit = session.CreateCriteria(typeof (TD));

                    foreach (var exp in expressions)
                    {
                        queryCrit.Add(exp);
                        countCrit.Add(exp);
                    }

                    foreach (var order in orders)
                        queryCrit.AddOrder(order);

                    IList results = mulicrit
                        .Add(queryCrit.SetFirstResult(page*pageSize).SetMaxResults(pageSize))
                        .Add(countCrit.SetProjection(Projections.RowCountInt64()))

                        .List();

                    trans.Commit();

                    IList<TD> _list = new List<TD>();

                    foreach (var o in (IList) results[0])
                        _list.Add((TD) o);

                    count = (long) ((IList) results[1])[0];

                    return _list;

                }

            }
        }

        public void ExecuteSql(string sql)
        {
            using (var session = GetSession())
            {

                using (var trans = session.BeginTransaction())
                {

                    IQuery q = session.CreateQuery(sql);

                    q.ExecuteUpdate();

                    trans.Commit();

                }

            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public void BatchUpdate(IList<TD> list)
        {
            using (var session = GetSession())
            {

                int listCount = list.Count;
                int batchTimes = listCount%10 == 0 ? listCount/10 : listCount/10 + 1;
                for (int j = 0; j < batchTimes; j++)
                {
                    var transaction = session.Transaction;
                    transaction.Begin();
                    for (int i = 0; i < 10; i++)
                    {
                        int currentItem = i + 10*j;
                        if (currentItem < listCount)
                            session.Update(list[currentItem]);
                    }
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// 批量删除（设置删除标识，伪删除）
        /// </summary>
        /// <param name="ids"></param>
        public void BatchDelete(ICollection ids)
        {
            using (var session = GetSession())
            {
                string sql = SqlHelper.DeleteHql(typeof (TD).FullName, ids);
                if (string.IsNullOrEmpty(sql) == false)
                {
                    session.CreateQuery(sql).ExecuteUpdate();
                }
            }
        }

        /// <summary>
        /// 批量删除（设置删除标识，伪删除）
        /// </summary>
        /// <param name="fieldName">sql字段名</param>
        /// <param name="ids"></param>
        public void BatchDeleteByField(string fieldName, ICollection ids)
        {
            using (var session = GetSession())
            {
                string sql = SqlHelper.DeleteHql(typeof (TD).FullName, fieldName, ids);
                session.CreateQuery(sql).ExecuteUpdate();
            }
        }

        public void BatchRealDelete(ICollection ids)
        {
            using (var session = GetSession())
            {
                string sql = SqlHelper.RealDeleteHql(typeof (TD).FullName, ids);
                if (!string.IsNullOrEmpty(sql))
                    session.CreateQuery(sql).ExecuteUpdate();
            }
        }

        public void ClearTable()
        {
            NHibernate.Cfg.Configuration config = SessionProvider.Config;
            string sqlStr = config.GetProperty("connection.connection_string");
            if (string.IsNullOrEmpty(sqlStr) || sqlStr.Contains("tea_develop;"))
            {
                Console.WriteLine("这个库不能随便清除数据");
                return;
            }

            using (var session = GetSession())
            {
                session.CreateQuery(string.Format("delete from {0}", typeof (TD).FullName)).ExecuteUpdate();
            }
        }

    }
}
