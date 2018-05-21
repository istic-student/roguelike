﻿using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Health : MonoBehaviour
    {

        public float StartingHealth = 100f;
        public float CurrentHealth { get; private set;  }

        private Inventory.Inventory _inventory;
        private PlayerController _playerController;

        private void Start()
        {
            CurrentHealth = 100;
            _playerController = GetComponent<PlayerController>();
            _inventory = GetComponent<Inventory.Inventory>();
            _playerController.Notify();
        }

        public void TakeDamage(float amount)
        {
            amount -= _inventory.Protection();
            if (amount < 0) amount = 0;
            CurrentHealth -= amount;
            if (CurrentHealth <= 0)
                Die();
            _playerController.Notify();
        }

        private void Die()
        {
            _inventory.DropAllConsumables();
            _playerController.Notify();
        }

    }
}
