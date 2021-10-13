using System.IO;
using Backups.Entities;
using Backups.StorageAlgorithms;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupsTest
    {
        [Test]
        public void CreateBackupJobAddTwoFilesDeleteOne_InBackupTwoRestorePointsThreeStorages()
        {
            string backupJobName = "Test1SplitStorage";
            var backupJob = new BackupJob(backupJobName);
            IStorageAlgorithm splitStorage = new SplitStorage();
            backupJob.SetStorageAlgorithm(splitStorage);

            string firstFilePath = @"C:\Users\misha\Documents\Test1\a.txt";
            string secondFilePath = @"C:\Users\misha\Documents\Test1\b.txt";

            backupJob.AddFiles(firstFilePath, secondFilePath);
            backupJob.CreateRestorePoint();

            string path = $@"D:\backups\{backupJobName}";
            string[] dirs = Directory.GetDirectories(path);
            Assert.AreEqual(backupJob.RestorePointsCounter, dirs.Length);
        }
    }
}