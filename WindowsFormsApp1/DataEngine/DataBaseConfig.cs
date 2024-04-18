
using System.Configuration;

namespace JW.Dal
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public class DataBaseConfig
    {
        /// <summary>
        /// 数据库连接串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return GetConnectionString("ConnectionString");
            }
        }
        /// <summary>
        /// 获取数据库连接串
        /// </summary>
        /// <param name="key">key名称</param>
        /// <returns></returns>
        public static string GetConnectionString(string key)
        {

                return ConfigurationManager.ConnectionStrings[key].ConnectionString;
            
        }
        /// <summary>
        /// 获取App设置
        /// </summary>
        /// <param name="key">key名称</param>
        /// <returns></returns>
        public static string GetAppSetting(string key)
        {
        
                return ConfigurationManager.AppSettings[key];
            
        }
    
    }
}
