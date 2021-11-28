using System.Collections.Generic;
using Backups.Entities;

namespace Backups.PointSelectionAlgorithms
{
    public class HybridSelector : DecoratorSelector
    {
        public HybridSelector(Selector selector)
            : base(selector)
        {
        }

        public override List<RestorePoint> SelectRestorePoints(BackupJob backupJob)
        {
            List<RestorePoint> points = _selector.SelectRestorePoints(backupJob);

            List<RestorePoint> anotherPoints = base.SelectRestorePoints(backupJob);

            return points.Count < anotherPoints.Count ? points : anotherPoints;
        }
    }
}