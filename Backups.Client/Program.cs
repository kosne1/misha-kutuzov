using System;
using System.Collections.Generic;
using Backups.Entities;

namespace Backups.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var backupTcpClient = new BackupTcpClient("127.0.0.1", 1234);

            const string backupDirPath = @"D:\Backups\Test3Server";

            var firstFile = new JobObject(@"C:\Users\misha\Documents\Test2\c.txt");
            var secondFile = new JobObject(@"C:\Users\misha\Documents\Test2\d.txt");
            var filePaths = new List<JobObject>
            {
                firstFile,
                secondFile
            };

            backupTcpClient.SendBackupDirectory(backupDirPath);
            backupTcpClient.SendAmountOfFiles(filePaths.Count);

            foreach (JobObject jobObject in filePaths)
            {
                try
                {
                    backupTcpClient.SendFileToServer(jobObject);
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
            }
        }
    }
}