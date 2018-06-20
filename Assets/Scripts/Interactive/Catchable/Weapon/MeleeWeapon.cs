using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Abstract;
using Assets.Scripts.Interactive.Catchable.Abstract;
using Assets.Scripts.Player;
using Assets.Scripts.Utils;
using UnityEngine;
using CharacterController = Assets.Scripts.Character.CharacterController;

namespace Assets.Scripts.Interactive.Catchable.Weapon
{
    public class MeleeWeapon : Equipment
    {
        
        public float Range;
        public float Radius = 1f;

        public MeleeWeapon()
        {
            Type = EquipmentEnum.Weapon;
        }

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
            return Helpers.OverlapCapsuleAll(transform.position, Radius, Range, GetComponentInParent<CharacterController>().Direction);
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
