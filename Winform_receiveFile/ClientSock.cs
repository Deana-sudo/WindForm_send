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

    public class DataObj
    {
        Socket ActualSocket = null;
        public int bufferSize = 0;
        public byte[] buffer;


        public DataObj(int bufferSize)
        {
            this.buffer = new byte[bufferSize];
        }
    }
    class ClientSock
    {
        //public byte[] recvBuffer = new byte[4096];
        //private byte[] recvBuffer;
        //private string FileName = "Downloaded_File.png";
        //BinaryWriter writer = null;
        // string selected = "";



        public ClientSock(String IP, int port)
        {
         //   Socket socket;
             
          //  byte[] recvBuffer = new byte[4096];
        //    Socket Client;
            IPEndPoint ep;
            byte[] recvBuffer;
            byte[] receivedBufferSize = new byte[1024];
            int bufferSize;
            byte[] fileNameSizeBuffer = new byte[4];
            byte[] fileNameSize;
            string fileName;


            Socket Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

                ep = new IPEndPoint(IPAddress.Parse(IP), port);

                Client.BeginConnect(ep, ConnectCallBack, Client);

                //Client.BeginConnect(ep, ConnectCallBack, 0);

            //Client.BeginConnect(ep,ConnectCallBack,0);

            void ConnectCallBack(IAsyncResult ar)
            {
                Socket temp = (Socket)ar.AsyncState;

                //temp.EndConnect(ar);
                //socket = temp;
                MessageBox.Show("Connected to : " + temp.RemoteEndPoint.ToString());

                temp.Receive(receivedBufferSize);
                bufferSize = BitConverter.ToInt32(receivedBufferSize,0);
                recvBuffer = new byte[bufferSize];

                temp.Receive(fileNameSizeBuffer);
                int size = BitConverter.ToInt32(fileNameSizeBuffer, 0);


                fileNameSize = new byte[size];
                temp.Receive(fileNameSize);
                fileName = Encoding.Default.GetString(fileNameSize);


                temp.BeginReceive(recvBuffer, 0, recvBuffer.Length, SocketFlags.None, ReceiveCallBack, temp);

            }
            void ReceiveCallBack(IAsyncResult ar)
            {

                Socket transferSock = ar.AsyncState as Socket;

                MessageBox.Show("starting Receive : " + transferSock.RemoteEndPoint.ToString());
                FileSaver.fileSave(recvBuffer,fileName);
                MessageBox.Show("Save Completed. Shutting Donw");
                

                //MessageBox.Show(BitConverter.ToString(recvBuffer));



                transferSock.EndReceive(ar);

                transferSock.Close();


                //Socket sock = (Socket)ar.AsyncState;

                //byte[] buffer = new byte[4];
                //sock.Receive(buffer);       //파일 크기를 받는다.
                //int fileLength = BitConverter.ToInt32(buffer, 0);

                //// 파일 이름 사이즈
                //buffer = new byte[4];
                //sock.Receive(buffer);       //파일 이름의 크기를 받는다.
                //int fileNameLength = BitConverter.ToInt32(buffer, 0);

                //// 파일 이름
                //buffer = new byte[fileNameLength];
                //sock.Receive(buffer);       //파일 이름을 받는다.
                //string fileName = Encoding.Default.GetString(buffer);
                ////MessageBox.Show("  [2] 파일이름 " + fileName);

                //// 파일
                //buffer = new byte[1024];
                //int totalLength = 0;
                ////파일이름 = 디렉토리 + 파일이름

                //string dirName = Path + "\\" + fileName;        //위에서 선택한 경로에 파일이름을 붙힘
                //FileStream fileStream = new FileStream(
                //    dirName, FileMode.Create, FileAccess.Write);        //파일 생성모드

                //writer = new BinaryWriter(fileStream);

                //while (totalLength < fileLength)        //전체 파일을 받는다.
                //{
                //    int receiveLength = sock.Receive(buffer);

                //    writer.Write(buffer, 0, receiveLength);

                //    totalLength += receiveLength;
                //}


                //int strLength = sock.EndReceive(ar);
            }


            //void ByteToStream(byte[] File)
            //{
            //    MemoryStream stream = new MemoryStream();

            //    stream.Write(File, 0, File.Length);


            //    return ;
            //}
        }
        

    }
}
