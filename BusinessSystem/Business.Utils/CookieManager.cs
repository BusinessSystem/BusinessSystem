using System;
using System.Web;
using System.Web.Security;

namespace Business.Utils
{
    public class CookieManager
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="expires"></param>
        public static void SetCookie(string name, string value, DateTime? expires = null)
        {
            HttpContext.Current.Response.Cookies.Remove(name);
            value = HttpUtility.UrlEncode(value);
            HttpCookie cookie = new HttpCookie(name, value);
            //cookie.Domain = FormsAuthentication.CookieDomain;
            //cookie.Path = FormsAuthentication.FormsCookiePath;
            var domain = GetServerDomain();
            //if (!domain.Equals("localhost"))
            //    cookie.Domain = domain;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (expires != null)
            {
                cookie.Expires = expires.Value;
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static bool TryGetCT(string CTCookieName, out string CT)
        {
            try
            {
                CT = GetCookie(CTCookieName);
                if (string.IsNullOrEmpty(CT))
                {
                    return false;
                }
                return true;
            }
            catch
            {
                CT = string.Empty;
                return false;
            }

        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetCookie(string name)
        {
            HttpCookie ck = HttpContext.Current.Request.Cookies[name];
            string value = string.Empty;
            if (ck != null)
            {
                value = HttpUtility.UrlDecode(ck.Value);
            }
            return value;
        }


        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetCookie(string name, HttpRequestBase req)
        {
            HttpCookie ck = req.Cookies[name];
            string value = string.Empty;
            if (ck != null)
            {
                value = HttpUtility.UrlDecode(ck.Value);
            }
            return value;
        }
        public static void SetCookie(HttpResponseBase response, string name, string value, DateTime? expires = null)
        {
            response.Cookies.Remove(name);
            value = HttpUtility.UrlEncode(value);
            HttpCookie cookie = new HttpCookie(name, value);
            //cookie.Domain = FormsAuthentication.CookieDomain;
            //cookie.Path = FormsAuthentication.FormsCookiePath;
            var domain = GetServerDomain();
            //if (!domain.Equals("localhost"))
            //    cookie.Domain = domain;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (expires != null)
            {
                cookie.Expires = expires.Value;
            }
            response.Cookies.Add(cookie);
        }
        public static void ClearCookie(string name)
        {
            HttpCookie cookie = new HttpCookie(name);
            cookie.Expires = DateTime.Now.AddDays(-1);
            cookie.Path = FormsAuthentication.FormsCookiePath;

            //cookie.Domain = GetServerDomain();
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static bool IsNumeric(string str)
        {
            try { int i = Convert.ToInt32(str); return true; }
            catch { return false; }
        }


        /// <summary>
        /// 获取服务器根域名
        /// </summary>
        /// <returns></returns>
        public static string GetServerDomain()
        {
            string strHostName = HttpContext.Current.Request.Url.Host.ToLower(); // 此处获取值转换为小写
            if (strHostName.IndexOf('.') > 0)
            {
                string[] strArr = strHostName.Split('.');
                string lastStr = strArr.GetValue(strArr.Length - 1).ToString();
                if (IsNumeric(lastStr)) // 如果最后一位是数字，那么说明是IP地址
                {
                    return strHostName.Replace(".", ""); // 替换.为纯数字输出
                }
                else // 否则为域名
                {
                    string[] domainRules = ".com.cn|.net.cn|.org.cn|.gov.cn|.com|.net|.cn|.org|.cc|.me|.tel|.mobi|.asia|.biz|.info|.name|.tv|.hk|.公司|.中国|.网络".Split('|');
                    string findStr = string.Empty;
                    string replaceStr = string.Empty;
                    string returnStr = string.Empty;
                    for (int i = 0; i < domainRules.Length; i++)
                    {
                        if (strHostName.EndsWith(domainRules[i].ToLower())) // 如果最后有找到匹配项
                        {
                            findStr = domainRules[i].ToString();
                            replaceStr = strHostName.Replace(findStr, ""); // 将匹配项替换为空，便于再次判断
                            if (replaceStr.IndexOf('.') > 0) // 存在二级域名或者三级
                            {
                                string[] replaceArr = replaceStr.Split('.');
                                returnStr = replaceArr.GetValue(replaceArr.Length - 1).ToString() + findStr;
                                return returnStr;
                            }
                            else
                            {
                                returnStr = replaceStr + findStr;
                                return returnStr;
                            };
                        }
                        else
                        {
                            returnStr = strHostName;
                        }
                    }
                    return returnStr;
                }
            }
            else
            {
                return strHostName;
            }
        }

    }
}
