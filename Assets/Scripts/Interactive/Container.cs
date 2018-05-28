using System.Collections.Generic;
using Assets.Scripts.Manager;
using UnityEngine;

namespace Assets.Scripts.Interactive
{
    public class Container : MonoBehaviour
    {

        public Transform CatchablesTransform;

        private List<Abstract.Catchable> _catchables;

        public void Start()
        {
            _catchables = new List<Abstract.Catchable>();
            foreach (var childCatchable in CatchablesTransform.GetComponentsInChildren<Abstract.Catchable>())
            {
                _catchables.Add(childCatchable);
            }
        }

        /// <summary>
        /// Drop all elements in the chest
        /// </summary>
        public void DropAll()
        {
            foreach (var item in _catchables)
            {
                item.transform.parent = GameManager.AssetsManager.InteractiveTransform;
            }
        }

    }
}
