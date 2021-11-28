using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Archivers;
using Backups.Entities;
using Backups.Logger;
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
            var backupJob = new BackupJob(new ConsoleLogger());

            const string backupPath = @"D:\Backups\Test1SplitStorage";
            var backupDir = new DirectoryInfo(backupPath);

            backupJob.SetRepository(new LocalRepository(backupDir));
            backupJob.SetArchiver(new BackupZipArchiver(new SplitStorage()));

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

            backupJob.CreateRestorePoint(DateTime.Now);

            int dirs = Directory.GetDirectories(backupPath).Length;
            Assert.AreEqual(backupJob.RestorePoints.Count, dirs);

            backupJob.DeleteJobObject(firstFile);
            backupJob.CreateRestorePoint(DateTime.Now);

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
            var backupJob = new BackupJob(new ConsoleLogger());

            const string backupPath = @"D:\Backups\Test2SingleStorage";
            var backupDir = new DirectoryInfo(backupPath);
            backupJob.SetRepository(new LocalRepository(backupDir));
            backupJob.SetArchiver(new BackupZipArchiver(new SplitStorage()));

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

            backupJob.CreateRestorePoint(DateTime.Now);

            Assert.IsTrue(backupDir.Exists);

            int dirs = backupDir.GetDirectories().Length;
            Assert.AreEqual(backupJob.RestorePoints.Count, dirs);
        }
    }
}