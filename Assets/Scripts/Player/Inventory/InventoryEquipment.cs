using System;
using System.Collections.Generic;
using Assets.Scripts.Interactive;

namespace Assets.Scripts.Player.Inventory
{
    public partial class Inventory
    {

        private IDictionary<EquipmentEnum, Equipment> _equipment;

        private void InitEquipment()
        {
            _equipment = new Dictionary<EquipmentEnum, Equipment>();
            foreach (EquipmentEnum equipment in Enum.GetValues(typeof(EquipmentEnum)))
            {
                _equipment[equipment] = null;
            }
        }

        public void DropEquipment(Equipment equipment)
        {
            _equipment[equipment.Type] = null;
            _playerController.Notify();
        }

    }
}
