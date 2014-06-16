using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utils
{
    public class SecurityCodeHelper
    {
        //23进制
        private const int BaseDigit = 22;
        //长度为6
        private const int BaseLength = 6;
        //包含字母
        private static char[] baseCode = new char[BaseDigit] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'W', 'X', 'Y'};
        //10进制最大值
        private static int maxValue = Convert.ToInt32(Math.Pow(BaseDigit, BaseLength));

        private static List<string> _generateCodes;


        public static List<string> Generate(int number)
        {
            return Generate(number, null);
        }

        public static List<string> Generate(int number, List<string> existCodesList)
        {
            if (existCodesList != null && existCodesList.Count > 0)
                _generateCodes = existCodesList;
            else
                _generateCodes = new List<string>();

            int i = 0;

            while (i < number)
            {
                var random = RandomToSystem22();

                if (_generateCodes.Contains(random)) continue;

                _generateCodes.Add(random);

                i++;
            }

            if (existCodesList != null && existCodesList.Count > 0)
                _generateCodes.RemoveRange(0, existCodesList.Count);

            return _generateCodes;
        }

        public static string RandomToSystem22()
        {
            string result = string.Empty;

            System.Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < BaseLength; i++)
            {
                int rnd = random.Next(0, BaseDigit - 1);
                result += baseCode[rnd];
            }
            return result;
        }

    }
}
