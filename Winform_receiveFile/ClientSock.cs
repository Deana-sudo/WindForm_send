using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Winform_receiveFile
{
    class ClientSock
    {
        //const int BUFFER_SIZE_VALUE = 1024;

        public ClientSock(string IP, int port, string path)
        {
            IPEndPoint ep;
            int bufferSize;
            int fileNameSize;
            string fileNameString;
            byte[] fileBuffer;
            BinaryWriter writer = null;


            Socket Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

                ep = new IPEndPoint(IPAddress.Parse(IP), port);

                Client.BeginConnect(ep, ConnectCallBack, Client);


            void ConnectCallBack(IAsyncResult ar)
            {
                try
                {
                    Socket sock = (Socket)ar.AsyncState;


                    // 파일 사이즈 수신
                    fileBuffer = new byte[32];
                    sock.Receive(fileBuffer);
                    bufferSize = BitConverter.ToInt32(fileBuffer, 0);
                    //MessageBox.Show("파일 크기 : " + bufferSize);

                    //파일 이름 사이즈 수신
                    fileBuffer = new byte[32];
                    sock.Receive(fileBuffer);
                    fileNameSize = BitConverter.ToInt32(fileBuffer, 0);
                    //MessageBox.Show("파일 이름 크기(Client) : " + fileNameSize);


                    //파일 이름 수신
                    fileBuffer = new byte[fileNameSize];
                    sock.BeginReceive(fileBuffer, 0, fileBuffer.Length, SocketFlags.None, ReceiveCallBack, sock);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error : " + e.Message);
                }


            }
            void ReceiveCallBack(IAsyncResult ar)
            {
                Socket sock = ar.AsyncState as Socket;
                fileNameString = Encoding.Default.GetString(fileBuffer);

                int total = 0;

                string dirName = path + fileNameString; // dirName = C:\example\example.abc
                FileStream fileStream = new FileStream(dirName, FileMode.Create); // -> 멈춤
                writer = new BinaryWriter(fileStream);

                // 프로그래스 바 윈도우 폼 시작
                // 쓰레드 / 백그라운드 워커 사용 여부 ---- ???
                //frm.Show();

                //ProgressBytesMaxValue = bufferSize; // 파일 크기 지정
                //ProgressBytesValue = total;



                while (total < bufferSize)
                {
                    int receiveLength = sock.Receive(fileBuffer); 

                    writer.Write(fileBuffer, 0, receiveLength);

                    total += receiveLength;
 
                    // 스레드 혹은 백그라운드워커 사용해서 얼마나 읽었는지 Progreebar 윈폼에 값 전달
                }
                writer.Close();
                fileStream.Close();
                sock.Close();

                MessageBox.Show("다운로드 완료 ");
            }   
        }
    }
}
