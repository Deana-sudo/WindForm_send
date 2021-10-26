using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winform_receiveFile
{
    class FileSaver
    {
        public static void fileSave(byte[] recvBuffer, string filename)
        {
            string[] token = filename.Split('.');
            string NAME = token == null ? "TRUE" : "TempFileName";
            string file_extension = token[token.Count() -1]; //구분 필요함

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            if (file_extension != null)
            {
                saveFileDialog1.Filter = "*.BMP;*.JPG;*.GIF;*.JPEG;|*.BMP;*.JPG;*.GIF;*.JPEG;|*.AVI;*.MP4;*.MKV|*.AVI;*.MP4;*.MKV;|*.Zip;|*.Zip;| All files (*.*)|*.*";
            // 끝나고 필터값 구분할것 
            }
            else 
            {
                saveFileDialog1.Filter = "All Files|*.*";
            }
            saveFileDialog1.Title = "저장하기";
            saveFileDialog1.FileName = filename;
            saveFileDialog1.AutoUpgradeEnabled = false; // 디폴트는 true 값이나 true일경우 폼이 나오지않는경우가 있음

            DialogResult result = saveFileDialog1.ShowDialog();
   
            if (result == DialogResult.OK)
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
