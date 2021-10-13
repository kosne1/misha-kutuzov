using System.Collections.Generic;
using Backups.Entities;

namespace Backups.StorageAlgorithms
{
    public interface IStorageAlgorithm
    {
        public List<Storage> Store(List<JobObject> jobObjects, string restorePointDir);
    }
}