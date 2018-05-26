using UnityEngine;

namespace Assets.Scripts.Character
{
    public class Health : MonoBehaviour
    {

        public float StartingHealth = 100f;
        public float CurrentHealth { get; private set; }

        private Inventory.Inventory _inventory;

        public delegate void HealthChangeHandler();
        public event HealthChangeHandler HealthChange;

        private void Start()
        {
            CurrentHealth = 100;
            _inventory = GetComponent<Inventory.Inventory>();
        }

        public void TakeDamage(float amount)
        {
            amount -= _inventory.Protection();
            if (amount < 0) amount = 0;
            CurrentHealth -= amount;
            if (CurrentHealth <= 0)
                Die();
            OnHealthChange();
        }

        private void Die()
        {
            _inventory.DropAllConsumables();
            OnHealthChange();
        }

        private void OnHealthChange()
        {
            if (HealthChange != null)
                HealthChange();
        }

    }
}
