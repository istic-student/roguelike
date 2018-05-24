using System.Collections.Generic;
using Assets.Scripts.Interactive;

namespace Assets.Scripts.Character.Inventory
{
    public partial class Inventory
    {

        public List<Passive> Passives;

        private void InitPassives()
        {
            Passives = new List<Passive>();
        }

        public void AddPassive(Passive passive)
        {
            if (passive == null)
                return;
            Passives.Add(passive);
            OnInventoryChange();
        }

    }
}
