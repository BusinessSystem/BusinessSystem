using System;
using System.Collections;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Business.Utils
{
    public class ConvertTools
    {
        public static double ToDouble(object value)
        {
            if (value == null || value.ToString() == "")
                return 0;
            else
                return Convert.ToDouble(value);
        }

        public static Int64 ToInt64(string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            Int64 ret = 0;
            Int64.TryParse(value, out ret);
            return ret;
        }

        public static decimal ToDecimal(string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            decimal ret = 0;
            decimal.TryParse(value, out ret);
            return ret;
        }

        public static int ToInt(string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            int ret = 0;
            int.TryParse(value, out ret);
            return ret;
        }

        public static int ToInt(object o, int defaultInt)
        {
            int result = default(int);
            try
            {
                Int32.TryParse(o.ToString(), out result);
            }
            catch
            {
                result = defaultInt;
            }
            return result;
        }

        public static DateTime ToDateTime(object o, DateTime defaultDate)
        {
            var result = DateTime.MinValue;
            try
            {
                DateTime.TryParse(o.ToString(), out result);
            }
            catch
            {
                result = defaultDate;
            }
            return result;
        }

        public static DateTime ToDateTime(object o)
        {
            return ToDateTime(o, DateTime.Parse("1900-01-01"));
        }

        /// <summary>
        /// To convert a string to DateTime,
        /// in order to put into db can use prepared statement or
        /// {DateTime}.ToString("yyyy/MM/dd")
        /// </summary>
        /// <param name="value">A string in dd/MM/yyyy</param>
        /// <returns>Converted DateTime</returns>
        public static object ToDate(string value, string[] from)
        {
            if (!string.IsNullOrEmpty(value))
            {
                DateTimeFormatInfo dtfi = new CultureInfo("en-US", false).DateTimeFormat;
                dtfi.SetAllDateTimePatterns(from, 'd');
                return Convert.ToDateTime(value, dtfi);
            }
            return null;
        }

        /// <summary>
        /// To get the DateTime formate instead of string format
        /// </summary>
        /// <param name="value">The string that want to convert</param>
        /// <param name="from">String array of the initial DateTime format</param>
        /// <param name="db">If the DateTime used in database</param>
        /// <returns>Converted DateTime</returns>
        public static object ToDate(object value, string[] from, Boolean db)
        {
            if (db && value is DBNull)
                return null;

            if (value.GetType() == typeof(string) && !string.IsNullOrEmpty((string)value))
            {
                return ((DateTime)ToDate((string)value, from));
            }
            else if (value.GetType() == typeof(DateTime))
            {
                return ((DateTime)value);
            }

            if (db)
                return DBNull.Value;
            else
                return null;
        }


        /// <summary>
        /// To convert a string from [from] format to [to] format
        /// </summary>
        /// <param name="value">The string that want to convert</param>
        /// <param name="from">String array of the initial DateTime format</param>
        /// <param name="to">Target fromat</param>
        /// <param name="db">If the string used in database</param>
        /// <returns>Converted DataTime string</returns>
        public static object ToDateString(object value, string[] from, string to, Boolean db)
        {
            if (db && value is DBNull)
                return null;
            if (value == null)
                if (db)
                    return DBNull.Value;
                else
                    return null;

            if (value.GetType() == typeof(string) && !string.IsNullOrEmpty((string)value))
                return ((DateTime)ToDate((string)value, from)).ToString(to, new CultureInfo("en-US", false).DateTimeFormat);
            else if (value.GetType() == typeof(DateTime))
                return ((DateTime)value).ToString(to, new CultureInfo("en-US", false).DateTimeFormat);

            if (db)
                return DBNull.Value;
            else
                return null;
        }

        /// <summary>
        /// To convert an ArrayList to string with ',' seperate
        /// </summary>
        /// <param name="value">ArrayList want to convert</param>
        /// <returns>A string with ',' seperate</returns>
        public static string ToString(IEnumerable value)
        {
            return ToString(value, ',');
        }

        /// <summary>
        /// 把字符串集合 按splitChar分隔转换成单个字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="splitChar"></param>
        /// <returns></returns>
        public static string ToString(IEnumerable value, char splitChar)
        {
            string result = "";
            if (value != null)
            {
                foreach (object obj in value)
                {
                    result += Convert.ToString(obj) + splitChar;
                }
                if (result.Length > 0)
                {
                    result = result.Substring(0, result.Length - 1);
                }
            }

            return result;
        }


        /// <summary>
        /// 把value 转化为 16位 Guid
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Guid ToGuid(int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }

        public static byte[] ConvertToCSharpBytesFromJavaBytesString(string strJavaBytesStringSplitByBlankSpace)
        {

            if (string.IsNullOrEmpty(strJavaBytesStringSplitByBlankSpace))
                return null;

            byte[] byteRtn = null;

            string[] strJavaBytes = strJavaBytesStringSplitByBlankSpace.Split(' ');

            byteRtn = new byte[strJavaBytes.Length];

            int intTry;

            int i = 0;

            foreach (string str in strJavaBytes)
            {
                if (!int.TryParse(str, out intTry))
                {
                    throw new Exception("Convert From Java Bytes String Failed!");
                }
                else
                {
                    int temp;

                    temp = Convert.ToInt32(str);

                    temp = (temp < 0) ? (temp + 256) : temp;

                    byteRtn.SetValue((byte)temp, i);

                    i++;
                }
            }

            return byteRtn;

        }

        public static string ConvertToBase64String(string strValue)
        {
            string strRtn = null;

            if (strValue == null)
                return strRtn;

            byte[] byteRtn = null;

            byteRtn = System.Text.Encoding.UTF8.GetBytes(strValue);

            strRtn = Convert.ToBase64String(byteRtn);

            return strRtn;
        }

        public static string ConvertToBase64String(byte[] byteValue)
        {
            string strRtn = null;

            strRtn = Convert.ToBase64String(byteValue);

            return strRtn;
        }

        public static byte[] ConvertFromBase64String(string strBase64)
        {
            byte[] byteRtn = null;

            byteRtn = Convert.FromBase64String(strBase64);

            return byteRtn;
        }

        /// <summary>
        /// Check Text Decimal format
        /// </summary>
        /// <param name="textString">pass you vaildate string</param>
        /// <param name="Long">max length</param>
        /// <param name="dot">after dot max length</param>
        /// <returns></returns>
        public string CheckTextDecimal(string textString, int Long, int dot, double minValue)
        {


            if (IsNumeric(textString) == false)
            {
                return "Please enter a numeric value.";
            }
            else if (IsNumeric(textString) == true)
            {
                string[] value = textString.Split('.');
                if (value[0].Length == textString.Length)
                {
                    double max = 10;
                    for (int i = 0; i < Long; i++)
                    {
                        if (i == 0)
                        {
                            max = 10;
                        }
                        else
                        {
                            max *= max;
                        }
                    }
                    max = max - 1;
                    if (Convert.ToDouble(textString) > max)
                    {
                        return "you input is to large,Please between 0~" + max.ToString();
                    }
                    else
                    {
                        return "true";
                    }

                }
                else
                {
                    if ((value[0].Length + value[1].Length) > Long)
                    {
                        return "you input string is to long , please between 0~" + Long.ToString();
                    }
                    else
                    {
                        if (value[1].Length > dot)
                        {
                            return "you input decimal  is to long , please between 0~" + dot.ToString();
                        }
                        else
                        {
                            double max = 10;
                            for (int i = 0; i < Long; i++)
                            {
                                if (i == 0)
                                {
                                    max = 10;
                                }
                                else
                                {
                                    max *= max;
                                }
                            }
                            max = max - 1;
                            if (Convert.ToDouble(textString) > max)
                            {
                                return "you input is to large,Please between 0~" + max.ToString();
                            }
                            else if (Convert.ToDouble(textString) <= minValue)
                            {
                                return "Please enter a value greater than " + minValue.ToString();
                            }
                            else
                            {
                                return "true";
                            }

                        }

                    }
                }
            }

            return "true";
        }

        private bool IsNumeric(string sStr)
        {
            bool bReturnValue = true;
            if (sStr == null || sStr.Length == 0)
            {
                bReturnValue = false;
            }
            else
            {
                foreach (char c in sStr)
                {
                    if (!Char.IsNumber(c))
                    {
                        if (c != '.')
                        {
                            bReturnValue = false;
                            break;
                        }
                    }
                }
            }
            return bReturnValue;
        }
        /// <summary>
        /// check the date is exist
        /// </summary>
        /// <param name="year">year,pls format is yyyy</param>
        /// <param name="month">month,please is mm</param>
        /// <param name="day">day,please is day</param>
        /// <returns></returns>
        public bool CheckDataIsExist(int year, int month, int day)
        {
            if (month > 12 || day > 31)
            {
                return false;
            }
            else
            {
                if (month == 2 && day == 29)
                {
                    if (DateTime.IsLeapYear(year))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (month == 2 && day == 30)
                {
                    return false;
                }
                else
                {
                    if ((month == 2 || month == 4 || month == 6 || month == 9 || month == 11) && day == 31)
                    {
                        return false;
                    }
                }
            }
            return true;

        }
        /// <summary>
        /// User number to get the month， example input 13 retrun 1 ,15 return 3
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public int Month(int month)
        {
            if (month % 12 == 0)
            {
                return 12;
            }
            else
            {
                return month % 12;
            }

        }

        /// <summary>
        /// Calculated to increase the number of months to get an increase of the number of years
        /// </summary>
        /// <param name="month">Current month</param>
        /// <param name="add_month">Increase the number of months</param>
        /// <param name="year">Current Year</param>
        /// <returns>The final year of</returns>
        public int Year(int month, int add_month, int year)
        {
            int temp = (month + add_month) % 12;
            int temp1 = (month + add_month) / 12;
            if (temp == 0)
            {
                return year;
            }
            else if (temp == 0 && temp1 == 1)
            {
                return year;
            }
            else
                return (year + temp1);
        }

        /// <summary>
        /// 是否有效的SQL字段名
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static bool IsValidSqlFieldName(string fieldName)
        {
            Regex regex = new Regex(@"[A-Za-z0-9]+");
            return regex.IsMatch(fieldName);
        }

    }
}
