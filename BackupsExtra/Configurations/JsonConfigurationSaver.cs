using System.IO;
using System.Text.Json;
using Backups.Entities;

namespace BackupsExtra.Configurations
{
    public class JsonConfigurationSaver : IConfigurationSaver
    {
        public async void SaveConfiguration(BackupJob backupJob)
        {
            await using var fs = new FileStream(
                Path.Combine(backupJob.Repository.DirectoryInfo.Name, "config.json"),
                FileMode.OpenOrCreate);
            await JsonSerializer.SerializeAsync<BackupJob>(fs, backupJob);
        }
    }
}