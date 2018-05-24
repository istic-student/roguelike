using System;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class CharacterController : MonoBehaviour
    {

        public float Speed;
        public bool IsMoving;
        public int Direction = 2; // 0 : north | 1 : east | 2 : south | 3 : west 
        public float AnimationTolerance = 0.1f;

        private Rigidbody2D _rigidbody2D;
        private Health _health;

        public void Start()
        {
            _health = GetComponent<Health>();
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
