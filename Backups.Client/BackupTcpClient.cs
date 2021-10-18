using System.IO;
using System.Net.Sockets;
using Backups.Entities;

namespace Backups.Client
{
    public class BackupTcpClient
    {
        private readonly int _port;

        public BackupTcpClient(int port)
        {
            _port = port;
        }

        public void SendFileToServer(JobObject jobObject)
        {
            using var tcpClient = new TcpClient("127.0.0.1", _port);
            var sWriter = new StreamWriter(tcpClient.GetStream());

            byte[] bytes = File.ReadAllBytes(jobObject.FilePath);

            sWriter.WriteLine(bytes.Length.ToString());
            sWriter.Flush();

            sWriter.WriteLine(jobObject.FilePath);
            sWriter.Flush();
            tcpClient.Client.SendFile(jobObject.FilePath);
        }

        public void SendAmountOfFiles(int amount)
        {
            using var tcpClient = new TcpClient("127.0.0.1", _port);
            var sWriter = new StreamWriter(tcpClient.GetStream());
            
            sWriter.WriteLine(amount.ToString());
            sWriter.Flush();
        }
        
        public void SendBackupDirectory(string name)
        {
            using var tcpClient = new TcpClient("127.0.0.1", _port);
            var sWriter = new StreamWriter(tcpClient.GetStream());
            
            sWriter.WriteLine(name);
            sWriter.Flush();
        }
    }
}