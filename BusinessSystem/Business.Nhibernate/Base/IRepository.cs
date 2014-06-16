using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Nhibernate.Base
{
    public interface IRepository<TD> where TD : class
    {
        void Delete(TD delObj);
        void ExecuteSql(string sql);
        IList<TD> Find(string condition);
        TD GetById(long id);
        TD GetById(string id);
        void Save(TD saveObj);
        void BatchUpdate(IList<TD> list);
        /// <summary>
        /// 并不是真正的删除，只是更新IsDeleted字段为1
        /// </summary>
        /// <param name="ids"></param>
        void BatchDelete(ICollection ids);

        /// <summary>
        /// 批量删除（设置删除标识，伪删除）
        /// </summary>
        /// <param name="fieldName">sql字段名</param>
        /// <param name="ids"></param>
        void BatchDeleteByField(string fieldName, ICollection ids);

        /// <summary>
        /// 真正地将数据从数据库移除
        /// </summary>
        /// <param name="ids">主键值</param>
        void BatchRealDelete(ICollection ids);
        void ClearTable();
    }
}
