using System.Collections.Generic;
using Backups.Entities;

namespace Backups.ClearPointAlgorithms
{
    public interface ICleaner
    {
        public void ClearPoints(List<RestorePoint> allRestorePoints, List<RestorePoint> selectedRestorePoints);
    }
}