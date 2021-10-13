namespace Backups.Entities
{
    public class JobObject
    {
        public JobObject(string filePath, string fileName)
        {
            FilePath = filePath;
            FileName = fileName;
        }

        public string FilePath { get; }
        public string FileName { get; }
    }
}