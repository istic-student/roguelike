using System.Collections.Generic;
using Assets.Scripts.Interactive;
using Assets.Scripts.Interactive.Abstract;
using UnityEngine;

namespace Assets.Scripts.Character.Inventory
{
    public partial class Inventory : MonoBehaviour
    {

        public List<Consumable> Consumables;

        public delegate void InventoryChangeHandler();
        public event InventoryChangeHandler InventoryChange;

        private void Start()
        {
            InitEquipment();
            InitPassives();
        }

        public void Add(Catchable item)
        {
            if (item == null)
                return;
            AddConsumable(item as Consumable);
            AddPassive(item as Passive);
            AddEquipment(item as Equipment);
        }

        public void AddConsumable(Consumable consumable)
        {
            if (consumable == null)
                return;
            Consumables.Add(consumable);
            OnInventoryChange();
        }

        public void DropConsumable(Consumable consumable)
        {
            if (consumable == null)
                return;
            Consumables.Remove(consumable);
            OnInventoryChange();
        }

        public void DestroyConsumable(Consumable consumable)
        {
            if (consumable == null)
                return;
            Consumables.Remove(consumable);
            Destroy(consumable.gameObject);
            OnInventoryChange();
        }

        public void DropAllConsumables()
        {
            foreach (var consumable in Consumables)
            {
                DropConsumable(consumable);
            }
        }

        public void TryToUse(Activable objectToActive)
        {
            foreach (var consumable in Consumables)
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
