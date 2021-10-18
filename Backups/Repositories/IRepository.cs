using System.Collections.Generic;
using System.IO;
using Backups.Entities;

namespace Backups.Repositories
{
    public interface IRepository
    {
        public DirectoryInfo DirectoryInfo { get; }
        public void SaveStorages(List<Storage> storages, int restorePointNumber);
    }
}