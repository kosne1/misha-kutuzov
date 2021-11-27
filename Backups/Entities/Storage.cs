using System;
using System.IO;

namespace Backups.Entities
{
    [Serializable]
    public class Storage
    {
        public Storage(FileInfo fileInfo)
        {
            FileInfo = fileInfo;
        }

        public FileInfo FileInfo { get; }
    }
}