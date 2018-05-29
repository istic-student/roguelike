using Assets.Scripts.Abstract;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Interactive.Catchable.Weapon
{
    public class Projectile : MonoBehaviour
    {
        
        public float Damages;

        private void OnCollisionEnter2D(Collision2D hit)
        {
            if (hit.gameObject.GetComponent<PlayerController>() != null)
                return;
            var health = hit.gameObject.GetComponent<Health>();
            if (health != null)
                health.TakeDamage(Damages);
            Destroy(gameObject);
        }

    }
}
