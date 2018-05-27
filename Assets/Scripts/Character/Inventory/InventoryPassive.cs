using System.Collections.Generic;
using Assets.Scripts.Interactive;

namespace Assets.Scripts.Character.Inventory
{
    public partial class Inventory
    {

        private List<Passive> _passives;
        
        /// <summary>
        /// Init passives (on Start)
        /// </summary>
        private void InitPassives()
        {
            _passives = new List<Passive>();
        }

        /// <summary>
        /// Add passive
        /// </summary>
        /// <param name="passive">Passive to add</param>
        public void AddPassive(Passive passive)
        {
            if (passive == null)
                return;
            _passives.Add(passive);
            OnInventoryChange();
        }

    }
}
