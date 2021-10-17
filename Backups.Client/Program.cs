using System;
using System.Collections.Generic;

namespace Backups.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var backupTcpClient = new BackupTcpClient(1234);

            const string backupName = "TestServer";

            const string firstFilePath = @"C:\Users\misha\Documents\Test2\c.txt";
            const string secondFilePath = @"C:\Users\misha\Documents\Test2\d.txt";
            var filePaths = new List<string>
            {
                firstFilePath,
                secondFilePath
            };

            backupTcpClient.SendBackupJobName(backupName);
            backupTcpClient.SendAmountOfFiles(filePaths.Count);

            foreach (string filePath in filePaths)
            {
                try
                {
                    backupTcpClient.SendFileToServer(filePath);
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
            }
        }
    }
}