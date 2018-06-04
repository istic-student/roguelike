using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerAnimatorController : MonoBehaviour
    {

        private PlayerController _playerController;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _playerController = GetComponent<PlayerController>();
            _animator = GetComponentInChildren<Animator>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void Update()
        {
            switch (_playerController.Direction)
            {
                case 1:
                    _spriteRenderer.flipX = true;
                    break;
                case 3:
                    _spriteRenderer.flipX = false;
                    break;
            }

            _animator.SetInteger("Direction", _playerController.Direction);
            _animator.SetBool("Moving", _playerController.IsMoving);
        }
    }
}
