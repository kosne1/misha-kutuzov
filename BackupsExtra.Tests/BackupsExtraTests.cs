using System;
using System.Collections.Generic;
using System.IO;
using Backups.Archivers;
using Backups.Entities;
using Backups.Logger;
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
        public void CreateBackupSystemCreateRestorePointSaveConfiguration_ConfigurationSaved()
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

            backupJob.CreateRestorePoint(DateTime.Now);

            var configurator = new XmlConfigurator();
            configurator.SaveConfiguration(backupJob);

            Assert.IsTrue(File.Exists(Path.Combine(backupPath, "config.xml")));
        }
    }
}