namespace Backups.Entities
{
    public class JobObject
    {
        public JobObject(string filePath)
        {
            FilePath = filePath;
        }

        public string FilePath { get; }
    }
}