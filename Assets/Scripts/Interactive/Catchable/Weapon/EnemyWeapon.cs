using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CharacterController = Assets.Scripts.Character.CharacterController;
using Assets.Scripts.Interactive.Catchable.Abstract;
using Assets.Scripts.Utils;
using Assets.Scripts.Abstract;
using Assets.Scripts.Ennemies;

namespace Assets.Scripts.Interactive.Catchable.Weapon
{
    public class EnemyWeapon : Equipment
    {

        public float minDamage;
        public float maxDamage;
        public float Range;
        public float Radius = 1f;

        private EnemyController _enemyController;

        void Start()
        {
            Type = EquipmentEnum.Weapon;
            _enemyController = GetComponentInParent<EnemyController>();
            Range = _enemyController._enemyAttackRange;
        }

        public override void Use()
        {
            foreach (var health in GetHealthAround())
            {
                float attackDamage;
                attackDamage = Random.Range(minDamage, maxDamage + 1);
                print(_enemyController.name + " inflicted " + attackDamage + " damage");
                health.TakeDamage(attackDamage);
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
            return GetAllCollisions().Select(hit => hit.GetComponent<Health>()).Where(health => health != null && health.GetComponent<EnemyController>() == null);
        }

    }
}
