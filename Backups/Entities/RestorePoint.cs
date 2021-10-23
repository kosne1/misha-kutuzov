using System.Collections.Generic;

namespace Backups.Entities
{
    public class RestorePoint
    {
        private readonly List<Storage> _storages;

        public RestorePoint()
        {
            _storages = new List<Storage>();
        }

        public IReadOnlyCollection<Storage> Storages => _storages;

        public void AddStorage(Storage storage)
        {
            _storages.Add(storage);
        }
    }
}