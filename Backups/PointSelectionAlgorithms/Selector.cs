using System.Collections.Generic;
using Backups.Entities;

namespace Backups.PointSelectionAlgorithms
{
    public abstract class Selector
    {
        public abstract List<RestorePoint> SelectRestorePoints(BackupJob backupJob);
    }
}