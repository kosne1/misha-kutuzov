using System;
using System.Collections.Generic;
using Backups.Entities;

namespace Backups.PointSelectionAlgorithms
{
    public class DateSelector
    {
        private readonly DateTime _dateLimit;

        public DateSelector(DateTime dateLimit)
        {
            _dateLimit = dateLimit;
        }

        public List<RestorePoint> SelectRestorePoints(BackupJob backupJob)
        {
            var selectedPoints = new List<RestorePoint>();
            foreach (RestorePoint restorePoint in backupJob.RestorePoints)
            {
                TimeSpan diff = restorePoint.CreationTime - _dateLimit;
                if (diff.Seconds > 0)
                {
                    selectedPoints.Add(restorePoint);
                }
            }

            return selectedPoints;
        }
    }
}