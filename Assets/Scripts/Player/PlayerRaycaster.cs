using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerRaycaster : MonoBehaviour
    {

        public float RayDistance = 1.5f;

        private PlayerController _playerController;

        private void Start()
        {
            _playerController = GetComponent<PlayerController>();
        }

        private void Update()
        {

            var direction = GetVectorDirection();
            var hit = Physics2D.Raycast(transform.position, direction, RayDistance);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider);
            }

            Debug.DrawRay(transform.position, direction * RayDistance, Color.green);

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
