using System;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class CharacterController : MonoBehaviour
    {

        public float Speed;
        public bool IsMoving;
        public int Direction = 2; // 0 : north | 1 : east | 2 : south | 3 : west
        public Vector2 preciseDirection;
        public float AnimationTolerance = 0.1f;

        public Vector2 VectorDirection
        {
            get
            {
                switch (Direction)
                {
                    case 0:
                        return Vector2.up;
                    case 1:
                        return Vector2.right;
                    case 2:
                        return Vector2.down;
                    case 3:
                        return Vector2.left;
                }
                return Vector2.up;
            }
        }

        private Rigidbody2D _rigidbody2D;
        private CharacterHealth _characterHealth;

        public void Start()
        {
            _characterHealth = GetComponent<CharacterHealth>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Move(float axisHorizontal, float axisVertical)
        {
            var moveDirection = new Vector2(axisHorizontal, axisVertical);
            var speed = Speed;
            SetDirection(axisHorizontal, axisVertical);
            IsMoving = Math.Abs(axisVertical) > AnimationTolerance || Math.Abs(axisHorizontal) > AnimationTolerance;
            _rigidbody2D.velocity = moveDirection.normalized * speed;
        }

        private void SetDirection(float axisHorizontal, float axisVertical)
        {
            preciseDirection.x = axisHorizontal;
            preciseDirection.y = axisVertical;
            if (Math.Abs(axisVertical) >= Math.Abs(axisHorizontal))
            {
                if (axisVertical > 0)
                    Direction = 0;
                else if (axisVertical < 0)
                    Direction = 2;
            } else {
                if (axisHorizontal > 0)
                    Direction = 1;
                else if (axisHorizontal < 0)
                    Direction = 3;
            }
        }

    }
}
