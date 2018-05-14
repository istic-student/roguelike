using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Inventory : MonoBehaviour {

        public List<int> Items;

        private PlayerController _playerController;

        private void Start()
        {
            _playerController = GetComponent<PlayerController>();
        }

        public void AddItem(int item)
        {
            Items.Add(item);
            _playerController.Notify();
        }

        public void DropItem(int item)
        {
            Items.Remove(item);
            _playerController.Notify();
        }

        public void DropAll()
        {
            foreach (var item in Items)
            {
                DropItem(item);
            }
        }

        public void TryToUse()
        {
            foreach (var item in Items)
            {
            }
        }

    }
}
