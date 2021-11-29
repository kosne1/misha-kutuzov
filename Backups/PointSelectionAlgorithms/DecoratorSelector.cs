using System.Collections.Generic;
using Backups.Entities;

namespace Backups.PointSelectionAlgorithms
{
    public abstract class DecoratorSelector : ISelector
    {
        public DecoratorSelector(ISelector selector)
        {
            Selector = selector;
        }

        public ISelector Selector { get; private set; }

        public void SetSelector(ISelector selector)
        {
            Selector = selector;
        }

        public virtual List<RestorePoint> SelectRestorePoints(List<RestorePoint> restorePoints)
        {
            return Selector?.SelectRestorePoints(restorePoints);
        }
    }
}