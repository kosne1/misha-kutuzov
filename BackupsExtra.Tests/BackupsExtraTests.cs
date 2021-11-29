using System;
using System.Collections.Generic;
using System.IO;
using Backups.Archivers;
using Backups.ClearPointAlgorithms;
using Backups.Entities;
using Backups.Logger;
using Backups.PointSelectionAlgorithms;
using Backups.Repositories;
using Backups.StorageAlgorithms;
using BackupsExtra.Configurations;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class BackupsExtraTests
    {
        [Test]
        [Ignore("No trash for computer")]
        public void CreateJobCreateRestorePointSaveConfigurationLoadConfiguration_ConfigurationSavedAndLoaded()
        {
            var backupJob = new BackupJob(new ConsoleLogger());

            const string backupPath = @"C:\Users\quvi\Documents\Backup";
            var backupDir = new DirectoryInfo(backupPath);

            backupJob.SetRepository(new LocalRepository(backupDir));
            backupJob.SetArchiver(new BackupZipArchiver(new SplitStorage()));

            var jobObjects = new List<JobObject>
            {
                new(@"C:\Users\quvi\Documents\TestExtra\a.txt")
            };

            foreach (JobObject job in jobObjects)
            {
                backupJob.AddJobObject(job);
            }

            RestorePoint restorePoint = backupJob.CreateRestorePoint(DateTime.Now);

            var configurator = new XmlConfigurator();
            configurator.SaveConfiguration(backupJob);

            Assert.IsTrue(File.Exists(Path.Combine(backupPath, "config.xml")));

            BackupJob loadedBackupJob = configurator.LoadConfiguration(backupPath);
            Assert.Contains(restorePoint, backupJob.RestorePoints);
        }

        [Test]
        [Ignore("No trash for computer")]
        public void CreateJobCreateRestorePointsOutOfNumberLimit_DeletedExtra()
        {
            var backupJob = new BackupJob(new ConsoleLogger());

            const string backupPath = @"C:\Users\quvi\Documents\Backup";
            var backupDir = new DirectoryInfo(backupPath);

            backupJob.SetRepository(new LocalRepository(backupDir));
            backupJob.SetArchiver(new BackupZipArchiver(new SplitStorage()));
            backupJob.SetCleaner(new DeletePoints());
            backupJob.SetSelector(new LimitSelector(5));

            var jobObjects = new List<JobObject>
            {
                new(@"C:\Users\quvi\Documents\TestExtra\a.txt")
            };

            foreach (JobObject job in jobObjects)
            {
                backupJob.AddJobObject(job);
            }

            backupJob.CreateRestorePoint(DateTime.Now);
            backupJob.CreateRestorePoint(DateTime.Now);
            backupJob.CreateRestorePoint(DateTime.Now);
            backupJob.CreateRestorePoint(DateTime.Now);
            backupJob.CreateRestorePoint(DateTime.Now);
            backupJob.CreateRestorePoint(DateTime.Now);

            backupJob.ClearOldRestorePoints();
            Assert.AreEqual(5, backupJob.RestorePoints.Count);
        }

        [Test]
        [Ignore("No trash for computer")]
        public void CreateJobCreateRestorePointsOutOfDateLimit_DeletedExtra()
        {
            var backupJob = new BackupJob(new ConsoleLogger());

            const string backupPath = @"C:\Users\quvi\Documents\Backup";
            var backupDir = new DirectoryInfo(backupPath);

            backupJob.SetRepository(new LocalRepository(backupDir));
            backupJob.SetArchiver(new BackupZipArchiver(new SplitStorage()));
            backupJob.SetCleaner(new DeletePoints());
            backupJob.SetSelector(new DateSelector(DateTime.Now.AddYears(1)));

            var jobObjects = new List<JobObject>
            {
                new(@"C:\Users\quvi\Documents\TestExtra\a.txt")
            };

            foreach (JobObject job in jobObjects)
            {
                backupJob.AddJobObject(job);
            }

            backupJob.CreateRestorePoint(DateTime.Now);
            backupJob.CreateRestorePoint(DateTime.Now);
            backupJob.CreateRestorePoint(DateTime.Now.AddMonths(6));
            backupJob.CreateRestorePoint(DateTime.Now.AddDays(5));
            backupJob.CreateRestorePoint(DateTime.Now.AddYears(3));

            backupJob.ClearOldRestorePoints();
            Assert.AreEqual(4, backupJob.RestorePoints.Count);
        }

        [Test]
        [Ignore("No trash for computer")]
        public void CreateJobCreateRestorePointsOutOfDateLimit_Merged()
        {
            var backupJob = new BackupJob(new ConsoleLogger());

            const string backupPath = @"C:\Users\quvi\Documents\Backup";
            var backupDir = new DirectoryInfo(backupPath);

            backupJob.SetRepository(new LocalRepository(backupDir));
            backupJob.SetArchiver(new BackupZipArchiver(new SingleStorage()));
            backupJob.SetCleaner(new MergePoints());
            backupJob.SetSelector(new DateSelector(DateTime.Now.AddYears(1)));

            var jobObjects = new List<JobObject>
            {
                new(@"C:\Users\quvi\Documents\TestExtra\a.txt")
            };

            foreach (JobObject job in jobObjects)
            {
                backupJob.AddJobObject(job);
            }

            backupJob.CreateRestorePoint(DateTime.Now);
            backupJob.CreateRestorePoint(DateTime.Now);
            backupJob.CreateRestorePoint(DateTime.Now.AddMonths(6));
            backupJob.CreateRestorePoint(DateTime.Now.AddDays(5));
            backupJob.CreateRestorePoint(DateTime.Now.AddYears(3));

            backupJob.ClearOldRestorePoints();
            Assert.AreEqual(4, backupJob.RestorePoints.Count);
        }
    }
}