using System.Collections.Generic;
using Assets.Scripts.Manager;
using UnityEngine;

namespace Assets.Scripts.Interactive.Activable
{
    public class Chest : Abstract.Activable
    {

        public List<Abstract.Catchable> Catchables;
        public Transform CatchablesTransform;

        public void Start()
        {
            foreach (var childCatchable in CatchablesTransform.GetComponentsInChildren<Abstract.Catchable>())
            {
                Catchables.Add(childCatchable);
            }
        }

        protected override void Unlock()
        {
            Debug.Log("Open Chest");
            DropAll();
            // todo : play animation
        }

        /// <summary>
        /// Drop all elements in the chest
        /// </summary>
        private void DropAll()
        {
            foreach (var item in Catchables)
            {
                item.transform.parent = GameManager.AssetsManager.InteractiveTransform;
            }
        }

    }
}
