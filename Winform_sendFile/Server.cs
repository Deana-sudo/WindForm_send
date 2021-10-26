using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace Winform_sendFile
{
    // 비동기 작업에서 사용하는 소켓과 해당 작업에 대한 데이터 버퍼를 저장하는 클래스
    //-------------
    class Server
    {
        const int BUFFER_SIZE_VALUE = 512;
        byte[] fileBuffer;

        public Server(int port, string fileName)
        {

            Socket mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Any, port);

            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            
            string[] token = fileName.Split('\\');
            string o = token[token.Count() - 1]; // file name
            int fileLength = (int)fileStream.Length;

            mainSocket.Bind(serverEP);
            mainSocket.Listen(3);
            mainSocket.BeginAccept(AcceptCallback, mainSocket);

            void AcceptCallback(IAsyncResult ar)
            {


                Socket Listener = ar.AsyncState as Socket;
                //mainSocket.BeginAccept(AcceptCallback, mainSocket);


                Socket sock = Listener.EndAccept(ar);


                // 파일 크기 전송
                fileLength = (int)fileStream.Length; 
                fileBuffer = BitConverter.GetBytes(fileLength);
                sock.Send(fileBuffer);
                //Array.Clear(fileBuffer, 0, fileBuffer.Length);
                //MessageBox.Show("파일 크기 : " + fileLength);

                //파일 이름 크기 전송
                int fileNameLength = o.Length; 
                fileBuffer = BitConverter.GetBytes(fileNameLength);
                sock.Send(fileBuffer);
                //MessageBox.Show("파일 이름 크기 : " + fileNameLength);



                //파일 이름 전송 -- Begin으로 비동기 전송
                fileBuffer = Encoding.UTF8.GetBytes(o);
                sock.BeginSend(fileBuffer, 0, fileBuffer.Length, SocketFlags.None, sendStr, sock);
                 
            }
            void sendStr(IAsyncResult ar)
            {

                Socket sock = ar.AsyncState as Socket;

                int loop_Count = fileLength / BUFFER_SIZE_VALUE;

                BinaryReader reader = new BinaryReader(fileStream);

                for (int i = 0; i < loop_Count; i++)
                {
                    fileBuffer = reader.ReadBytes(BUFFER_SIZE_VALUE);

                    sock.Send(fileBuffer);
                }
                MessageBox.Show("Done");
                reader.Close();
                //sock.Close();

            }


        }
    }
}