using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace Winform_sendFile
{
    public partial class ServerWin : Form
    {
        //값은 변경 윈폼에서 지정 가능하게 텍스트박스 지정

        public ServerWin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int port;
            port = Int32.Parse(textBox2.Text);

            try
            {
                FileSelector file = new FileSelector(port); // 실제 시작
                //Server server = new Server(port);
                textBox1.Text = file.fileString;
            }
            catch(Exception a)
            {
                MessageBox.Show("Port number not entered" + a .Message);
            }
            button1.Enabled = false;
                
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back) || e.KeyChar == '.'))
            {
                e.Handled = true;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void IP_Check_Click(object sender, EventArgs e)
        {
            ipText.Text = GetExternalIPAddress();
        }
        public static string GetExternalIPAddress() //외부 아이피 (59.219.....)
        {
            string externalip = new WebClient().DownloadString("http://ipinfo.io/ip").Trim(); //http://icanhazip.com

            if (String.IsNullOrWhiteSpace(externalip))
            {
                externalip = GetInternalIPAddress();//null경우 Get Internal IP를 가져오게 한다.
            }

            return externalip;
        }
        public static string GetInternalIPAddress() //내부 아이피 (192.168....)
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            throw new Exception("No network adapters with an IPv4 address in the system!");
        }





    }
}
