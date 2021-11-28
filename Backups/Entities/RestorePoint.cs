using System;
using System.Collections.Generic;

namespace Backups.Entities
{
    public class RestorePoint
    {
        private readonly List<Storage> _storages;

        public RestorePoint(DateTime creationTime)
        {
            _storages = new List<Storage>();
            CreationTime = creationTime;
        }

        public IReadOnlyCollection<Storage> Storages => _storages;
        public DateTime CreationTime { get; }

        public void AddStorage(Storage storage)
        {
            _storages.Add(storage);
        }
    }
}