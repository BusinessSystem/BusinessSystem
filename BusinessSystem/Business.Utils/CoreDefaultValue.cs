using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Utils
{
    /// <summary>
    /// 实体默认值
    /// </summary>
    public struct CoreDefaultValue
    {
        /// <summary>
        /// 1900-01-01
        /// </summary>
        public static readonly DateTime MinTime = new DateTime(1900, 1, 1);

        public static string SystemAdminName = "systemadmin";

       /// <summary>
        /// 1980-01-01
        /// </summary>
        public static readonly DateTime MinBirTime = new DateTime(1980, 1, 1);
        /// <summary>
        /// 1900-01-01 00:00:00
        /// </summary>
        public const string MinTimeString = "1900-01-01 00:00:00";

        /// <summary>
        /// False = 0
        /// </summary>
        public const short False = 0;
        /// <summary>
        /// True = 1
        /// </summary>
        public const short True = 1;

        /// <summary>
        /// 退货期默认15天
        /// </summary>
        public const short RetrunProductPeriod = 15;

        #region Categorybase 实体

        /// <summary>
        /// 父ID（用于分级），（默认）最顶级为0
        /// </summary>
        public const short Category_ParentId = 0;

        /// <summary>
        /// 级别深度（默认从1开始）
        /// </summary>
        public const short Category_Depth = 1;
        #endregion

        /// <summary>
        /// 品牌状态:0为不使用，1为使用
        /// </summary>
        public const short BrandStatus = 1;

        public const short All = -1;

        public enum ShoppingCartLogOperationEnum : short
        {
            ADD, DELETE, CHANGE_QTY, PRE_SUBMIT, SUBMIT
        }

        public enum ShoppingCartItemStatus
        {
            /// <summary>
            /// 正常
            /// </summary>
            Normal
        }
    }

}
