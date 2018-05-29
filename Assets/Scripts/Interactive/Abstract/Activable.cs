using System.Collections.Generic;
using Assets.Scripts.Interactive.Catchable.Abstract;

namespace Assets.Scripts.Interactive.Abstract
{
    public abstract class Activable : Interactive
    {

        public List<Consumable> Unlockers;
        public bool MustHaveAllUnlockers;
        public bool Actived;

        /// <summary>
        /// Active
        /// </summary>
        /// <returns>Activation has worked</returns>
        public virtual bool Active()
        {
            if (Actived || Unlockers != null && Unlockers.Count > 0) return false;
            ActiveToUnlock();
            return true;
        }

        /// <summary>
        /// Active with consumable
        /// </summary>
        /// <param name="consumable">Used consumable</param>
        /// <returns>activation has worked</returns>
        public virtual bool Active(Consumable consumable)
        {
            if (Actived || !Contains(consumable)) return false;
            if (!MustHaveAllUnlockers)
                ActiveToUnlock();
            Unlockers.RemoveAt(GetIndex(consumable));
            if (MustHaveAllUnlockers && Unlockers.Count <= 0)
                ActiveToUnlock();
            return true;
        }

        /// <summary>
        /// Get index of consumable in unlockers
        /// </summary>
        /// <param name="consumable">consumable</param>
        /// <returns>Index of consumable in unlockers</returns>
        private int GetIndex(Consumable consumable)
        {
            for (var i = 0; i < Unlockers.Count; i++)
            {
                var unlocker = Unlockers[i];
                if (unlocker.TypeAndValueEquals(consumable))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// check if unlockers contains consumable
        /// </summary>
        /// <param name="consumable">consumable</param>
        /// <returns>Unlockers contains consumable</returns>
        private bool Contains(Consumable consumable)
        {
            return GetIndex(consumable) >= 0;
        }

        /// <summary>
        /// Set Activated to true and unlock
        /// </summary>
        private void ActiveToUnlock()
        {
            if (Actived) return;
            Actived = true;
            Unlock();
        }

        protected abstract void Unlock();

    }
}
