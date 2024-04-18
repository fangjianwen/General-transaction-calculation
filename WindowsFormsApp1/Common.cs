using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppFruitCalc
{
    public class Common
    {

       
        /// <summary>
        /// 检查有几位小数
        /// </summary>
        /// <param name="number">值</param>
        /// <returns></returns>
        public static int GetDecimalPlaces(decimal number)
        {
            string val = number.ToString();
            if (val.Contains("."))
            {
                return number.ToString(CultureInfo.InvariantCulture).Split('.')[1].Length;
            }
            else
            {
                return 0;
            }
            
        }
        public static string GetMacAddress()
        {
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                // 排除虚拟网卡和无效网卡
                if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Loopback || networkInterface.OperationalStatus != OperationalStatus.Up)
                    continue;

                PhysicalAddress physicalAddress = networkInterface.GetPhysicalAddress();
                byte[] bytes = physicalAddress.GetAddressBytes();
                string macAddress = BitConverter.ToString(bytes);
                return macAddress;
            }

            return "";
        }


    }
}
