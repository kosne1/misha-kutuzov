using System.IO.Compression;
using Backups.Entities;

namespace BackupsExtra.RestoreMethods
{
    public class ZipOriginalLocationRestore : IRestore
    {
        public void Restore(RestorePoint restorePoint, string path)
        {
            foreach (Storage storage in restorePoint.Storages)
            {
                ZipFile.ExtractToDirectory(storage.FileInfo.FullName, path, true);
            }
        }
    }
}