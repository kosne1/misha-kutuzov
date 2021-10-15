using System.Collections.Generic;

namespace Backups.Entities
{
    public class Backup
    {
        private readonly List<RestorePoint> _restorePoints;

        public Backup()
        {
            _restorePoints = new List<RestorePoint>();
        }

        public IReadOnlyCollection<RestorePoint> RestorePoints => _restorePoints;

        public void AddRestorePoint(RestorePoint restorePoint)
        {
            _restorePoints.Add(restorePoint);
        }
    }
}