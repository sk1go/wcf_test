using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            var client = new ServiceReference1.HddInfoClient("NetTcpBinding_IHddInfo");
            //var res1 = client.Test();
            try
            {
                var res3 = client.TestMyObject();
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }

            //listBox1.Items.Add(res3.)
            //var res2 = client.GetHddInfo();
        }
    }
}
