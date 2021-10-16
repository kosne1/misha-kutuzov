using System;
using System.IO;
using System.Net.Sockets;

namespace Backups.Client
{
    public class BackupTcpClient : IDisposable
    {
        private readonly TcpClient _tcpClient;

        public BackupTcpClient(int port)
        {
            _tcpClient = new TcpClient("127.0.0.1", port);
        }

        public void SendFileToServer(string filePath)
        {
            var sWriter = new StreamWriter(_tcpClient.GetStream());

            byte[] bytes = File.ReadAllBytes(filePath);

            sWriter.WriteLine(bytes.Length.ToString());
            sWriter.Flush();

            sWriter.WriteLine(filePath);
            sWriter.Flush();

            _tcpClient.Client.SendFile(filePath);
        }

        public void Dispose()
        {
            _tcpClient.Dispose();
        }
    }
}