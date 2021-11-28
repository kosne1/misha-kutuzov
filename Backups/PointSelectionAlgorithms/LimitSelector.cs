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

        public override List<RestorePoint> SelectRestorePoints(List<RestorePoint> restorePoints)
        {
            return restorePoints.GetRange(0, restorePoints.Count - _limit);
        }
    }
}