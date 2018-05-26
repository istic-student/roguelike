using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Interactive;

namespace Assets.Scripts.Character.Inventory
{
    public partial class Inventory
    {

        public Equipment Weapon
        {
            get { return _equipment[EquipmentEnum.Weapon]; }
        }

        private IDictionary<EquipmentEnum, Equipment> _equipment;

        private void InitEquipment()
        {
            _equipment = new Dictionary<EquipmentEnum, Equipment>();
            foreach (EquipmentEnum equipment in Enum.GetValues(typeof(EquipmentEnum)))
            {
                _equipment[equipment] = null;
            }
        }

        public void AddEquipment(Equipment equipment)
        {
            if (equipment == null)
                return;
            if (_equipment[equipment.Type] != null)
                DropEquipment(_equipment[equipment.Type]);
            _equipment[equipment.Type] = equipment;
            OnInventoryChange();
        }

        public void DropEquipment(Equipment equipment)
        {
            if (equipment == null)
                return;
            _equipment[equipment.Type] = null;
            OnInventoryChange();
        }

        public float Attack()
        {
            return _equipment.Where(equipment => equipment.Value != null).Sum(equipment => equipment.Value.Attack);
        }

        public float Protection()
        {
            return _equipment.Where(equipment => equipment.Value != null).Sum(equipment => equipment.Value.DamageAbsorption);
        }

    }
}
