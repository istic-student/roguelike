using UnityEngine;

namespace Assets.Scripts.Character
{
    public class CharacterAnimatorController : MonoBehaviour
    {

        private CharacterController _characterController;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void Update()
        {
            switch (_characterController.Direction)
            {
                case 1:
                    _spriteRenderer.flipX = true;
                    break;
                case 3:
                    _spriteRenderer.flipX = false;
                    break;
            }

            _animator.SetInteger("Direction", _characterController.Direction);
            _animator.SetBool("Moving", _characterController.IsMoving);
        }
    }
}
