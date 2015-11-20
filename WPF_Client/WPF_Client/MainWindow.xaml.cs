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
        ConnectionInfo _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            TB_ip_1.Focus();
            _viewModel = (ConnectionInfo)base.DataContext;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var IP = "169.254.72.217";
            //var Port = "12345";
            //var IP = "95.84.228.121";
            //var Port = "12345";

            var IP = string.Format("{0}.{1}.{2}.{3}", TB_ip_1.Text, TB_ip_2.Text, TB_ip_3.Text, TB_ip_4.Text);
            var Port = TB_port.Text;

            NetTcpBinding b = new NetTcpBinding();
            b.Security.Mode = SecurityMode.Transport;
            b.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            EndpointAddress ea = new EndpointAddress(new Uri("net.tcp://" + IP + ":" + Port + "/HddInfo"));

            var client = new ServiceReference1.HddInfoClient(b, ea);
            client.Endpoint.Address = new EndpointAddress(new Uri("net.tcp://" + IP + ":" + Port + "/HddInfo"));

            client.ClientCredentials.Windows.ClientCredential.UserName = TB_UserName.Text;
            client.ClientCredentials.Windows.ClientCredential.Password = TB_Password.Text;

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

        private void TB_ip_1_GotFocus(object sender, RoutedEventArgs e)
        {
            TB_ip_1.SelectAll();
        }

        private void TB_ip_2_GotFocus(object sender, RoutedEventArgs e)
        {
            TB_ip_2.SelectAll();
        }

        private void TB_ip_3_GotFocus(object sender, RoutedEventArgs e)
        {
            TB_ip_3.SelectAll();
        }

        private void TB_ip_4_GotFocus(object sender, RoutedEventArgs e)
        {
            TB_ip_4.SelectAll();
        }

        private void TB_port_GotFocus(object sender, RoutedEventArgs e)
        {
            TB_port.SelectAll();
        }

        private void TB_UserName_GotFocus(object sender, RoutedEventArgs e)
        {
            TB_UserName.SelectAll();
        }

        private void TB_Password_GotFocus(object sender, RoutedEventArgs e)
        {
            TB_Password.SelectAll();
        }
    }
}
