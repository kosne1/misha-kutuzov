using System;
using System.IO;
using System.Net.Sockets;

namespace Backups.Client
{
    public class BackupTcpClient
    {
        private readonly int _port;

        public BackupTcpClient(int port)
        {
            _port = port;
        }

        public void SendFileToServer(string filePath)
        {
            using var tcpClient = new TcpClient("127.0.0.1", _port);
            var sWriter = new StreamWriter(tcpClient.GetStream());

            byte[] bytes = File.ReadAllBytes(filePath);

            sWriter.WriteLine(bytes.Length.ToString());
            sWriter.Flush();

            sWriter.WriteLine(filePath);
            sWriter.Flush();
            tcpClient.Client.SendFile(filePath);
        }

        public void SendAmountOfFiles(int amount)
        {
            using var tcpClient = new TcpClient("127.0.0.1", _port);
            var sWriter = new StreamWriter(tcpClient.GetStream());
            
            sWriter.WriteLine(amount.ToString());
            sWriter.Flush();
        }
        
        public void SendBackupJobName(string name)
        {
            using var tcpClient = new TcpClient("127.0.0.1", _port);
            var sWriter = new StreamWriter(tcpClient.GetStream());
            
            sWriter.WriteLine(name);
            sWriter.Flush();
        }
    }
}