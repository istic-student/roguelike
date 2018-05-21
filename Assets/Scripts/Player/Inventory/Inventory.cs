using System.Collections.Generic;
using Assets.Scripts.Interactive;
using Assets.Scripts.Interactive.Abstract;
using UnityEngine;

namespace Assets.Scripts.Player.Inventory
{
    public partial class Inventory : MonoBehaviour
    {

        public List<Consumable> Consumables;

        private PlayerController _playerController;

        private void Start()
        {
            _playerController = GetComponent<PlayerController>();
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
            _playerController.Notify();
        }

        public void DropConsumable(Consumable consumable)
        {
            if (consumable == null)
                return;
            Consumables.Remove(consumable);
            _playerController.Notify();
        }

        public void DestroyConsumable(Consumable consumable)
        {
            if (consumable == null)
                return;
            Consumables.Remove(consumable);
            Destroy(consumable.gameObject);
            _playerController.Notify();
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

    }
}
