﻿using Assets.Scripts.Character;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;
using Inventory = Assets.Scripts.Character.Inventory.Inventory;

namespace Assets.Scripts.UI
{
    public class PlayerUI : MonoBehaviour
    {

        public Image Icon;
        public Text Health;
        public Text Inventory;

        public void SetData(PlayerController playerController)
        {
            SetHealth(playerController.GetComponent<Health>());
            SetInventory(playerController.GetComponent<Inventory>());
        }

        private void SetIcon(Sprite image)
        {
            Icon.sprite = image;
        }

        private void SetHealth(Health health)
        {
            if (health == null)
                return;
            Health.text = health.CurrentHealth + " / " + health.StartingHealth;
        }

        private void SetInventory(Inventory inventory)
        {
            if (inventory == null)
                return;
            var str = "";
            foreach (var item in inventory.Consumables)
            {
                str += item + ", ";
            }
            Inventory.text = str;
        }

    }
}
