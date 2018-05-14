using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerRaycaster : MonoBehaviour
    {

        public float RayDistance = 1.5f;

        private PlayerController _playerController;
        private PlayerInteraction _playerInteraction;

        private void Start()
        {
            _playerController = GetComponent<PlayerController>();
            _playerInteraction = GetComponent<PlayerInteraction>();
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
            var colliderInteractive = hit.collider.GetComponent<Interactive.Abstract.Interactive>();
            if (hit.collider == null && _playerInteraction.ColliderInteractive != null)
                _playerInteraction.ColliderInteractive = null;
            else if (hit.collider != null && colliderInteractive.Equals(_playerInteraction.ColliderInteractive))
                _playerInteraction.ColliderInteractive = colliderInteractive;
        }

        private Vector2 GetVectorDirection()
        {
            switch (_playerController.Direction)
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
