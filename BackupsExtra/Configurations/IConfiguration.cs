using System.Threading.Tasks;
using Backups.Entities;

namespace BackupsExtra.Configurations
{
    public interface IConfiguration
    {
        public void SaveConfiguration(BackupJob backupJob);
        public Task<BackupJob> LoadConfiguration(string backupPath);
    }
}