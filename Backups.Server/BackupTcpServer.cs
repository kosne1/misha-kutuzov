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

        public string ReceiveFile()
        {
            _tcpClient = _tcpListener.AcceptTcpClient();
            var reader = new StreamReader(_tcpClient.GetStream());

            string cmdFileSize = reader.ReadLine();

            string cmdFileName = reader.ReadLine();
            
            Console.WriteLine(cmdFileSize);
            Console.WriteLine(cmdFileName);

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

            using var fStream = new FileStream(Path.GetFileName(cmdFileName), FileMode.Create);

            fStream.Write(buffer, 0, buffer.Length);
            fStream.Flush();
            fStream.Close();

            return Path.GetFileName(cmdFileName);
        }

        public int ReceiveAmountOfFiles()
        {
            _tcpClient = _tcpListener.AcceptTcpClient();

            var reader = new StreamReader(_tcpClient.GetStream());

            string filesAmount = reader.ReadLine();

            return Convert.ToInt32(filesAmount);
        }

        public string ReceiveBackupDirectory()
        {
            _tcpClient = _tcpListener.AcceptTcpClient();

            var reader = new StreamReader(_tcpClient.GetStream());

            string backupName = reader.ReadLine();

            return backupName;
        }
    }
}