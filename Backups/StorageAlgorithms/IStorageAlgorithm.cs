using System.Collections.Generic;
using System.IO;
using Backups.Entities;

namespace Backups.StorageAlgorithms
{
    public interface IStorageAlgorithm
    {
        public List<Storage> Store(List<JobObject> jobObjects, DirectoryInfo restorePointDir);
    }
}