using System.Collections.Generic;
using System.IO;
using Backups.Archivers;
using Backups.Entities;
using Backups.Repositories;
using Backups.StorageAlgorithms;
using BackupsExtra.Configurations;
using BackupsExtra.Entities;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class BackupsExtraTests
    {
        [Test]
        public void CreateBackupSystemCreateRestorePointSaveConfiguration_ConfigurationSaved()
        {
            var backupSystem = new BackupSystem();
            BackupJob backupJob = backupSystem.AddBackupJob();

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

            backupJob.CreateRestorePoint();

            backupSystem.SetConfigurator(new XmlConfigurator());
            backupSystem.SaveConfigurations();

            Assert.IsTrue(File.Exists(Path.Combine(backupPath, "config.xml")));
        }
    }
}