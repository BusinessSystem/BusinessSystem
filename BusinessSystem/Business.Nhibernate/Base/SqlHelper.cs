using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Nhibernate.Base
{
    class SqlHelper
    {
        public static string DeleteHql(string entityName, ICollection ids)
        {
            string sql = string.Empty;

            if (ids.Count > 0)
            {
                string strIds = Utils.ConvertTools.ToString(ids);

                sql = string.Format("UPDATE {0} SET IsDeleted={1},DeletedTime ='{2}' WHERE id IN({3})", entityName, 1, DateTime.Now, strIds);
            }
            return sql;
        }

        public static string DeleteHql(string entityName, string fieldName, ICollection ids)
        {
            string sql = string.Empty;

            if (ids.Count > 0)
            {
                string strIds = Utils.ConvertTools.ToString(ids);

                sql = string.Format("UPDATE {0} SET IsDeleted={1},DeletedTime ='{2}' WHERE {3} IN({4})", entityName, 1, DateTime.Now, fieldName, strIds);
            }
            return sql;
        }

        public static string RealDeleteHql(string entityName, ICollection ids)
        {
            string sql = string.Empty;

            if (ids.Count > 0)
            {
                string strIds = Utils.ConvertTools.ToString(ids);

                sql = string.Format("Delete FROM {0} WHERE id IN({1})", entityName, strIds);
            }
            return sql;
        }
    }
}
