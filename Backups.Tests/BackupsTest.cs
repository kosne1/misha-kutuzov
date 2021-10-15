using System.IO;
using System.Linq;
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
            const string backupJobName = "Test1SplitStorage";
            var backupJob = new BackupJob(backupJobName);
            IStorageAlgorithm splitStorage = new SplitStorage();
            backupJob.SetStorageAlgorithm(splitStorage);

            const string firstFilePath = @"C:\Users\misha\Documents\Test1\a.txt";
            const string secondFilePath = @"C:\Users\misha\Documents\Test1\b.txt";

            backupJob.AddFiles(firstFilePath, secondFilePath);
            backupJob.CreateRestorePoint();

            string path = $@"D:\backups\{backupJobName}";
            int dirs = Directory.GetDirectories(path).Length;
            Assert.AreEqual(backupJob.RestorePointsCounter, dirs);

            File.Delete(firstFilePath);
            backupJob.CreateRestorePoint();

            dirs = Directory.GetDirectories(path).Length;
            Assert.AreEqual(backupJob.RestorePointsCounter, dirs);

            int actualStoragesCount = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Length;
            int storagesCount = backupJob.Backup.RestorePoints.Sum(restorePoint => restorePoint.Storages.Count);
            
            Assert.AreEqual(actualStoragesCount, storagesCount);
        }
    }
}