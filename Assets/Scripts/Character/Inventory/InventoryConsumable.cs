using System.Collections.Generic;
using Assets.Scripts.Interactive.Abstract;
using Assets.Scripts.Interactive.Catchable;
using UnityEngine;

namespace Assets.Scripts.Character.Inventory
{
    public partial class Inventory
    {

        public IEnumerable<Consumable> Consumables
        {
            get { return _consumables; }
        }

        private List<Consumable> _consumables;

        /// <summary>
        /// Init consumables (on Start)
        /// </summary>
        private void InitConsumable()
        {
            _consumables = new List<Consumable>();
        }

        /// <summary>
        /// Add a consumable to the inventory
        /// </summary>
        /// <param name="consumable">consumable to add</param>
        public void AddConsumable(Consumable consumable)
        {
            if (consumable == null)
                return;
            _consumables.Add(consumable);
            OnInventoryChange();
        }

        /// <summary>
        /// drop one consumable
        /// </summary>
        /// <param name="consumable">consumable to drop</param>
        public void DropConsumable(Consumable consumable)
        {
            if (consumable == null)
                return;
            Drop(consumable);
            _consumables.Remove(consumable);
            OnInventoryChange();
        }

        /// <summary>
        /// Destroy one consumable
        /// </summary>
        /// <param name="consumable">consumable to destroy</param>
        public void DestroyConsumable(Consumable consumable)
        {
            if (consumable == null)
                return;
            _consumables.Remove(consumable);
            Destroy(consumable.gameObject);
            OnInventoryChange();
        }

        /// <summary>
        /// Drop all consumables
        /// </summary>
        public void DropAllConsumables()
        {
            foreach (var consumable in _consumables)
            {
                DropConsumable(consumable);
            }
        }

        /// <summary>
        /// Try to use a consumable on an activable, if it work, the consumable is removed from the inventory
        /// </summary>
        /// <param name="objectToActive">object to active</param>
        public void TryToUse(Activable objectToActive)
        {
            for (var i = _consumables.Count - 1; i >= 0; i--)
            {
                var consumable = _consumables[i];
                if (!objectToActive.Active(consumable))
                    continue;
                DestroyConsumable(consumable);
                if (objectToActive.Actived)
                    return;
            }
        }

    }
}
