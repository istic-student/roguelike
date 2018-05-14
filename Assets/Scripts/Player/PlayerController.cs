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

        public delegate void PlayerChangeHandler(PlayerController playerController);
        public event PlayerChangeHandler PlayerChange;

        private Rigidbody2D _rigidbody2D;

        private void Start()
        {
            Joystick = new Joystick(PlayerNumber);
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            var axisHorizontal = Input.GetAxisRaw(Joystick.Horizontal);
            var axisVertical = Input.GetAxisRaw(Joystick.Vertical);
            var moveDirection = new Vector2(axisHorizontal, axisVertical);
            var speed = Speed;

            SetDirection(axisHorizontal, axisVertical);

            IsMoving = Math.Abs(axisVertical) > AnimationTolerance || Math.Abs(axisHorizontal) > AnimationTolerance;
            
            _rigidbody2D.velocity = moveDirection.normalized * speed;
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

        public void Notify()
        {
            if (PlayerChange != null)
                PlayerChange(this);
        }

    }
}
