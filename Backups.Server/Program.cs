using System.IO;
using Backups.Archivers;
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

            string backupDirPath = backupTcpServer.ReceiveBackupDirectory();

            var backupJob = new BackupJob();

            backupJob.Archiver = new BackupZipArchiver(new SplitStorage());

            var dirInfo = new DirectoryInfo(backupDirPath);
            backupJob.Repository = new LocalRepository(dirInfo);

            int amount = backupTcpServer.ReceiveAmountOfFiles();

            int counter = 0;
            while (true)
            {
                string file = backupTcpServer.ReceiveFile();

                backupJob.AddJobObject(new JobObject(file));

                counter++;

                if (counter == amount) break;
            }

            backupJob.CreateRestorePoint();
        }
    }
}