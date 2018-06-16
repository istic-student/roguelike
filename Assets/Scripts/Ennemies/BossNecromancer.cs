using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Character;

public class BossNecromancer : MonoBehaviour {

    private CharacterHealth _characterHealth;
    private bool phaseThreeStarted, firstSummon, secondSummon;

    // Use this for initialization
    void Start () {
        _characterHealth = GetComponent<CharacterHealth>();
        phaseThreeStarted = false;
        firstSummon = false;
        secondSummon = false;
    }

    public void Attack()
    {
        if ( _characterHealth.CurrentHealth < 30 && phaseThreeStarted == false)
        {
            phaseThree();
            phaseThreeStarted = true;
        }
        if ( _characterHealth.CurrentHealth < 60)
        {
            phaseTwo();
        }

        phaseOne();
    }

    public void phaseOne()
    {
        //the default pattern of the necromancer used in this fight

        //staf attack with a chance to inflict poison damage

        //small chance to summon one skeleton

        int rollDice;

        rollDice = Random.Range(0, 5);

        if (rollDice == 5 && firstSummon == false)
        {
            //summonSkeleton(1);
            firstSummon = true;
        } else
        {
            rollDice = Random.Range(4, 8);
            //inflictDamage(rollDice);
        }

    }

    public void phaseTwo()
    {
        //at 66% hp the necromancer start to use this pattern with the first one

        //medium chance to summon one skeleton

        //medium chance to heal (small) skeleton if one is present

        int rollDice;

        rollDice = Random.Range(0, 5);

        if (rollDice > 3 && secondSummon == false)
        {
            //summonSkeleton(2);
            secondSummon = true;
        }
        else
        {
            rollDice = Random.Range(4, 8);
            //inflictDamage(rollDice);
        }
    }

    public void phaseThree()
    {
        //at 33% hp he use also this pattern once to summon 3 weak skeletton to help him

        //summonSkeleton(3,4,5);
    }

    public void summonSkeleton()
    {
        // smt like getComponent(skeleton).enabled();
        // le skeletton en disabled aurait alors un script qui le fait suivre seulement le necromancien tant qu'il n'apparait pas 
        // ou vu que c'est une salle de boss une range tres grande qui le fais detecter automatique le player pour l'attaquer
    }

    // Update is called once per frame
    void Update () {
		
	}
}
