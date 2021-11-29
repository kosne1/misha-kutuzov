using System;
using System.Collections.Generic;
using Backups.StorageAlgorithms;

namespace Backups.Entities
{
    public class RestorePoint
    {
        public RestorePoint(DateTime creationTime, IStorageAlgorithm storageAlgorithm)
        {
            Storages = new List<Storage>();
            CreationTime = creationTime;
            StorageAlgorithm = storageAlgorithm;
        }

        public IStorageAlgorithm StorageAlgorithm { get; }
        public List<Storage> Storages { get; set; }
        public DateTime CreationTime { get; }

        public void AddStorage(Storage storage)
        {
            Storages.Add(storage);
        }
    }
}