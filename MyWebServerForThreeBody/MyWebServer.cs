using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using MyThreeBodySimulator;


namespace MyWebServerForThreeBody
{
    class MyWebServer
    {
        private string _ip;
        private int _port;
        private Socket _socket;
        private byte[] buffer = new byte[1024 * 1024 * 20];
        private MyEnvironment _env;

        public MyWebServer(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }

        public MyWebServer(int port=8899)
        {
            _ip = "0.0.0.0";
            _port = port;
        }

        public void StartListen()
        {
            try
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Parse(_ip);
                IPEndPoint endPoint = new IPEndPoint(ip, _port);
                _socket.Bind(endPoint);
                _socket.Listen(1);
                Thread thread = new Thread(ListenConnect);
                thread.Start();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void ListenConnect()
        {
            try
            {
                while (true)
                {
                    Socket client = _socket.Accept();
                    _env = new MyEnvironment();
                    Thread thread = new Thread(this.ReceiveMessage);
                    thread.Start(client);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void ReceiveMessage(object client)
        {
            Socket clientSocket = (Socket)client;
            while (true)
            {
                try
                {
                    //获取从客户端发来的数据
                    int length = clientSocket.Receive(buffer);
                    if (length>0)
                    {
                        string cmd = Encoding.UTF8.GetString(buffer, 0, length);
                        var proResult=MyDataFormat.ParseInputJson(cmd,_env);
                        string outputJson = MyDataFormat.ParseOutput(proResult);
                        Console.WriteLine(cmd);
                        clientSocket.Send(Encoding.UTF8.GetBytes(outputJson));
                        
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    break;
                }
            }


        }

    }
}
