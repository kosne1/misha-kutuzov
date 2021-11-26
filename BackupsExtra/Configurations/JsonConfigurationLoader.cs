using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Backups.Entities;

namespace BackupsExtra.Configurations
{
    public class JsonConfigurationLoader : IConfigurationLoader
    {
        public async Task<BackupJob> LoadConfiguration(string backupPath)
        {
            await using var fs = new FileStream(Path.Combine(backupPath, "config.json"), FileMode.OpenOrCreate);
            BackupJob restoredPerson = await JsonSerializer.DeserializeAsync<BackupJob>(fs);
            return restoredPerson;
        }
    }
}