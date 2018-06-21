using Assets.Scripts.Abstract;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class CharacterHealth : Health
    {

        private Inventory.Inventory _inventory;

        protected override void Init()
        {
            _inventory = GetComponent<Inventory.Inventory>();
        }
        
        public override void TakeDamage(float amount)
        {
            if (_inventory != null)
                amount -= _inventory.Protection();
            base.TakeDamage(amount);
        }
        
        protected override void Die()
        {
            Debug.Log("Die " + gameObject);
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            if (_inventory != null)
                _inventory.DropAllConsumables();
            OnHealthChange();
        }

    }
}
