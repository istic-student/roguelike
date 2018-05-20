using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Health : MonoBehaviour
    {

        public int StartingHealth = 100;
        public int CurrentHealth { get; private set;  }

        private Inventory.Inventory _inventory;
        private PlayerController _playerController;

        private void Start()
        {
            CurrentHealth = 100;
            _playerController = GetComponent<PlayerController>();
            _inventory = GetComponent<Inventory.Inventory>();
            _playerController.Notify();
        }

        public void TakeDamage(int amount)
        {
            CurrentHealth -= amount;
            if (CurrentHealth <= 0)
                Die();
            _playerController.Notify();
        }

        private void Die()
        {
            _inventory.DropAllConsumable();
            _playerController.Notify();
        }

    }
}
