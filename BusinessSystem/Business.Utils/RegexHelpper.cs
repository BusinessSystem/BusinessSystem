using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Business.Utils
{
    /// <summary>
    /// 正则
    /// </summary>
    public class RegexHelpper
    {
        private static Regex specialRege = new Regex(@"[^\w]|_");

        public static string RemoveSpecialChar(string input)
        {
            if (specialRege.IsMatch(input))
            {
                return specialRege.Replace(input, "ct").Replace("ct"," ");
            }
            return input;
        }
    }
}
