using System.Collections.Generic;
using Backups.Entities;

namespace Backups.PointSelectionAlgorithms
{
    public interface ISelector
    {
        public List<RestorePoint> SelectRestorePoints(List<RestorePoint> restorePoints);
    }
}