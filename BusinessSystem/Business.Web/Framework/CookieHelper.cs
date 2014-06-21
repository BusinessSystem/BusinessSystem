using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Business.Core;
using Business.Utils;

namespace Business.Web.Framework
{
    public class CookieHelper
    {

        /// <summary>
        /// 用户密码加密KEY
        /// </summary>
        public static string LoginPassWord = "DsGvGstqONMsyfnH/IdtdxTNRpWni5uCrppYWdM9J+4JSROl7Ikmx2EDiHEtTm1HPzYi6Shp/wCSdhvwSiAjwf/7l3iEsJWz2xvETosDODqOuWxftkdG5FxcdxWyjPiXKcRZVtu2aMvwhL0OXTvxiOenPdvmGwssnrbJA02PxjQ=";
         
        /// <summary>
        /// RSA通用密钥
        /// </summary>
        public static string CommEncryptKey = "nnLqOJSowvA7AtUXrwpEuWBW/HvgyB+9T9eOZ/dFitvSL6jHB2gHcK7h2c/D9KmS3ndap7neXj/Rhz0N3Kfbdk3kDg2xqNA08Nrq5aDXsw2XC8+93cfBOw2S0Oogu1uswb30bpuN1XqeH0q9iSH+sl4HKQw5Bwqlipji2toFG18=";

       
        /// <summary>
        /// 
        /// </summary>
        public static string GenerateTicksForRequest()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.Ticks.ToString());
            CookieManager.SetCookie(CookieConst.COOKIE_TICKS_NAME, sb.ToString());
            return sb.ToString();
        }

        public static void ClearLoginCookie()
        {
            CookieManager.ClearCookie(CookieConst.COOKIE_USER);
        }

       

         

        /// <summary>
        /// 获取 RSA 加密之后的Key
        /// </summary>
        protected static string EncryptKey = GetRSAPwdKey();

        /// <summary>
        /// 获取 RSA 加密之后的Key
        /// </summary>
        /// <returns></returns>
        private static string GetRSAPwdKey()
        {
            try
            {
                return EncryptTools.RSADecryption(LoginPassWord);

            }
            catch (Exception ex)
            {
                //ServerLogger.Error(ex.Message);
            }
            return "";
        }


        public static void SaveManagerCookie(Manager manager)
        {
            CookieManager.SetCookie(CookieConst.COOKIE_MANAMGER,
                EncryptTools.EncryptDES(manager.GetManagerCookieString(), EncryptKey),DateTime.Now.AddDays(14));
        }

        public static Manager GetManagerFromCookie()
        {
            try
            {
                //加密串 进行一次DES 加密,RSA串作为密Key,保存到cookie\
                var encryptCookie = CookieManager.GetCookie(CookieConst.COOKIE_MANAMGER);
                var cookieString = EncryptTools.DecryptDES(encryptCookie, EncryptKey);
                return Manager.GetFromCookieString(cookieString);

            }
            catch
            {
                return null;
            }

        }

        public static void ClearManagerLoginCookie()
        {
            CookieManager.ClearCookie(CookieConst.COOKIE_MANAMGER);
        }



        public struct CookieConst
        {
            /// <summary>
            /// cookie 验证码加密 key 
            /// </summary>
            public const string COOKIE_VALIDATE_ENCRYPTKEY = "$teavcode&";

            /// <summary>
            /// 加密串 Login User Cookie Name
            /// </summary>
            public const string COOKIE_USER = "_ct_u";

            public const string COOKIE_MANAMGER = "_ct_ma";

            /// <summary>
            /// 跟踪计算机
            /// </summary>
            public const string COOKIE_CT = "_ct_ct";

            /// <summary>
            /// 登录名和昵称
            /// </summary>
            public const string COOKIE_NAME = "_ct_n";

            /// <summary>
            /// 验证码 cookie name 
            /// </summary>
            public const string COOKIE_VALIDATE_NAME = "_ct_vc_";

            /// <summary>
            /// 请求ID Cookie Name
            /// </summary>
            public const string COOKIE_REQUEST_TAG_NAME = "_rc_";

            /// <summary>
            /// 
            /// </summary>
            public const string COOKIE_TICKS_NAME = "_tc_";

            /// <summary>
            /// loginame
            /// </summary>
            public const string COOKIE_LOGINNAME_NAME = "_teanpin";

            /// <summary>
            /// loginame
            /// </summary>
            public const string COOKIE_NICKNAME_NAME = "_npin";

            /// <summary>
            /// CustomId
            /// </summary>
            public const string COOKIE_PIN_NAME = "_teapin";

            /// <summary>
            /// 购物车使用 cookie name
            /// 客户端访问唯一 ID (Guid)
            /// </summary>
            public const string COOKIE_UNIQUE_NAME = "_tea_unique_";
        }

    }
}