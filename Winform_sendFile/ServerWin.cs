using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    }
}
