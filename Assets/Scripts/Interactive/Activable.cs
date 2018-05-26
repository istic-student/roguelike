using System.Collections.Generic;

namespace Assets.Scripts.Interactive
{
    public class Activable : Abstract.Interactive
    {

        public List<Consumable> Unlockers;
        public bool Actived;

        public bool Active()
        {
            if (Unlockers != null) return false;
            Unlock();
            return true;
        }

        public bool Active(Consumable consumable)
        {
            if (!Unlockers.Contains(consumable)) return false;
            Unlock();
            return true;
        }

        private void Unlock()
        {
            if (Actived) return;;
            Actived = true;
        }

    }
}
