using Assets.Scripts.Character;
using UnityEngine;
using CharacterController = Assets.Scripts.Character.CharacterController;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        
        public Joystick Joystick { get; private set; }
        public int PlayerNumber;

        public delegate void PlayerChangeHandler(PlayerController playerController);
        public event PlayerChangeHandler PlayerChange;
        
        private Character.Inventory.Inventory _inventory;
        private Health _health;
        private CharacterController _characterController;
        private CharacterInteraction _characterInteraction;

        private void Start()
        {
            Joystick = new Joystick(PlayerNumber);
            _characterController = GetComponent<CharacterController>();
            _characterInteraction = GetComponent<CharacterInteraction>();
            _inventory = GetComponent<Character.Inventory.Inventory>();
            _health = GetComponent<Health>();
            _health.HealthChange += Notify;
            _inventory.InventoryChange += Notify;
        }

        private void FixedUpdate()
        {
            var axisHorizontal = Input.GetAxisRaw(Joystick.Horizontal);
            var axisVertical = Input.GetAxisRaw(Joystick.Vertical);
            _characterController.Move(axisHorizontal, axisVertical);
        }

        private void Update()
        {
            if (Input.GetButtonDown(Joystick.Action))
                _characterInteraction.Action();

            if (Input.GetButtonDown(Joystick.Use))
                _characterInteraction.Use();

            if (Input.GetButtonDown(Joystick.Attack))
                _characterInteraction.Attack();
        }

        public void Notify()
        {
            if (PlayerChange != null)
                PlayerChange(this);
        }

    }
}
