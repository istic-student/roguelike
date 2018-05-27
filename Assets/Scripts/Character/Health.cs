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

        /// <summary>
        /// calculates the actual damage with the protection and reduces Current Health
        /// </summary>
        /// <param name="amount">Amount of damages</param>
        public void TakeDamage(float amount)
        {
            Debug.Log("TakeDamage (" + amount + ") " + gameObject);
            if (_inventory != null)
                amount -= _inventory.Protection();
            if (amount < 0) amount = 0;
            CurrentHealth -= amount;
            if (CurrentHealth <= 0)
                Die();
            OnHealthChange();
        }

        /// <summary>
        /// Health is less or equal to 0
        /// </summary>
        protected virtual void Die()
        {
            Debug.Log("Die " + gameObject);
            if (_inventory != null)
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
