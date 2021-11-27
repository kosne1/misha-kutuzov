using System.IO;

namespace Backups.Entities
{
    public class Storage
    {
        public Storage(FileInfo fileInfo)
        {
            FileInfo = fileInfo;
        }

        public FileInfo FileInfo { get; }
    }
}