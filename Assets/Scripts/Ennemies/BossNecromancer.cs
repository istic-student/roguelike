using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Character;

namespace Assets.Scripts.Ennemies
{
    public class BossNecromancer : MonoBehaviour
    {

        private CharacterHealth _characterHealth;
        private Character.Inventory.Inventory _inventory;
        private bool phaseThreeStarted, firstSummon, secondSummon;

        // Use this for initialization
        void Start()
        {
            _characterHealth = GetComponent<CharacterHealth>();
            _inventory = GetComponent<Character.Inventory.Inventory>();
            phaseThreeStarted = false;
            firstSummon = false;
            secondSummon = false;
        }

        public void Attack()
        {
            if (_characterHealth.CurrentHealth < 30 && phaseThreeStarted == false)
            {
                print("phase two started");
                phaseThree();
                phaseThreeStarted = true;
            }
            if (_characterHealth.CurrentHealth < 60)
            {
                print("phase two");
                phaseTwo();
            }
            print("phase one");
            phaseOne();
        }

        public void phaseOne()
        {
            //the default pattern of the necromancer used in this fight

            //staf attack with a chance to inflict poison damage

            //small chance to summon one skeleton

            int rollDice;
            rollDice = Random.Range(0, 10);

            if (rollDice == 9 && firstSummon == false)
            {
                transform.GetChild(1).gameObject.SetActive(true);
                firstSummon = true;
            }
            else
            {
                if (_inventory.Weapon == null)
                {
                    print("no weapon detected");
                    return;
                }
                else
                {
                    _inventory.Weapon.Use();
                }

            }

        }

        public void phaseTwo()
        {
            //at 66% hp the necromancer start to use this pattern with the first one

            //medium chance to summon one skeleton

            //medium chance to heal (small) skeleton if one is present

            int rollDice;

            rollDice = Random.Range(0, 10);
            print("roll dice for " + rollDice);

            if (rollDice > 7 && secondSummon == false)
            {
                transform.GetChild(2).gameObject.SetActive(true);
                secondSummon = true;
            }
            else
            {
                if (_inventory.Weapon == null)
                {
                    print("no weapon detected");
                    return;
                }
                else
                {
                    _inventory.Weapon.Use();
                }
            }
        }

        public void phaseThree()
        {
            //at 33% hp he use also this pattern once to summon 3 weak skeletton to help him

            transform.GetChild(3).gameObject.SetActive(true);
            transform.GetChild(4).gameObject.SetActive(true);
            transform.GetChild(5).gameObject.SetActive(true);
        }
    }
}
