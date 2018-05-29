using Assets.Scripts.Interactive.Abstract;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class CharacterInteraction : MonoBehaviour
    {

        [HideInInspector]
        public Interactive.Abstract.Interactive ColliderInteractive;

        private Inventory.Inventory _inventory;

        public void Start()
        {
            _inventory = GetComponent<Inventory.Inventory>();
        }

        public void Action()
        {
            var catchable = ColliderInteractive as Catchable;
            if (catchable == null) return;
            Debug.Log("Action : " + catchable.name);
            _inventory.Add(catchable);
        }

        public void Use()
        {
            var activable = ColliderInteractive as Activable;
            if (activable == null) return;

            Debug.Log("Use : " + activable.name);
            if (activable.Active())
                return;
            _inventory.TryToUse(activable);
        }

        public void Attack()
        {
            if (_inventory.Weapon == null)
                return;
            Debug.Log("Attack with " + _inventory.Weapon.name);
            _inventory.Weapon.Use();
        }

    }
}
