using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups.Entities;

namespace Backups.StorageAlgorithms
{
    public class SingleStorage : IStorageAlgorithm
    {
        public List<Storage> Store(List<JobObject> jobObjects, DirectoryInfo directoryInfo)
        {
            var storages = new List<Storage>();

            string archivePath = Path.Join(directoryInfo.FullName, @"\archive.zip");

            foreach (JobObject jobObject in jobObjects.Where(jobObject => jobObject.FilePath.Exists))
            {
                using ZipArchive zipArchive = ZipFile.Open(archivePath, ZipArchiveMode.Update);

                string pathFileToAdd = jobObject.FilePath.FullName;

                string filename = jobObject.FilePath.Name;

                zipArchive.CreateEntryFromFile(pathFileToAdd, filename);
            }

            var storage = new Storage(new FileInfo(archivePath));
            storages.Add(storage);

            return storages;
        }
    }
}