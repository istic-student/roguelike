using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Character;
using Assets.Scripts.Player;
using CharacterController = Assets.Scripts.Character.CharacterController;

namespace Assets.Scripts.Ennemies
{

    public class EnemyController : MonoBehaviour
    {

        public delegate void EnnemyChangeHandler(EnemyController ennemyController);
        public event EnnemyChangeHandler EnnemyChange;

        private Character.Inventory.Inventory _inventory;
        private CharacterHealth _characterHealth;
        public int _enemyDetectionRange;
        public int _enemyAttackRange;
        public float _enemySpeed;
        private float _playerDistance;
        private int _playerToChase;
        private CharacterController _characterController;
        private BossNecromancer _bossNecromancer;

        private Transform target;
        private bool trackEnnemy;
        public bool enemyIsBoss;

        private int waitFrameToWalk;
        private float randomX, randomY;

        // Use this for initialization
        void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _inventory = GetComponent<Character.Inventory.Inventory>();
            _characterHealth = GetComponent<CharacterHealth>();
            _characterHealth.HealthChange += Notify;
            _inventory.InventoryChange += Notify;
            trackEnnemy = false;
            _playerDistance = 150;
            _playerToChase = 0;
            _enemyDetectionRange = 10;
            _enemyAttackRange = 2;
            _enemySpeed = 2.5f;
            _characterController.Speed = _enemySpeed;
            waitFrameToWalk = 0;
            randomX = 0; randomY = 0;

            //to generalize (detect if normal mob or boss)
            if (GetComponent<BossNecromancer>() != null) {
                enemyIsBoss = true;
                _bossNecromancer = GetComponent<BossNecromancer>();
            } else
            {
                enemyIsBoss = false;
            }
            
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
                if (Vector2.Distance(target.position, this.transform.position) > _enemyAttackRange)
                {
                    this.moveTorwardPlayer();
                }
          
                if (Vector2.Distance(target.position, this.transform.position) <= _enemyAttackRange)
                {
                    print("the player is at hitting range !");
                    
                    hitPlayer();
                }
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
            if (waitFrameToWalk == 0)
            {
                randomX = Random.Range(-1f, 1f);
                randomY = Random.Range(-1f, 1f);
                waitFrameToWalk++;
            }

            if (waitFrameToWalk < 5)
            {
                _characterController.Move(randomX, randomY);
                waitFrameToWalk++;
            } else
            {
                waitFrameToWalk = 0;
            }
            
        }

        public void hitPlayer()
        {
            if (enemyIsBoss)
            {
                print("Started attacking");
                if (_inventory.Weapon == null)
                {
                    print("no weapon enemy controller");
                }
                _bossNecromancer.Attack();
            } else
            {
                if (_inventory.Weapon == null)
                    return;
                Debug.Log("Attack with " + _inventory.Weapon.name);
                _inventory.Weapon.Use();
            }
            
        }

        public void Notify()
        {
            if (EnnemyChange != null)
                EnnemyChange(this);
        }
    }

}
