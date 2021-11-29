using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Backups.Entities;

namespace BackupsExtra.Configurations
{
    public class XmlConfigurator : IConfigurator
    {
        public void SaveConfiguration(BackupJob backupJob)
        {
            if (backupJob == null)
            {
                return;
            }

            var xmlDocument = new XmlDocument();
            var serializer = new XmlSerializer(backupJob.GetType());
            using var stream = new MemoryStream();
            serializer.Serialize(stream, backupJob);
            stream.Position = 0;
            xmlDocument.Load(stream);
            xmlDocument.Save(Path.Combine(backupJob.Repository.DirectoryInfo.FullName, "config.xml"));
        }

        public BackupJob LoadConfiguration(string backupPath)
        {
            string fileName = Path.Combine(backupPath, "config.xml");
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);
            string xmlString = xmlDocument.OuterXml;

            using var read = new StringReader(xmlString);
            Type outType = typeof(BackupJob);

            var serializer = new XmlSerializer(outType);
            using XmlReader reader = new XmlTextReader(read);
            var objectOut = (BackupJob)serializer.Deserialize(reader);

            return objectOut;
        }
    }
}