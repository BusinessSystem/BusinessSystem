using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Business.Utils
{
    public class CheckTools
    {

        /// <summary>
        /// To validate if a string is decimal
        /// </summary>
        /// <param name="theValue">The string which want to validate</param>
        /// <returns>True if the string can be change into decimal otherwise false</returns>
        public static bool IsDecimal(string theValue)
        {
            return IsDecimal(theValue, null, null);
        }

        /// <summary>
        /// To validate if a string can be change into decimal
        /// with the length of integer part and length of decimal part
        /// </summary>
        /// <param name="theValue">The string which want to validate</param>
        /// <param name="integerPart">The length of the integer part, null if no limit</param>
        /// <param name="decimalPart">The length of the deciaml part, null if no limit</param>
        /// <returns>True if pass validation, otherwise false</returns>
        public static bool IsDecimal(string theValue, int? integerPart, int? decimalPart)
        {
            try
            {
                Convert.ToDecimal(theValue);
                string[] temp = theValue.Split('.');
                if (decimalPart != null && temp.Length > 1)
                {
                    if (temp[1].Length > decimalPart)
                        return false;
                }
                if (integerPart != null)
                {
                    if (temp[0].Length > integerPart)
                        return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// To validate if a string can be change into integer
        /// </summary>
        /// <param name="theValue">The string which want to validate</param>
        /// <returns>True if pass validation, otherwise false</returns>
        public static bool IsInteger(string theValue)
        {
            try
            {
                Convert.ToInt32(theValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string CheckEmptyString(string _obj)
        {
            if (string.IsNullOrEmpty(_obj))
                return string.Empty;
            else
                return _obj;
        }


        public static bool IsValidEmail(string strEmail)
        {
            string pattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            Regex reg = new Regex(pattern);
            return reg.IsMatch(strEmail);
        }

        public static string IsDBNull_string(object obj)
        {
            if (obj == DBNull.Value)
                return string.Empty;
            else if (obj == null)
                return string.Empty;
            else
                return obj.ToString();
        }

        /// <summary>
        /// /*移动号段16个（2011年版）  
        /// 134、135、136、137、138、139、147、150、151、152、157、158、159、182、187、188  
        /// 规则：三位固定号段+8为数字*/  
        /// /*联通号段7个  
        /// 130、131、132、155、156、185、186  
        ///*电信号段4个   133、153、180、189   
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool IsMobileNum(string num)
        {
            Regex rg = new Regex(@"^(1(([35][0-9])|(47)|[8][012356789]))\d{8}$");

            return rg.IsMatch(num);
        }

        /// <summary>
        /// 是否有汉字 xiqing
        /// </summary>
        /// <param name="srcString"></param>
        /// <returns></returns>
        public static bool IsIncludeChinese(string srcString)
        {

            int strLen = srcString.Length;
            //字符串的长度，一个字母和汉字都算一个  
            int bytLeng = System.Text.Encoding.UTF8.GetBytes(srcString).Length;
            //字符串的字节数，字母占1位，汉字占2位,注意，一定要UTF8  
            bool chkResult = false;
            if (strLen < bytLeng)
            //如果字符串的长度比字符串的字节数小，当然就是其中有汉字啦^-^  
            {
                chkResult = true;
            }

            return chkResult;
        }

        /// <summary>
        /// 是否纯数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsAllNumber(string str)
        {
            Regex rg = new Regex(@"^[0-9]+$");
            return rg.IsMatch(str);
        }
        /// <summary>
        /// 是否纯字母
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsAllWord(string str)
        {
            Regex re = new Regex(@"^[a-zA-Z]+$");
            return re.IsMatch(str);
        }

    }
}
