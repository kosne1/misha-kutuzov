using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups.Entities;

namespace Backups.StorageAlgorithms
{
    public class SingleStorage : IStorageAlgorithm
    {
        public List<Storage> Store(List<JobObject> jobObjects, DirectoryInfo restorePointDir)
        {
            var storages = new List<Storage>();

            string archivePath = restorePointDir.FullName + @"\archive.zip";

            foreach (JobObject jobObject in jobObjects.Where(jobObject => File.Exists(jobObject.FilePath)))
            {
                using ZipArchive zipArchive = ZipFile.Open(archivePath, ZipArchiveMode.Update);

                string pathFileToAdd = $"{jobObject.FilePath}";

                string filename = Path.GetFileName(jobObject.FilePath);

                zipArchive.CreateEntryFromFile(pathFileToAdd, filename);

                var storage = new Storage(zipArchive);
                storages.Add(storage);
            }

            return storages;
        }
    }
}