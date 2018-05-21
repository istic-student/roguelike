using Assets.Scripts.Interactive;
using Assets.Scripts.Interactive.Abstract;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerInteraction : MonoBehaviour
    {

        [HideInInspector]
        public Interactive.Abstract.Interactive ColliderInteractive;

        private PlayerController _playerController;
        private Inventory.Inventory _inventory;
        private Joystick _joystick;

        private void Start()
        {
            _playerController = GetComponent<PlayerController>();
            _inventory = GetComponent<Inventory.Inventory>();
            _joystick = _playerController.Joystick;
        }

        private void Update()
        {
            if (_joystick == null)
            {
                _playerController = GetComponent<PlayerController>();
                _joystick = _playerController.Joystick;
                return;
            }

            if (Input.GetButtonDown(_joystick.Action))
            {
                _inventory.Add(ColliderInteractive as Catchable);
            }

            if (Input.GetButtonDown(_joystick.Use))
            {
                var activable = ColliderInteractive as Activable;
                if (activable != null)
                {
                    if (!activable.Active())
                        _inventory.TryToUse(activable);
                }
            }

        }

        public void Attack()
        {
            // todo : CircleCast range
        }

    }
}
