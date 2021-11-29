using System.Threading.Tasks;
using Backups.Entities;

namespace BackupsExtra.Configurations
{
    public interface IConfigurator
    {
        public BackupJob LoadConfiguration(string backupPath);
        public void SaveConfiguration(BackupJob backupJob);
    }
}