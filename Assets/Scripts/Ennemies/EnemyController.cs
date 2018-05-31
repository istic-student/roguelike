using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Character;
using Assets.Scripts.Interactive.Abstract;
using CharacterController = Assets.Scripts.Character.CharacterController;

namespace Assets.Scripts.Player
{

    public class EnemyController : MonoBehaviour
    {

        //public int EnnemyNumber;

        [HideInInspector]
        public Interactive.Abstract.Interactive ColliderInteractive;

        public delegate void EnnemyChangeHandler(EnemyController ennemyController);
        public event EnnemyChangeHandler EnnemyChange;

        private Character.Inventory.Inventory _inventory;
        private CharacterHealth _characterHealth;
        private int _enemyDetectionRange;
        private int _enemyAttackRange;
        private float _playerDistance;
        private int _playerToChase;
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
            _characterHealth = GetComponent<CharacterHealth>();
            _characterHealth.HealthChange += Notify;
            _inventory.InventoryChange += Notify;
            trackEnnemy = false;
            _playerDistance = 150;
            _playerToChase = 0;
            _enemyDetectionRange = 10;
            _enemyAttackRange = 3;
        }

        // Update is called once per frame
        void Update()
        {
            var playersPositions = FindObjectsOfType<PlayerController>(); //on récupère la position actuelle des joueurs par rapport à l'ennemie
            for (var i = 0; i < playersPositions.Length; i++)
            {
                var distTemp = Vector2.Distance(playersPositions[i].transform.position, this.transform.position);
                if (distTemp < _playerDistance)
                {
                    _playerDistance = distTemp;
                    _playerToChase = i;
                }
            }
            if (_playerDistance < _enemyDetectionRange) //detection d'un joueur a portée
            {
                print("the player has been detected !");
                trackEnnemy = true;                
            }

            if (trackEnnemy) //si le joueur a été détecté on recupère sa position pour le suivre
            {
                target = playersPositions[_playerToChase].transform;
                this.moveTorwardPlayer();
                if (Vector2.Distance(target.position, this.transform.position) < _enemyAttackRange)
                    hitPlayer();
            } else
            {
                this.moveRandomly();
            }
        }

        public void moveTorwardPlayer()
        {
            float xDir = 0, yDir = 0;
            yDir = target.position.y > transform.position.y ? 1 : -1; //move up or down
            xDir = target.position.x > transform.position.x ? 1 : -1; //move right or left

            _characterController.Move(xDir, yDir);
        }

        public void moveRandomly()
        {
            //plus tard a scripter des pattern de deplacement pou chaque type d'ennemie
            _characterController.Move(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }

        public void hitPlayer()
        {
            var playerToHit = ColliderInteractive as Interactive;
            if (playerToHit == null)
                return;
            Debug.Log("Attack " + playerToHit.tag + " with " + _inventory.Weapon.name);
            _inventory.Weapon.Use();
        }

        public void Notify()
        {
            if (EnnemyChange != null)
                EnnemyChange(this);
        }
    }

}
