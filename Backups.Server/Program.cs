﻿using System;
using System.IO;
using Backups.Archivers;
using Backups.Entities;
using Backups.Logger;
using Backups.Repositories;
using Backups.StorageAlgorithms;

namespace Backups.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            // Listen on port 1234    
            var backupTcpServer = new BackupTcpServer(1234);

            string backupDirPath = backupTcpServer.ReceiveBackupDirectory();

            var backupJob = new BackupJob(new ConsoleLogger());

            backupJob.SetArchiver(new BackupZipArchiver(new SplitStorage()));

            var dirInfo = new DirectoryInfo(backupDirPath);
            backupJob.SetRepository(new LocalRepository(dirInfo));

            int amount = backupTcpServer.ReceiveAmountOfFiles();

            int counter = 0;
            while (true)
            {
                string file = backupTcpServer.ReceiveFile();

                backupJob.AddJobObject(new JobObject(file));

                counter++;

                if (counter == amount) break;
            }

            backupJob.CreateRestorePoint(DateTime.Now);
        }
    }
}