using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Archivers;
using Backups.Entities;
using Backups.Repositories;
using Backups.StorageAlgorithms;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupsTest
    {
        [Test]
        [Ignore("No trash for computer")]
        public void CreateBackupJobAddTwoFilesDeleteOne_InBackupTwoRestorePointsThreeStorages()
        {
            var backupJob = new BackupJob();

            const string backupPath = @"D:\Backups\Test1SplitStorage";
            var backupDir = new DirectoryInfo(backupPath);

            backupJob.Repository = new LocalRepository(backupDir);
            backupJob.Archiver = new BackupZipArchiver(new SplitStorage());

            var firstFile = new JobObject(@"C:\Users\misha\Documents\Test1\a.txt");
            var secondFile = new JobObject(@"C:\Users\misha\Documents\Test1\b.txt");

            var jobObjects = new List<JobObject>
            {
                firstFile,
                secondFile
            };

            foreach (JobObject job in jobObjects)
            {
                backupJob.AddJobObject(job);
            }

            backupJob.CreateRestorePoint();

            int dirs = Directory.GetDirectories(backupPath).Length;
            Assert.AreEqual(backupJob.RestorePoints.Count, dirs);

            backupJob.DeleteJobObject(firstFile);
            backupJob.CreateRestorePoint();

            dirs = Directory.GetDirectories(backupPath).Length;
            Assert.AreEqual(backupJob.RestorePoints.Count, dirs);

            int actualStoragesCount = Directory.GetFiles(backupPath, "*.*", SearchOption.AllDirectories).Length;
            int storagesCount = backupJob.RestorePoints.Sum(restorePoint => restorePoint.Storages.Count);

            Assert.AreEqual(actualStoragesCount, storagesCount);
        }

        [Test]
        [Ignore("No trash for computer")]
        public void CreateBackupJobAddTwoFiles_CheckThatFilesAndDirectoriesWereCreated()
        {
            var backupJob = new BackupJob();

            backupJob.Archiver = new BackupZipArchiver(new SingleStorage());

            const string backupDirPath = @"D:\Backups\Test2SingleStorage";
            var dirInfo = new DirectoryInfo(backupDirPath);
            backupJob.Repository = new LocalRepository(dirInfo);

            var firstFile = new JobObject(@"C:\Users\misha\Documents\Test2\c.txt");
            var secondFile = new JobObject(@"C:\Users\misha\Documents\Test2\d.txt");

            var jobObjects = new List<JobObject>
            {
                firstFile,
                secondFile
            };

            foreach (JobObject job in jobObjects)
            {
                backupJob.AddJobObject(job);
            }

            backupJob.CreateRestorePoint();

            Assert.IsTrue(dirInfo.Exists);

            int dirs = dirInfo.GetDirectories().Length;
            Assert.AreEqual(backupJob.RestorePoints.Count, dirs);
        }
    }
}