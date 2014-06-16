using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace Business.Utils
{
    /// <summary>
    /// 包括 DES,RSA,MD5
    /// </summary>
    public class EncryptTools
    {
        #region DES加密解密
        //默认密钥向量
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        /**/
        /**/
        /**/
        private static DESCryptoServiceProvider des = new DESCryptoServiceProvider();

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] inputByteArray = Encoding.GetEncoding("UTF-8").GetBytes(encryptString);
                des.Key = ASCIIEncoding.ASCII.GetBytes(encryptKey.Substring(0, 8));
                des.IV = ASCIIEncoding.ASCII.GetBytes(encryptKey.Substring(0, 8));
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                StringBuilder ret = new StringBuilder();
                foreach (byte b in ms.ToArray())
                {
                    ret.AppendFormat("{0:X2}", b);
                }
                return ret.ToString();
            }
            catch (Exception ex)
            {
                return encryptString;
            }
        }

        /**/
        /**/
        /**/
        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] inputByteArray = new byte[decryptString.Length / 2];
                for (int x = 0; x < decryptString.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(decryptString.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }
                des.Key = ASCIIEncoding.ASCII.GetBytes(decryptKey.Substring(0, 8));
                des.IV = ASCIIEncoding.ASCII.GetBytes(decryptKey.Substring(0, 8));
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                StringBuilder ret = new StringBuilder();
                return System.Text.Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                return decryptString;
            }
        }
        #endregion

        #region RSA加密解密

        /// <summary>
        /// RSA 的密钥产生 产生私钥 和公钥 
        /// </summary>
        /// <param name="xmlKeys"></param>
        /// <param name="xmlPublicKey"></param>
        public static void RSAKey(out string xmlKeys, out string xmlPublicKey)
        {
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            xmlKeys = rsa.ToXmlString(true);
            xmlPublicKey = rsa.ToXmlString(false);
        }

        const string PUBLICKEY = "<RSAKeyValue><Modulus>tCa0V2rPFI3eZZtTGYvVLfT3jAipT3IqEUHTH5u1x+HoA9gDdJo9dlMCf9lqcBwS5NvaKdgOe8T0mQ2O330blN2XWQUdl7rU/DbZljo0K1g5Dqkhz2quahy9rWwHXyPrJV+nmYv4mvurogh0XoeGSWJzjQinuZ7zBXto11gijx0=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        const string PRIVATEKEY = "<RSAKeyValue><Modulus>tCa0V2rPFI3eZZtTGYvVLfT3jAipT3IqEUHTH5u1x+HoA9gDdJo9dlMCf9lqcBwS5NvaKdgOe8T0mQ2O330blN2XWQUdl7rU/DbZljo0K1g5Dqkhz2quahy9rWwHXyPrJV+nmYv4mvurogh0XoeGSWJzjQinuZ7zBXto11gijx0=</Modulus><Exponent>AQAB</Exponent><P>8TMmfjUgILATok3EVjnDDtHUeSX2RgZEfBY+iiGbN8kTESIXczhLkq/KYc1bXLi9zxINp+HmBpDVsPxrB4nuJw==</P><Q>vzSU1kA86U02EsV7GUqhwtKaIoznodjdyo7e5wgmRlBaIVK5kU1kGMl/bJDG8gSw/8GIW/yRAXYAJeChW82nGw==</Q><DP>q/BomkNIucSK2oJRWZ4nfGL78bisDFLfcw1wW4uFWIkP/ICu8sXIqbKCtKFtZXWUaQ5Xibuw/DE3A8mMin06tQ==</DP><DQ>KOgR86h4n50yNV/kjyulYe32pe+pWrnv8XcRfzICJkbokXqGUuzQvnDVfx+WQI76Yy0/hBaL21kofPIK834TAw==</DQ><InverseQ>bPD5sipnq9ZLHYPqRkqar7kHDUxqUf1KTw9wAPTviSPNGaarV7+CliQ514o8XgLbPHc6FUZuCkSV65J8p75wpg==</InverseQ><D>GWQfJPcL6tS4FNAYnVAlIs4VGeqamnT30ujZLepPa7W+ctQ+YG/g282FW6m0I8sBKrqF/EHUgMNYyj2r9nn2qf7DaFFe1K7aJ/G8l70Hm06aohhim62ZM4HMgnaUR8T4UyIjtznIC7yI7AB7488JGC2YbUOCbl1BOtHRVjC8bw0=</D></RSAKeyValue>";

        /// 
        /// 加密
        /// 
        /// 待加密的字符串
        ///
        [ThreadStatic]
        private static RSACryptoServiceProvider rsa = null;
        [ThreadStatic]
        private static RSACryptoServiceProvider dersa = null;
        public static string RSAEncryption(string word)
        {
            //CspParameters param = new CspParameters();
            //param.KeyContainerName = PUBLICKEY;

            if (rsa == null)
            {
                rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(PUBLICKEY);
            }
            byte[] plaindata = System.Text.Encoding.Default.GetBytes(word);
            byte[] encryptdata = rsa.Encrypt(plaindata, false);
            string encryptstring = Convert.ToBase64String(encryptdata);
            return encryptstring;
        }
        /// 
        /// 解密，当密文不正确时，可能会抛出异常
        /// 
        /// 待解密的密文字符串
        /// 
        public static string RSADecryption(string encryptWord)
        {
            //CspParameters param = new CspParameters();
            //param.KeyContainerName = PRIVATEKEY;
            if (dersa == null)
            {
                rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(PRIVATEKEY);
            }


            byte[] encryptdata = Convert.FromBase64String(encryptWord);
            byte[] decryptdata = rsa.Decrypt(encryptdata, false);
            string plaindata = System.Text.Encoding.Default.GetString(decryptdata);
            return plaindata;

        }

        #endregion

        #region MD5加密
        /// <summary>
        /// MD5 32位加密
        /// </summary>
        /// <param name="s"></param>
        /// <param name="_input_charset"></param>
        /// <returns></returns>
        public static string GetMD5_32(string s, string _input_charset)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(s));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }
       

        public static string GetMD5_32(string s)
        {
            return GetMD5_32(s, "UTF-8");
        }

    
     
        /// <summary>
        /// HMACSHA1散列
        /// </summary>
        /// <param name="encryptString"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        public static string EncryptHMACSHA1(string encryptString, string encryptKey)
        {
            byte[] StrRes = Encoding.UTF8.GetBytes(encryptString);
            byte[] key = Encoding.Default.GetBytes(encryptKey);
            HMACSHA1 hmacsha1 = new HMACSHA1(key);
            hmacsha1.ComputeHash(StrRes);
            return Convert.ToBase64String(hmacsha1.Hash);
        }

        #endregion 
    }
}
