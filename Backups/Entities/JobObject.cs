using System.IO;

namespace Backups.Entities
{
    public class JobObject
    {
        public JobObject(string path)
        {
            FilePath = new FileInfo(path);
        }

        public FileInfo FilePath { get; }
    }
}