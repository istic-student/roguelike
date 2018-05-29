using Assets.Scripts.Interactive.Abstract;
using Assets.Scripts.Interactive.Catchable.Abstract;
using Assets.Scripts.Manager;
using UnityEngine;

namespace Assets.Scripts.Character.Inventory
{
    public partial class Inventory : MonoBehaviour
    {

        public Transform InventoryTransform;

        public delegate void InventoryChangeHandler();
        public event InventoryChangeHandler InventoryChange;

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
            item.transform.parent = GameManager.AssetsManager.InteractiveTransform;
        }

        private void OnInventoryChange()
        {
            if (InventoryChange != null)
                InventoryChange();
        }

    }
}
