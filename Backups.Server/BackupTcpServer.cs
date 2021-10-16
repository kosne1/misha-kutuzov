using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Backups.Server
{
    public class BackupTcpServer
    {
        private readonly TcpListener _tcpListener;
        private TcpClient _tcpClient;

        public BackupTcpServer(int port)
        {  
            _tcpListener = new TcpListener(IPAddress.Any, port);
            _tcpListener.Start();
        }

        public void AcceptTcpClient()
        {
            _tcpClient = _tcpListener.AcceptTcpClient();
        }

        public void ReceiveFile()
        {
            var reader = new StreamReader(_tcpClient.GetStream());
 
            string cmdFileSize = reader.ReadLine();
  
            string cmdFileName = reader.ReadLine();

            int length = Convert.ToInt32(cmdFileSize);
            byte[] buffer = new byte[length];
            int received = 0;
            int size = 1024;
  
            while (received < length)
            {
                int remaining = length - received;
                if (remaining < size)
                {
                    size = remaining;
                }

                int read = _tcpClient.GetStream().Read(buffer, received, size);
                received += read;
            }
            
            Console.Write(Path.GetFileName(cmdFileName));
            using var fStream = new FileStream(Path.GetFileName(cmdFileName), FileMode.Create);
            
            fStream.Write(buffer, 0, buffer.Length);
            fStream.Flush();
            fStream.Close();
        }
    }
}