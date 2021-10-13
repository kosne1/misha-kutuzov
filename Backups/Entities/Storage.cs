using System.IO.Compression;

namespace Backups.Entities
{
    public class Storage
    {
        private ZipArchive _zipArchive;
        public Storage(ZipArchive zipArchive)
        {
            _zipArchive = zipArchive;
        }
    }
}