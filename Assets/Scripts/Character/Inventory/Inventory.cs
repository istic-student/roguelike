using Assets.Scripts.Interactive;
using Assets.Scripts.Interactive.Abstract;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Character.Inventory
{
    public partial class Inventory : MonoBehaviour
    {

        public Transform InventoryTransform;

        public IEnumerable<Consumable> Consumables
        {
            get { return _consumables; }
        }

        public delegate void InventoryChangeHandler();
        public event InventoryChangeHandler InventoryChange;

        private List<Consumable> _consumables;

        public Inventory()
        {
            InitConsumable();
            InitEquipment();
            InitPassives();
        }

        private void Start()
        {
            foreach (var childCatchable in InventoryTransform.GetComponentsInChildren<Catchable>())
            {
                Add(childCatchable);
            }
        }

        /// <summary>
        /// Init consumables (on Start)
        /// </summary>
        private void InitConsumable()
        {
            _consumables = new List<Consumable>();
        }

        /// <summary>
        /// Add catchable to the inventory
        /// </summary>
        /// <param name="item">catchable to add</param>
        public void Add(Catchable item)
        {
            if (item == null)
                return;
            item.transform.parent = InventoryTransform;
            item.transform.localPosition = new Vector3();
            AddConsumable(item as Consumable);
            AddPassive(item as Passive);
            AddEquipment(item as Equipment);
        }
        /// <summary>
        /// drop catchable
        /// </summary>
        /// <param name="item">catchable to drop</param>
        public void Drop(Catchable item)
        {
            if (item == null)
                return;
            item.transform.parent = InventoryTransform;
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
            foreach (var consumable in _consumables)
            {
                if (!objectToActive.Active(consumable))
                    continue;
                DestroyConsumable(consumable);
                return;
            }
        }

        private void OnInventoryChange()
        {
            if (InventoryChange != null)
                InventoryChange();
        }

    }
}
