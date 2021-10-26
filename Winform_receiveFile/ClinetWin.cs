using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winform_receiveFile
{
    public partial class Socket_Client : Form
    {
        private String ipForConnect;
        private int port;
        string path = "";
        public Socket_Client()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ipForConnect = IpEnter.Text;
            port = Int32.Parse(PortEnter.Text);

            using (var dialog = new FolderBrowserDialog())      //어디다 저장할지 저장위치를 검색할 수 있게한다.
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    path = dialog.SelectedPath;
                    MessageBox.Show("아니" + path);
                }
            }

            ClientSock Starting = new ClientSock(ipForConnect, port, path);

            //FilePathSelector StartingNow = new FilePathSelector(ipForConnect,port);

            //fileSave(Starting.recvBuffer); 
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void IpEnter_TextChanged(object sender, EventArgs e)
        {

        }
        void fileSave(byte[] recvBuffer)
        {

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "All Files|*.*";
            saveFileDialog1.Title = "저장하기";
            saveFileDialog1.FileName = "test";

            DialogResult dr = saveFileDialog1.ShowDialog();
            //byte[] buffer;

            // saveFileDialog1.FileName = "test" + fileType;

            if (dr == DialogResult.OK)
            {
                if (saveFileDialog1.FileName != "")
                {
                    string name = saveFileDialog1.FileName; //경로 + 파일명
                    FileStream file = new FileStream(name, FileMode.Create);

                    file.Write(recvBuffer, 0, recvBuffer.Length);
                    file.Close();
                }
            }
        }


    }
}
