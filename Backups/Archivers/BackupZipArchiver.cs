using System.Collections.Generic;
using System.IO;
using Backups.Entities;
using Backups.StorageAlgorithms;

namespace Backups.Archivers
{
    public class BackupZipArchiver : IArchiver
    {
        public BackupZipArchiver(IStorageAlgorithm storageAlgorithm)
        {
            StorageAlgorithm = storageAlgorithm;
        }

        public IStorageAlgorithm StorageAlgorithm { get; }

        public List<Storage> Archive(List<JobObject> jobObjects, DirectoryInfo directoryInfo)
        {
            return StorageAlgorithm.Store(jobObjects, directoryInfo);
        }
    }
}