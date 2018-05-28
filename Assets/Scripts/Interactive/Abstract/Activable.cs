using System.Collections.Generic;
using Assets.Scripts.Interactive.Catchable;
using UnityEngine;

namespace Assets.Scripts.Interactive.Abstract
{
    public abstract class Activable : Interactive
    {

        public List<Consumable> Unlockers;
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
            if (Actived || !Unlockers.Contains(consumable)) return false;
            ActiveToUnlock();
            return true;
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
