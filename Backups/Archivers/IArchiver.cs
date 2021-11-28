using System.Collections.Generic;
using System.IO;
using Backups.Entities;
using Backups.StorageAlgorithms;

namespace Backups.Archivers
{
    public interface IArchiver
    {
        public IStorageAlgorithm StorageAlgorithm { get; }
        public List<Storage> Archive(List<JobObject> jobObjects, DirectoryInfo directoryInfo);
    }
}