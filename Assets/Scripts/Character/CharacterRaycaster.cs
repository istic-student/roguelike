using UnityEngine;

namespace Assets.Scripts.Character
{
    public class CharacterRaycaster : MonoBehaviour
    {

        public float RayDistance = 1.5f;

        private CharacterController _characterController;
        private CharacterInteraction _characterInteraction;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _characterInteraction = GetComponent<CharacterInteraction>();
        }

        private void Update()
        {
            var direction = GetVectorDirection();
            var hit = Physics2D.Raycast(transform.position, direction, RayDistance);
            SetColliderInteractive(hit);

            Debug.DrawRay(transform.position, direction * RayDistance, Color.green);
        }

        private void SetColliderInteractive(RaycastHit2D hit)
        {
            Interactive.Abstract.Interactive colliderInteractive = null;
            if (hit.collider != null)
                colliderInteractive = hit.collider.GetComponent<Interactive.Abstract.Interactive>();

            if (colliderInteractive == null && _characterInteraction.ColliderInteractive != null)
                _characterInteraction.ColliderInteractive = null;
            else if (colliderInteractive != null && !colliderInteractive.Equals(_characterInteraction.ColliderInteractive))
                _characterInteraction.ColliderInteractive = colliderInteractive;
        }

        private Vector2 GetVectorDirection()
        {
            switch (_characterController.Direction)
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
}
