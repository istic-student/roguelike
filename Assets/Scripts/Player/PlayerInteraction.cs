using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerInteraction : MonoBehaviour
    {

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
            }

            if (Input.GetButtonDown(_joystick.Use))
            {
            }

        }

    }
}
