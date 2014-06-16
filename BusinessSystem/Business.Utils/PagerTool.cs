using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Utils
{
    public class PagerTool
    {
        /// <summary>
        /// 分页索引
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="numPerPage"></param>
        /// <returns></returns>
        public static IList<int> GetPagerIndex(int totalCount, int numPerPage)
        {
            IList<int> idPeriods = new List<int>();

            if (totalCount == 0 || numPerPage == 0) return idPeriods;

            idPeriods.Add(0);

            if (totalCount == 1) return idPeriods;

            double periods = Math.Floor((double)(totalCount / numPerPage));

            for (var i = 1; i <= periods; i++)
            {
                idPeriods.Add(i * numPerPage - 1);
            }

            if (totalCount % numPerPage > 0)
                idPeriods.Add(totalCount - 1);

            return idPeriods;
        }
        /// <summary>
        /// 修正页码
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static void FixPageIndex(ref int PageSize,ref int pageIndex)
        {
            PageSize = PageSize < 0 ? 0 : PageSize;
            pageIndex= pageIndex <= 0 ? 1 : pageIndex;
        }
        /// <summary>
        /// 获取分页需要跳过的条数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static int GetSkipCount(int pageSize ,int pageIndex)
        {
            int result;

            result=(pageIndex - 1) * pageSize;

            result = result > 0 ? result : 0;

            return result;
        }

        /// <summary>
        /// 分页索引
        /// </summary>
        /// <param name="idEnd"></param>
        /// <param name="numPerPage"></param>
        /// <param name="idStart"></param>
        /// <returns></returns>
        public static IList<long> GetPagerIndex(long idStart,long idEnd, int numPerPage)
        {
            IList<long> idPeriods = new List<long>();

            if (idEnd == 0 || numPerPage == 0) return idPeriods;

            idPeriods.Add(idStart);

            if (idEnd == 1) return idPeriods;

            double periods = Math.Floor((double)((idEnd - idStart) / numPerPage));

            for (var i = 1; i <= periods; i++)
            {
                idPeriods.Add(i * numPerPage + idStart - 1);
            }

            if (idEnd % numPerPage > 0)
                idPeriods.Add(idEnd);

            return idPeriods;
        }
    }
}
