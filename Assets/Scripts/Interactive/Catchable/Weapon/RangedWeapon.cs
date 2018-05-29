using Assets.Scripts.Interactive.Catchable.Abstract;
using UnityEngine;
using CharacterController = Assets.Scripts.Character.CharacterController;

namespace Assets.Scripts.Interactive.Catchable.Weapon
{
    public class RangedWeapon : Equipment
    {

        public float ProjectileSpeed;
        public Projectile Projectile;

        public RangedWeapon()
        {
            Type = EquipmentEnum.Weapon;
        }

        public override void Use()
        {
            var projectile = Instantiate(Projectile);

            var direction = GetComponentInParent<CharacterController>().VectorDirection;
            projectile.transform.position = transform.position + new Vector3(direction.x, direction.y);
            var rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(direction * ProjectileSpeed);
        }

    }
}
