using System;
using System.Collections.Generic;
using Backups.Entities;

namespace Backups.PointSelectionAlgorithms
{
    public class DateSelector : Selector
    {
        private readonly DateTime _dateLimit;

        public DateSelector(DateTime dateLimit)
        {
            _dateLimit = dateLimit;
        }

        public override List<RestorePoint> SelectRestorePoints(List<RestorePoint> restorePoints)
        {
            var selectedPoints = new List<RestorePoint>();
            foreach (RestorePoint restorePoint in restorePoints)
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