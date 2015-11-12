using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace GetHddIngoHost
{
    class Program
    {
        static void Main()
        {
            using (var host = new ServiceHost(typeof(HDDInfo.HddInfo)))
            {
                host.Open();
                Console.WriteLine("Host is running...");
                Console.ReadLine();
                host.Close();
            }

           
        }
    }
}
