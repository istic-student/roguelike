using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Interactive.Catchable.Abstract;

namespace Assets.Scripts.Character.Inventory
{
    public partial class Inventory
    {

        public Equipment Weapon
        {
            get { return _equipment[EquipmentEnum.Weapon]; }
        }

        private IDictionary<EquipmentEnum, Equipment> _equipment;

        /// <summary>
        /// Init equipment (on Start)
        /// </summary>
        private void InitEquipment()
        {
            _equipment = new Dictionary<EquipmentEnum, Equipment>();
            foreach (EquipmentEnum equipment in Enum.GetValues(typeof(EquipmentEnum)))
            {
                _equipment[equipment] = null;
            }
        }

        /// <summary>
        /// Add a equipment to the inventory
        /// </summary>
        /// <param name="equipment">equipment to add</param>
        public void AddEquipment(Equipment equipment)
        {
            if (equipment == null)
                return;
            if (_equipment[equipment.Type] != null)
                DropEquipment(_equipment[equipment.Type]);
            _equipment[equipment.Type] = equipment;
            OnInventoryChange();
        }

        /// <summary>
        /// drop one equipment
        /// </summary>
        /// <param name="equipment">equipment to drop</param>
        public void DropEquipment(Equipment equipment)
        {
            if (equipment == null)
                return;
            Drop(equipment);
            _equipment[equipment.Type] = null;
            OnInventoryChange();
        }

        /// <summary>
        /// calculates the sum of the protection of the equipment
        /// </summary>
        /// <returns>total of the protection</returns>
        public float Protection()
        {
            return _equipment.Where(equipment => equipment.Value != null).Sum(equipment => equipment.Value.DamageAbsorption);
        }

    }
}
