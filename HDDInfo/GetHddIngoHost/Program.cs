using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Net;
using System.Net.Sockets;

namespace GetHddIngoHost
{
    class Program
    {
        static void Main()
        {
            using (var host = new ServiceHost(typeof(HDDInfo.HddInfo)))
            {
                host.Open();
                Console.WriteLine("Host is running...\r\n");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("IP {0} \r\nPort {1}\r\n", getIP(), 12345);
                Console.ResetColor();
                Console.WriteLine("Avaliable users:");
                Console.ForegroundColor = ConsoleColor.Cyan;
                
                var query = "SELECT Name FROM Win32_UserAccount WHERE Disabled = false";
                var s1 = new ManagementObjectSearcher(query);
                if (s1 != null)
                    foreach (var user in s1.Get())
                {
                    Console.WriteLine("Username: {0}", user["Name"]);
                }
                Console.ResetColor();
                Console.WriteLine();
                Console.ReadLine();
            }

           
        }

        private static string getIP()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }
    }
}
