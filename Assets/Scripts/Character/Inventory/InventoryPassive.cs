using System.Collections.Generic;
using Assets.Scripts.Interactive.Catchable.Abstract;

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
            passive.PassiveIsFinished += PassiveIsFinished;
            passive.Active();
            OnInventoryChange();
        }

        private void PassiveIsFinished(Passive passive)
        {
            if (passive == null)
                return;
            _passives.Remove(passive);
            Destroy(passive.gameObject);
            OnInventoryChange();
        }

    }
}
