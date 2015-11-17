using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ServiceModel;

namespace WPF_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var IP = "169.254.72.217";
            //var Port = "12345";
            TB_ip_1.Text = "169";
            TB_ip_2.Text = "254";
            TB_ip_3.Text = "72";
            TB_ip_4.Text = "217";
            TB_port.Text = "12345";

            var IP = string.Format("{0}.{1}.{2}.{3}", TB_ip_1.Text, TB_ip_2.Text, TB_ip_3.Text, TB_ip_4.Text);
            var Port = TB_port.Text;

            var client = new ServiceReference1.HddInfoClient("NetTcpBinding_IHddInfo");
            client.Endpoint.Address = new EndpointAddress(new Uri("net.tcp://" + IP + ":" + Port + "/HddInfo"));

            client.ClientCredentials.UserName.UserName = "test_user";
            client.ClientCredentials.UserName.Password = "1234";
            try
            {
                var res3 = client.GetHddInfo();
                foreach (var item in res3)
                {
                    LB_result.Items.Add("===================================");
                    LB_result.Items.Add(item.Caption);
                    LB_result.Items.Add(item.DeviceID);
                    LB_result.Items.Add(item.Status);
                    LB_result.Items.Add(item.Size);
                    foreach (var ld in item.LogicalDrives)
                    {
                        LB_result.Items.Add(ld);
                    }
                    LB_result.Items.Add("===================================");
                }
            }
            catch (Exception ex)
            {
                LB_result.Items.Add(ex.Message);
            }
        }
    }
}
