using System.Collections.Generic;
using Backups.Entities;

namespace Backups.PointSelectionAlgorithms
{
    public class LimitSelector : Selector
    {
        private readonly int _limit;

        public LimitSelector(int limit)
        {
            _limit = limit;
        }

        public override List<RestorePoint> SelectRestorePoints(BackupJob backupJob)
        {
            return backupJob.RestorePoints.GetRange(0, backupJob.RestorePoints.Count - _limit);
        }
    }
}