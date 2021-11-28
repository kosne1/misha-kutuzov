using System.Collections.Generic;
using Backups.Entities;

namespace Backups.PointSelectionAlgorithms
{
    public abstract class DecoratorSelector : Selector
    {
        public DecoratorSelector(Selector selector)
        {
            Selector = selector;
        }

        public Selector Selector { get; private set; }

        public void SetSelector(Selector selector)
        {
            Selector = selector;
        }

        public override List<RestorePoint> SelectRestorePoints(BackupJob backupJob)
        {
            if (Selector != null)
            {
                return Selector.SelectRestorePoints(backupJob);
            }

            return null;
        }
    }
}