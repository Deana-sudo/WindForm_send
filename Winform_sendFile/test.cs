using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Winform_sendFile
{
    class test
    {

        public test(byte[] fileBytes)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "All Files|*.*";
            saveFileDialog1.Title = "저장하기";
            saveFileDialog1.FileName = "test" + GetFileType();

            DialogResult dr = saveFileDialog1.ShowDialog();
            //byte[] buffer;

            // saveFileDialog1.FileName = "test" + fileType;

            if (dr == DialogResult.OK)
            {
                if (saveFileDialog1.FileName != "")
                {
                    string name = saveFileDialog1.FileName; //경로 + 파일명
                    FileStream file = new FileStream(name, FileMode.Create);

                    file.Write(fileBytes, 0, fileBytes.Length);
                    file.Close();
                }
            }
            string GetFileType()
            {
                //   string[] Array = filePath.Split('.');
                //   return "." + Array[Array.Length - 1];
                return "";
            }
        }
    }
}
