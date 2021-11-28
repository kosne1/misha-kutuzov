using System.IO;

namespace Backups.Logger
{
    public class FileLogger
    {
        private readonly FileInfo _fileInfo;

        public FileLogger(string path)
        {
            _fileInfo = new FileInfo(path);
            if (!_fileInfo.Exists)
                _fileInfo.Create();
        }
    }
}