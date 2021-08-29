using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;

namespace Winform_sendFile
{
    // 비동기 작업에서 사용하는 소켓과 해당 작업에 대한 데이터 버퍼를 저장하는 클래스
    //-------------
    class Server
    {
        List<Socket> ClientSockList = new List<Socket>();

        class Dataobj
        {
            public byte[] buffer;
            public byte[] bufferSize;
            public byte[] fileNameBuffer;
            public byte[] fileNameSize;
            //public int bufferSize;
            //public string filePath;
            public Socket ActualSocket; // ????
            public Dataobj(Int32 size)
            {
                this.buffer = new byte[size];
              //  bufferSize = this.buffer.Length;
            }
            public void ClearBuffer()
            {
                Array.Clear(buffer,0,buffer.Length);
            }
        }
        public Server(int port, string fileName)
        {
            byte[] SendData = FileToByteArray(fileName);
            Socket mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Any, port);

            string[] token = fileName.Split('\\');
            string o = token[token.Count() - 1];

            mainSocket.Bind(serverEP);
            mainSocket.Listen(3);
            mainSocket.BeginAccept(AcceptCallback, mainSocket);

            void AcceptCallback(IAsyncResult ar)
            {

                Socket Listener = ar.AsyncState as Socket;
                mainSocket.BeginAccept(AcceptCallback, mainSocket);


                Socket handler = Listener.EndAccept(ar);

                Dataobj DO = new Dataobj(SendData.Length);

                DO.bufferSize = BitConverter.GetBytes(SendData.Length);
                handler.Send(DO.bufferSize);

                DO.fileNameSize = BitConverter.GetBytes(o.Length);
                handler.Send(DO.fileNameSize);

                DO.fileNameBuffer = Encoding.Default.GetBytes(o);
                handler.Send(DO.fileNameBuffer);

                DO.buffer = SendData;

                handler.BeginSend(DO.buffer, 0, DO.buffer.Length, SocketFlags.None, sendStr,DO);


            }
            void sendStr(IAsyncResult ar)
            {

                Dataobj DO = ar.AsyncState as Dataobj;
                Socket handler = DO.ActualSocket;

                //  int bytesSend = handler.EndSend(ar);
                //handler.Send(DO.Buffer);
                //Dataobj DO = ar.AsyncState as Dataobj;
                //DO.ActualSocket.EndSend(ar);

                handler.EndSend(ar);

            }
        }
        byte[] FileToByteArray(string fileName) // 파일을 바이트러 변환해 반환하는 함수, 매개변수는 파일 경로(파일확장자명까지 포함)
        {
            byte[] fileBytes = null;
            try
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
                {
                    fileBytes = new byte[fileStream.Length];
                    fileStream.Read(fileBytes, 0, fileBytes.Length);
                }
            }
            catch (Exception e)
            {

            }
            return fileBytes;
        }
    }
}

//void SendCallback(IAsyncResult ar)
//{
//    for (int i = ClientSockList.Count - 1; i >= 0; i--)
//    {
//        //Socket socket = ClientSockList[i];

//        // 핸들 값을 비교하여 이 데이터를 보낸 소켓인지 구분한다.

//           // 데이터를 보낸 소켓이 아니라면
//            // 수신받은 데이터를 전달해준다.
//         // socket.Send();

//    }
//}