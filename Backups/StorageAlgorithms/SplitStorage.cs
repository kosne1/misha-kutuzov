using System.Collections.Generic;
using System.IO.Compression;
using Backups.Entities;

namespace Backups.StorageAlgorithms
{
    public class SplitStorage : IStorageAlgorithm
    {
        public List<Storage> Store(List<JobObject> jobObjects, string restorePointDir)
        {
            var storages = new List<Storage>();

            foreach (JobObject jobObject in jobObjects)
            {
                string archivePath = restorePointDir + $@"\{jobObject.FileName}";

                using ZipArchive zipArchive = ZipFile.Open(archivePath, ZipArchiveMode.Create);

                string pathFileToAdd = $"{jobObject.FilePath}";

                string nameFileToAdd = jobObject.FileName;

                zipArchive.CreateEntryFromFile(pathFileToAdd, nameFileToAdd);

                var storage = new Storage(zipArchive);
                storages.Add(storage);
            }

            return storages;
        }
    }
}