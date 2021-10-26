using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Winform_sendFile
{
    class ByteDivider
    {
        public ByteDivider(string Path, int BUFFER_SIZE_VALUE = 4096) // file path, Divided Buffer Size (Default = 4096)
        {
            using (FileStream stream = new FileStream(Path, FileMode.Open))
            {

                byte[] fileBytes = new byte[stream.Length];
                byte[] loop_Byte = new byte[BUFFER_SIZE_VALUE]; // 4096
                byte[] total = new byte[stream.Length];

                int loop_Count = fileBytes.Length / BUFFER_SIZE_VALUE;
                int remain = fileBytes.Length % BUFFER_SIZE_VALUE;

                for (int i = 0; i < loop_Count; i++)
                {
                    stream.Read(fileBytes, i * BUFFER_SIZE_VALUE, BUFFER_SIZE_VALUE); // sender
                    Array.Copy(fileBytes, i * BUFFER_SIZE_VALUE, loop_Byte, 0, BUFFER_SIZE_VALUE); //sender
                    Array.Copy(loop_Byte, 0, total, i * BUFFER_SIZE_VALUE, BUFFER_SIZE_VALUE); // receiver
                    Array.Clear(loop_Byte, 0, loop_Byte.Length);  // receiver
                    //Socekt.Send
                }
                if (remain > 0)
                {
                    stream.Read(fileBytes, loop_Count * BUFFER_SIZE_VALUE, remain);
                    loop_Byte = new byte[remain];
                    Array.Copy(fileBytes, loop_Count * BUFFER_SIZE_VALUE, loop_Byte, 0, remain);
                    Array.Copy(loop_Byte, 0, total, loop_Count * BUFFER_SIZE_VALUE, remain);

                    //socket.Send
                }
                stream.Close();

                //test.Write(fileBytes, 0, fileBytes.Length);
                FileStream file = new FileStream("D:\\asdas.gif", FileMode.Create);

                file.Write(total, 0, total.Length);
                file.Close();

            }


        }
    }
}
