using Backups.Entities;

namespace BackupsExtra.Configurations
{
    public interface IConfigurationSaver
    {
        public void SaveConfiguration(BackupJob backupJob);
    }
}