using System;

namespace Backups.Entities
{
    [Serializable]
    public class JobObject
    {
        public JobObject(string filePath)
        {
            FilePath = filePath;
        }

        public string FilePath { get; }
    }
}