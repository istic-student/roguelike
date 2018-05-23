﻿using Assets.Scripts.Interactive;
using Assets.Scripts.Interactive.Abstract;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerInteraction : MonoBehaviour
    {

        public float DammageWithoutWeapon;

        [HideInInspector]
        public Interactive.Abstract.Interactive ColliderInteractive;

        private PlayerController _playerController;
        private Inventory.Inventory _inventory;
        private Joystick _joystick;

        private void Start()
        {
            _playerController = GetComponent<PlayerController>();
            _inventory = GetComponent<Inventory.Inventory>();
            _joystick = _playerController.Joystick;
        }

        private void Update()
        {
            if (_joystick == null)
            {
                _playerController = GetComponent<PlayerController>();
                _joystick = _playerController.Joystick;
                return;
            }

            if (Input.GetButtonDown(_joystick.Action))
                Action();

            if (Input.GetButtonDown(_joystick.Use))
                Use();

            if (Input.GetButtonDown(_joystick.Attack))
                Attack();
        }

        private void Action()
        {
            var catchable = ColliderInteractive as Catchable;
            if (catchable == null) return;
            Debug.Log("Action : " + catchable);
            _inventory.Add(catchable);
        }

        private void Use()
        {
            var activable = ColliderInteractive as Activable;
            if (activable == null) return;

            Debug.Log("Use : " + activable);
            if (!activable.Active())
                _inventory.TryToUse(activable);
        }

        private void Attack()
        {
            Debug.Log("Attack");
            if (_inventory.Weapon != null)
                _inventory.Weapon.Use();
        }

    }
}
