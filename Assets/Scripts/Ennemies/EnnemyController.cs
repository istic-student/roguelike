using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Character;
using CharacterController = Assets.Scripts.Character.CharacterController;

namespace Assets.Scripts.Player
{

    public class EnnemyController : MonoBehaviour
    {

        //public int EnnemyNumber;

        public delegate void EnnemyChangeHandler(EnnemyController ennemyController);
        public event EnnemyChangeHandler EnnemyChange;

        private Character.Inventory.Inventory _inventory;
        private Health _health;
        private int _playerDistance;
        private CharacterController _characterController;
        private CharacterInteraction _characterInteraction;

        private Transform target;
        private bool trackEnnemy;

        // Use this for initialization
        void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _characterInteraction = GetComponent<CharacterInteraction>();
            _inventory = GetComponent<Character.Inventory.Inventory>();
            _health = GetComponent<Health>();
            _health.HealthChange += Notify;
            _inventory.InventoryChange += Notify;
            trackEnnemy = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (_playerDistance < 5) //detection d'un joueur a portée
            {
                print("the player has been detected !");
                trackEnnemy = true;                
            }

            if (trackEnnemy) //si le joueur a été détecté on recupère sa position pour le suivre
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        public void moveTorwardPlayer()
        {
            float xDir = 0, yDir = 0;
            if (Mathf.Abs (target.position.x - transform.position.x) < float.Epsilon)
            {
                yDir = target.position.y > transform.position.y ? 1 : -1; //move up or down
            } else
            {
                xDir = target.position.x > transform.position.x ? 1 : -1; //move right or left
            }

            _characterController.Move(xDir, yDir);
        }

        public void moveRandomly()
        {
            //plus tard a scripter des pattern de deplacement pou chaque type d'ennemie
            _characterController.Move(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }

        public void Notify()
        {
            if (EnnemyChange != null)
                EnnemyChange(this);
        }
    }

}
