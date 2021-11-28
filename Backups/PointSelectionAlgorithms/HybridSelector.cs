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

        public override List<RestorePoint> SelectRestorePoints(List<RestorePoint> restorePoints)
        {
            List<RestorePoint> points = Selector.SelectRestorePoints(restorePoints);

            List<RestorePoint> anotherPoints = base.SelectRestorePoints(restorePoints);

            return points.Count < anotherPoints.Count ? points : anotherPoints;
        }
    }
}