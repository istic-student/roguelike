using Assets.Scripts.Interactive.Abstract;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerInteraction : MonoBehaviour
    {

        [HideInInspector]
        public Interactive.Abstract.Interactive ColliderInteractive;

        private PlayerController _playerController;
        private Joystick _joystick;

        private void Start()
        {
            _playerController = GetComponent<PlayerController>();
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
                GetComponent<Inventory.Inventory>().Add(ColliderInteractive as Catchable);
            }

            if (Input.GetButtonDown(_joystick.Use))
            {
            }

        }

    }
}
