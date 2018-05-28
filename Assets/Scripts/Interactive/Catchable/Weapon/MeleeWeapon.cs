using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Abstract;
using Assets.Scripts.Player;
using UnityEngine;
using CharacterController = Assets.Scripts.Character.CharacterController;

namespace Assets.Scripts.Interactive.Catchable.Weapon
{
    public class MeleeWeapon : Equipment
    {
        
        public float Range;
        public float Radius = 1f;

        public override void Use()
        {
            foreach (var health in GetHealthAround())
            {
                health.TakeDamage(Attack);
            }
        }

        /// <summary>
        /// Get all collisions in a capsule
        /// </summary>
        /// <returns>collisions in a capsule</returns>
        private IEnumerable<Collider2D> GetAllCollisions()
        {
            var point = new Vector2(); // Offset
            var size = new Vector2(); // Size
            var direction = CapsuleDirection2D.Vertical;
            var angle = 0f;
            switch (GetComponentInParent<CharacterController>().Direction)
            {
                case 0:
                    point = new Vector2(0, Range / 2);
                    size = new Vector2(Radius, Range);
                    break;
                case 1:
                    point = new Vector2(Range / 2, 0);
                    size = new Vector2(Range, Radius);
                    direction = CapsuleDirection2D.Horizontal;
                    angle = 90f;
                    break;
                case 2:
                    point = new Vector2(0, -(Range / 2));
                    size = new Vector2(Radius, Range);
                    break;
                case 3:
                    point = new Vector2(-(Range / 2), 0);
                    size = new Vector2(Range, Radius);
                    direction = CapsuleDirection2D.Horizontal;
                    angle = 90f;
                    break;
            }
            return Physics2D.OverlapCapsuleAll((Vector2)transform.position + point, size, direction, angle);
        }

        /// <summary>
        /// Get all healths arround the weapon with a range
        /// </summary>
        /// <returns>List of healths (players aren't included)</returns>
        public IEnumerable<Health> GetHealthAround()
        {
            return GetAllCollisions().Select(hit => hit.GetComponent<Health>()).Where(health => health != null && health.GetComponent<PlayerController>() == null);
        }

    }
}
