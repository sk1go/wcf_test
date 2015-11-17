using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var IP = "169.254.72.217";
            var Port = "12345";
            //var client = new ServiceReference1.HddInfoClient("NetTcpBinding_IHddInfo");
            //client.Endpoint.Address = new EndpointAddress(new Uri("net.tcp://" + IP + ":" + Port + "/HddInfo"));

            NetTcpBinding b = new NetTcpBinding();
            b.Security.Mode = SecurityMode.Transport;
            b.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            EndpointAddress ea = new EndpointAddress(new Uri("net.tcp://" + IP + ":" + Port + "/HddInfo"));

            var client = new ServiceReference1.HddInfoClient(b, ea);
            client.ClientCredentials.Windows.ClientCredential.UserName = "test_user";
            client.ClientCredentials.Windows.ClientCredential.Password = "123"; 

            try
            {
                var res3 = client.GetHddInfo();
                foreach(var item in res3)
                {
                    listBox1.Items.Add("===================================");
                    listBox1.Items.Add(item.Caption);
                    listBox1.Items.Add(item.DeviceID);
                    listBox1.Items.Add(item.Status);
                    listBox1.Items.Add(item.Size);
                    foreach (var ld in item.LogicalDrives)
                    {
                        listBox1.Items.Add(ld);
                    }
                    listBox1.Items.Add("===================================");
                }
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
        }
    }
}
