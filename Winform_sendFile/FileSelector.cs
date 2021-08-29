using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winform_sendFile
{
    class FileSelector
    {
        public string fileString;
        public FileSelector(int port)
        {
            OpenFileDialog fileS = new OpenFileDialog();
            DialogResult realfile = fileS.ShowDialog();
            if (realfile == DialogResult.OK)
            {
                fileString = fileS.FileName;
                try
                {
                    Server server = new Server(port,fileString);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Server not Started." + e.Message);
                }
            }
        }
    }
}
