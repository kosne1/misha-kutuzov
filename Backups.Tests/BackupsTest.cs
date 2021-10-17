using System.IO;
using System.Linq;
using Backups.Entities;
using Backups.Repositories;
using Backups.StorageAlgorithms;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupsTest
    {
        [Test]
        public void CreateBackupJobAddTwoFilesDeleteOne_InBackupTwoRestorePointsThreeStorages()
        {
            var backupJob = new BackupJob();
            IStorageAlgorithm splitStorage = new SplitStorage();

            const string backupJobName = "Test1SplitStorage";
            IRepository repository = new ComputerRepository(backupJobName);
            backupJob.StorageAlgorithm = splitStorage;
            backupJob.Repository = repository;

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
            var backupJob = new BackupJob();

            IStorageAlgorithm singleStorage = new SingleStorage();
            backupJob.StorageAlgorithm = singleStorage;
            
            const string backupDirPath = @"D:\backups\Test2SingleStorage";
            var dirInfo = new DirectoryInfo(backupDirPath);
            IRepository repository = new ComputerRepository(dirInfo);
            backupJob.Repository = repository;

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