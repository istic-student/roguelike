using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Character;
using Assets.Scripts.Player;
using UnityEngine;
using CharacterController = Assets.Scripts.Character.CharacterController;

namespace Assets.Scripts.Interactive.Weapon
{
    public class MeleeWeapon : Equipment
    {
        
        public float Range;
        public float Radius = 1f;

        public override void Use()
        {
            //var direction = GetComponentInParent<CharacterController>().VectorDirection;

            //var point = new Vector2(); // Offset
            //var size = new Vector2(); // Size
            //var direction = CapsuleDirection2D.Vertical;
            //var angle = 0f;
            //// 0 : north | 1 : east | 2 : south | 3 : west 
            //switch (GetComponentInParent<CharacterController>().Direction)
            //{
            //    case 0:
            //        point = new Vector2(0, Range / 2);
            //        size = new Vector2(Radius, Range);
            //        break;
            //    case 1:
            //        point = new Vector2(Range / 2, 0);
            //        size = new Vector2(Range, Radius);
            //        direction = CapsuleDirection2D.Horizontal;
            //        angle = 90f;
            //        break;
            //    case 2:
            //        point = new Vector2(0, -(Range / 2));
            //        size = new Vector2(Radius, Range);
            //        break;
            //    case 3:
            //        point = new Vector2(-(Range / 2), 0);
            //        size = new Vector2(Range, Radius);
            //        direction = CapsuleDirection2D.Horizontal;
            //        angle = 90f;
            //        break;
            //}

            ////Debug.Log(point + " : " + size + " : " + direction);
            //var hits = Physics2D.OverlapCapsuleAll((Vector2)transform.position + point, size, direction, angle);
            ////var hits = Physics2D.OverlapCapsuleAll(transform.position, direction * Range, CapsuleDirection2D.Vertical, 0f);
            ////Debug.Log("Use " + this + " : hit : " + hits.Length);
            //foreach (var hit in hits)
            //{
            //    if (hit.name == "Stone")
            //        Debug.Log("Attack on " + hit);
            //}

            // (Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle




            //var hits = Physics2D.OverlapCircleAll(transform.position, Range);
            //Debug.Log("Use " + this + " : hit : " + hits.Length);
            //foreach (var hit in hits)
            //{
            //    Debug.Log("Attack on " + hit);
            //}
            //var direction = GetComponentInParent<CharacterController>().VectorDirection;
            //var hits = Physics2D.CircleCastAll(transform.position, 3f, direction, Range);

            ////var hitsa = Collider2D.
            ////Debug.Log("Local pos : " + transform.localPosition + " : " + transform.position);
            ////Debug.Log("Use hit : " + hits.Length + " : " + GetComponentInParent<CharacterController>().VectorDirection);
            ////var hits = Physics2D.CircleCastAll(transform.position, Range, transform.forward * 20, Range);
            ////Debug.Log("Use hit : " + hits.Length + " : " + transform.up);
            //foreach (var hit in hits)
            //{
            //    Debug.Log("Attack on " + hit.collider);
            //}
            foreach (var health in GetHealthAround())
            {
                health.TakeDamage(Attack);
            }
        }

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
        /// <returns>List of healths (player aren't included</returns>
        public IEnumerable<Health> GetHealthAround()
        {
            //var hits = Physics2D.OverlapCircleAll(transform.position, Range);
            //return hits.Select(hit => hit.GetComponent<Health>()).Where(health => health != null && health.GetComponent<PlayerController>() == null);

            //var hits = Physics2D.CircleCastAll(transform.position, Range, transform.forward, 1f);
            //return hits.Select(hit => hit.collider.GetComponent<Health>()).Where(health => health != null);
            return GetAllCollisions().Select(hit => hit.GetComponent<Health>()).Where(health => health != null && health.GetComponent<PlayerController>() == null);
        }

    }
}
