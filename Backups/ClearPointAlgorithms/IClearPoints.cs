using System.Collections.Generic;
using Backups.Entities;

namespace Backups.ClearPointAlgorithms
{
    public interface IClearPoints
    {
        public void ClearPoints(List<RestorePoint> allRestorePoints, List<RestorePoint> selectedRestorePoints);
    }
}