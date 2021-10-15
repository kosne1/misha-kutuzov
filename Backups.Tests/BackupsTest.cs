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

        [Test]
        public void CreateBackupJobAddTwoFiles_CheckThatFilesAndDirectoriesWereCreated()
        {
            const string backupDirPath = @"D:\backups\Test2SingleStorage";
            var dirInfo = new DirectoryInfo(backupDirPath);

            const string backupJobName = "Test2SingleStorage";
            var backupJob = new BackupJob(backupJobName, dirInfo);
            IStorageAlgorithm singleStorage = new SingleStorage();
            backupJob.SetStorageAlgorithm(singleStorage);

            const string firstFilePath = @"C:\Users\misha\Documents\Test2\c.txt";
            const string secondFilePath = @"C:\Users\misha\Documents\Test2\d.txt";

            backupJob.AddFiles(firstFilePath, secondFilePath);
            backupJob.CreateRestorePoint();

            Assert.IsTrue(dirInfo.Exists);

            int dirs = dirInfo.GetDirectories().Length;
            Assert.AreEqual(backupJob.RestorePointsCounter, dirs);
        }
    }
}