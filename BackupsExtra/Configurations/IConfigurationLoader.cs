using System.Threading.Tasks;
using Backups.Entities;

namespace BackupsExtra.Configurations
{
    public interface IConfigurationLoader
    {
        public Task<BackupJob> LoadConfiguration(string backupPath);
    }
}