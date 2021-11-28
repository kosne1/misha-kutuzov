using System.Collections.Generic;
using System.Linq;
using Backups.Entities;
using Backups.StorageAlgorithms;

namespace Backups.ClearPointAlgorithms
{
    public class MergePoints : IClearPoints
    {
        public void ClearPoints(List<RestorePoint> allRestorePoints, List<RestorePoint> selectedRestorePoints)
        {
            RestorePoint latestPoint = allRestorePoints.Last();
            foreach (RestorePoint restorePoint in selectedRestorePoints)
            {
                if (restorePoint.StorageAlgorithm is SingleStorage)
                {
                    selectedRestorePoints.Remove(restorePoint);
                    continue;
                }

                foreach (Storage storage in restorePoint.Storages)
                {
                    Storage foundStorage = latestPoint.Storages.Find(s => s.FileInfo.Name == storage.FileInfo.Name);
                    if (foundStorage == null)
                        latestPoint.AddStorage(storage);

                    restorePoint.Storages.Remove(storage);
                }
            }
        }
    }
}