using System.Linq;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class CharacterRaycaster : MonoBehaviour
    {

        public float RayDistance = 1.5f;
        public float RayRadius = 1f;

        private CharacterController _characterController;
        private CharacterInteraction _characterInteraction;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _characterInteraction = GetComponent<CharacterInteraction>();
        }

        private void Update()
        {
            var direction = _characterController.VectorDirection;

            var hits = Helpers.OverlapCapsuleAll(transform.position, RayRadius, RayDistance * 2, _characterController.Direction);
            var transforms = hits
                .Where(x => x.gameObject != gameObject)
                .Select(x => x.transform);
            var hit = transform.GetClosest(transforms);
            SetColliderInteractive(hit);

            Debug.DrawRay(transform.position, direction * RayDistance, Color.green);
        }

        private void SetColliderInteractive(Component hit)
        {
            Interactive.Abstract.Interactive colliderInteractive = null;
            if (hit != null)
                colliderInteractive = hit.GetComponent<Interactive.Abstract.Interactive>();

            if (colliderInteractive == null && _characterInteraction.ColliderInteractive != null)
                _characterInteraction.ColliderInteractive = null;
            else if (colliderInteractive != null && !colliderInteractive.Equals(_characterInteraction.ColliderInteractive))
                _characterInteraction.ColliderInteractive = colliderInteractive;
        }

    }
}
