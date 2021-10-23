using System.IO;
using System.Net.Sockets;
using Backups.Entities;

namespace Backups.Client
{
    public class BackupTcpClient : System.IDisposable
    {
        private readonly int _port;
        private TcpClient _tcpClient;

        public BackupTcpClient(string hostname, int port)
        {
            _port = port;
            _tcpClient = new TcpClient(hostname, port);
        }

        public void SendFileToServer(JobObject jobObject)
        {
            var sWriter = new StreamWriter(_tcpClient.GetStream());

            byte[] bytes = File.ReadAllBytes(jobObject.FilePath);

            sWriter.WriteLine(bytes.Length.ToString());
            sWriter.Flush();

            sWriter.WriteLine(jobObject.FilePath);
            sWriter.Flush();
            _tcpClient.Client.SendFile(jobObject.FilePath);
        }

        public void SendAmountOfFiles(int amount)
        {
            var sWriter = new StreamWriter(_tcpClient.GetStream());
            
            sWriter.WriteLine(amount.ToString());
            sWriter.Flush();
        }
        
        public void SendBackupDirectory(string name)
        {
            var sWriter = new StreamWriter(_tcpClient.GetStream());
            
            sWriter.WriteLine(name);
            sWriter.Flush();
        }

        public void Dispose()
        {
            _tcpClient.Dispose();
        }
    }
}