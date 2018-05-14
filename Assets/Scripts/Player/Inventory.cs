using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Inventory : MonoBehaviour {

        public List<int> Items;

        public void AddItem(int item)
        {
            Items.Add(item);
        }

        public void DropItem(int item)
        {
            Items.Remove(item);
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
