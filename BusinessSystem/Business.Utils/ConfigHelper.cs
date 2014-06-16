namespace Business.Utils
{
    public static class ConfigHelper
    {
        /// <summary>
        /// 敏感词文件路径
        /// </summary>
        public const string Sensitivewordpath = "sensitivewordpath";
        public const string Server = "Server";
        /// <summary>
        /// 获取AppSetting配置节
        /// </summary>
        /// <param name="appKey"></param>
        /// <returns></returns>
        public static string GetAppSetting(string appKey)
        {
            if (string.IsNullOrEmpty(appKey))
            {
                //throw new ArgumentNullException("appKey");
                return string.Empty;
            }
            return System.Configuration.ConfigurationManager.AppSettings[appKey];
        }

        /// <summary>
        /// 让没有domain的图片路径加上,domain从配置文件读取
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public static string GetFullImage(this string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                string domain = GetAppSetting("ImageDomain") ?? "";
                domain = domain.EndsWith("/") ? domain : (domain + "/");
                if (imagePath.StartsWith("http:")) return imagePath;
                return string.Format("{0}{1}", domain, imagePath);
            }
            return string.Empty;
        }
    }
}
