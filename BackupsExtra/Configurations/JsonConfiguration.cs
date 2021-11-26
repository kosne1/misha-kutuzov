using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Backups.Entities;

namespace BackupsExtra.Configurations
{
    public class JsonConfiguration : IConfiguration
    {
        public async void SaveConfiguration(BackupJob backupJob)
        {
            await using var fs = new FileStream(
                Path.Combine(backupJob.Repository.DirectoryInfo.Name, "config.json"),
                FileMode.OpenOrCreate);
            await JsonSerializer.SerializeAsync<BackupJob>(fs, backupJob);
        }

        public async Task<BackupJob> LoadConfiguration(string backupPath)
        {
            await using var fs = new FileStream(Path.Combine(backupPath, "config.json"), FileMode.OpenOrCreate);
            BackupJob restoredPerson = await JsonSerializer.DeserializeAsync<BackupJob>(fs);
            return restoredPerson;
        }
    }
}