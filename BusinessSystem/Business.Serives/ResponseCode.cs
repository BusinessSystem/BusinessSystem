using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Serives
{
    public struct ResponseCode
    {
        public const string Ok = "0"; //"S0000";
        public const string Timeout = "F0001"; //超时

        public const string IsVip = "V0001";
        public const string NotVip = "V0000";

        public const string ModifySpecialVipError = "E0001";

        /// <summary>
        /// 外挂
        /// </summary>
        public const string Hacker = "F0002";

        /// <summary>
        /// 未知错误
        /// </summary>
        public const string UnknownError = "-1";

        /// <summary>
        /// 未找到数据
        /// </summary>
        public const string NotFoundData = "-2";

        /// <summary>
        /// 服务器错误 
        /// </summary>
        public const string ServerError = "-3";

        /// <summary>
        /// 参数错误 
        /// </summary>
        public const string QueryParamIsEmpty = "-4";

        /// <summary>
        /// 参数错误 
        /// </summary>
        public const string QueryParamError = "-5";

        /// <summary>
        /// 没有登录 
        /// </summary>
        public const string NoLogin = "-6";


        /// <summary>
        /// 反序列化数据出错
        /// </summary>
        public const string DataError = "-7";

        /// <summary>
        /// 没有权限
        /// </summary>
        public const string NoRight = "-8";

        /// <summary>
        /// 用户级别数据错误
        /// </summary>
        public const string CustomLevelError = "-9";

        /// <summary>
        /// 用户邮箱数据错误
        /// </summary>
        public const string CustomEmailError = "-10";

        /// <summary>
        /// 数量必须大于0
        /// </summary>
        public const string NumberThanZero = "-11";

        public const string ExistMobile = "-13";


        /// <summary>
        /// 用户模板
        /// </summary>
        public struct  Managaer
        {
            public const string UserNullOrEmpty = "1001";
            public const string PasswordNullOrEmpty = "1002";
            public const string LanguageNullOrEmpty = "1003";
            public const string RealNameNullOrEmpty = "1004";
            public const string CompanyNullOrEmpty = "1005";
            public const string UserNameHasExist = "1006";
            public const string UserNameError = "1007";
            public const string UserPasswordError = "1008";
            public const string OldPasswordNullOrEmpty = "1009";
            public const string NewPasswordNullOrEmpty = "1010";
            public const string ConfirmPasswordError = "1011";
            public const string OldPasswordError = "1012";
        }

        public struct Base
        {
            public const string ValueNullOrEmpty = "2001";
           
        }

    }
}
