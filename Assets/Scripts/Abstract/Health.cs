using UnityEngine;

namespace Assets.Scripts.Abstract
{
    public abstract class Health : MonoBehaviour
    {

        public float StartingHealth = 100f;
        public float CurrentHealth { get; private set; }

        public delegate void HealthChangeHandler();
        public event HealthChangeHandler HealthChange;

        private void Start()
        {
            CurrentHealth = StartingHealth;
            Init();
        }

        protected abstract void Init();

        /// <summary>
        /// calculates the actual damage with the protection and reduces CurrentHealth
        /// </summary>
        /// <param name="amount">Amount of damages</param>
        public void AddHealth(float amount)
        {
            CurrentHealth += amount;
            if (CurrentHealth > StartingHealth)
                CurrentHealth = StartingHealth;
            OnHealthChange();
        }

        /// <summary>
        /// calculates the actual damage with the protection and reduces CurrentHealth
        /// </summary>
        /// <param name="amount">Amount of damages</param>
        public virtual void TakeDamage(float amount)
        {
            if (CurrentHealth <= 0)
                return;
            Debug.Log(gameObject.name + " TakeDamage (" + amount + ")");
            if (amount < 0) amount = 0;
            CurrentHealth -= amount;
            if (CurrentHealth <= 0)
                Die();
            OnHealthChange();
        }

        /// <summary>
        /// Health is less or equal to 0
        /// </summary>
        protected abstract void Die();

        protected void OnHealthChange()
        {
            if (HealthChange != null)
                HealthChange();
        }

    }
}
