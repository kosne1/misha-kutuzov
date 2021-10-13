using System.Collections.Generic;
using Backups.Entities;

namespace Backups.StorageAlgorithms
{
    public class SingleStorage : IStorageAlgorithm
    {
        public List<Storage> Store(List<JobObject> jobObjects, string restorePointDir)
        {
            throw new System.NotImplementedException();
        }
    }
}