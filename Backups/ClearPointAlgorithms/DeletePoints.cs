using System.Collections.Generic;
using Backups.Entities;

namespace Backups.ClearPointAlgorithms
{
    public class DeletePoints : IClearPoints
    {
        public void ClearPoints(List<RestorePoint> allRestorePoints, List<RestorePoint> selectedRestorePoints)
        {
            foreach (RestorePoint restorePoint in selectedRestorePoints)
            {
                allRestorePoints.Remove(restorePoint);
            }
        }
    }
}