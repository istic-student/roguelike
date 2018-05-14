using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Health : MonoBehaviour {

        public int StartingHealth = 100;

        private int _currentHealth;
        private Inventory _inventory;

        private void Start()
        {
            _currentHealth = 100;
            _inventory = GetComponent<Inventory>();
        }

        public void TakeDamage(int amount)
        {
            _currentHealth -= amount;
            if (_currentHealth <= 0)
                Die();
        }

        void Die()
        {
            _inventory.DropAll();
        }

    }
}
