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
            switch(GetComponentInParent<CharacterController>().Direction) {
                case 1:
                    projectile.transform.eulerAngles =  new Vector3Int(0, 0, 270);
                    break;
                case 2:
                    projectile.transform.eulerAngles =  new Vector3Int(0, 0, 180);
                    break;
                case 3:
                    projectile.transform.eulerAngles =  new Vector3Int(0, 0, 90);
                    break;
                default:
                    break;
            }
            var rb = projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(direction * ProjectileSpeed);
        }             

    }
}
