using System;
using Backups.Entities;
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

            string backupJobName = backupTcpServer.ReceiveBackupName();

            var backupJob = new BackupJob();
            IRepository repository = new ServerRepository(backupJobName);
            backupJob.Repository = repository;
            IStorageAlgorithm storageAlgorithm = new SplitStorage();
            backupJob.StorageAlgorithm = storageAlgorithm;

            int amount = backupTcpServer.ReceiveAmountOfFiles();

            int counter = 0;
            while (true)
            {
                string file = backupTcpServer.ReceiveFile();

                backupJob.AddFiles(file);

                counter++;

                if (counter == amount) break;
            }

            backupJob.CreateRestorePoint();
        }
    }
}