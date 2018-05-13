using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {

        public float Speed;
        public Joystick Joystick { get; private set; }
        public int PlayerNumber;
        public bool IsMoving;
        public int Direction = 2; // 0 : north | 1 : east | 2 : south | 3 : west 

        public float AnimationTolerance = 0.1f;

        private CharacterController _characterController;

        private void Start()
        {
            Joystick = new Joystick(PlayerNumber);
            _characterController = GetComponent<CharacterController>();
        }

        private void FixedUpdate()
        {
            var axisHorizontal = Input.GetAxisRaw(Joystick.Horizontal);
            var axisVertical = Input.GetAxisRaw(Joystick.Vertical);
            var moveDirection = new Vector3(axisHorizontal, axisVertical, 0);
            var speed = Speed;

            SetDirection(axisHorizontal, axisVertical);

            IsMoving = Math.Abs(axisVertical) > AnimationTolerance || Math.Abs(axisHorizontal) > AnimationTolerance;

            _characterController.Move(moveDirection.normalized * Time.deltaTime * speed);
        }

        private void SetDirection(float axisHorizontal, float axisVertical)
        {
            if (Math.Abs(axisVertical) >= Math.Abs(axisHorizontal))
            {
                if (axisVertical > 0)
                    Direction = 0;
                else if (axisVertical < 0)
                    Direction = 2;
            }
            else
            {
                if (axisHorizontal > 0)
                    Direction = 1;
                else if (axisHorizontal < 0)
                    Direction = 3;
            }
        }

    }
}
