using System.Collections.Generic;
using System.IO;
using Backups.Entities;

namespace Backups.Archivers
{
    public interface IArchiver
    {
        public List<Storage> Archive(List<JobObject> jobObjects, DirectoryInfo directoryInfo);
    }
}