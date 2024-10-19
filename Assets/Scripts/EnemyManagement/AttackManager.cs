/***************************************************************
file: AttackManager.cs
author: Alex Mariano
class: CS 4700 – Game Development
assignment: program 3
date last modified: 10/18/2024

purpose: This program defines specific attack patterns per
wave of attack.

****************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{

    public GameObject alienManifest;
    public GameObject attackPatternRedL;
    public GameObject attackPatternRedR;
    public GameObject attackPatternYellowL;
    public GameObject attackPatternYellowR;
    
    private AlienManifest alienManifestScript;

    public bool active = false;
    
    private int attackPattern = 1;

    private bool doRandomAttacks = false;
    

    // Start is called before the first frame update
    void Start()
    {
        alienManifestScript = alienManifest.GetComponent<AlienManifest>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (active == true)
        // {
        //     StartCoroutine(ConfigureAndStartAttackPattern());
        //     active = false;
        // }
    }

    //function: DoAnAttack
    //purpose: Public visible function to start coroutine ConfigureAndStartAttackPattern().
    public void DoAnAttack()
    {
        StartCoroutine(ConfigureAndStartAttackPattern());
    }

    //function: ConfigureAndStartAttackPattern
    //purpose: Lists out different enemy attack patterns and cycles through
    //them when called.
    IEnumerator ConfigureAndStartAttackPattern()
    {   bool attackA = false;
        bool attackB = false;
        bool attackC = true;
        for (int i = 0; i < 5; i++)
        {
            if (doRandomAttacks)
            {
                attackPattern = Random.Range(1, 5);
            }
            if (attackPattern == 1)
            {
                attackA = EnemyStartAttack(alienColor.red, side.left, 0, attackPatternRedL);
                yield return new WaitForSeconds(0.5f);
                attackB = EnemyStartAttack(alienColor.yellow, side.left, 0, attackPatternYellowL);
            }
            if (attackPattern == 2)
            {
                attackA = EnemyStartAttack(alienColor.red, side.right, 0, attackPatternRedR);
                yield return new WaitForSeconds(1.0f);
                attackB = EnemyStartAttack(alienColor.yellow, side.left, 0, attackPatternYellowL);
            }
            if (attackPattern == 3)
            {
                attackA = EnemyStartAttack(alienColor.red, side.left, 0, attackPatternRedL);
                yield return new WaitForSeconds(1.0f);
                attackB = EnemyStartAttack(alienColor.yellow, side.right, 0, attackPatternYellowR);
            }
            if (attackPattern == 4)
            {
                attackA = EnemyStartAttack(alienColor.red, side.right, 0, attackPatternRedR);
                yield return new WaitForSeconds(0.5f);
                attackB = EnemyStartAttack(alienColor.yellow, side.right, 0, attackPatternYellowR);
            }
            attackPattern++;
            if (attackPattern > 4)
            {
                doRandomAttacks = true;
            }   
            if (doRandomAttacks)
            {
                attackPattern = Random.Range(5, 9);

                if (attackPattern == 5)
                {
                    attackC = EnemyStartAttack(1, attackPatternRedL);
                }
                if (attackPattern == 6)
                {
                    attackC = EnemyStartAttack(2, attackPatternRedL);
                }
                if (attackPattern == 7)
                {
                    attackC = EnemyStartAttack(3, attackPatternRedR);
                }
                if (attackPattern == 8)
                {
                    attackC = EnemyStartAttack(4, attackPatternRedR);
                }                           
            }
            bool attacked = attackA && attackB && attackC;
            if (attacked)
            {
                break;
            }
        }
    }

    //function: EnemyStartAttack
    //purpose: Retrieves an attacking unit and sets its attack pattern
    //according to the color and position in its formation.
    bool EnemyStartAttack(alienColor color, side sidePosition, int groupId, GameObject attackPattern)
    {
        GameObject attacker = alienManifestScript.GetAlienFittingParamters(color, sidePosition, groupId);
        if (attacker != null)
        {
            EnemyMovement attackerMovement = attacker.GetComponent<EnemyMovement>();
            attackerMovement.SetAttackPattern(attackPattern);
            attackerMovement.Attack();
            StartCoroutine(ShootWhileAttack(attacker));
            return true;
        }
        return false;
    }

    //function: EnemyStartAttack
    //purpose: Retrieves an attacking group unit and sets its attack pattern
    //according to the color and position in its formation.
    bool EnemyStartAttack(int groupId, GameObject attackPattern)
    {
        List<GameObject> attackers = alienManifestScript.GetAliensFittingGroupId(groupId);
        if (attackers.Count > 0)
        {
            foreach (GameObject attacker in attackers)
            {
                EnemyMovement attackerMovement = attacker.GetComponent<EnemyMovement>();
                attackerMovement.SetAttackPattern(attackPattern);
                attackerMovement.Attack();
                StartCoroutine(ShootWhileAttack(attacker));           
            }
            return true;
        }
        return false;
    }

    //funciton ShootWhileAttack
    //purpose: Sets an attack to do its attack pattern.
    IEnumerator ShootWhileAttack(GameObject attacker)
    {
        EnemyMissile missleShoot = attacker.GetComponent<EnemyMissile>();
        yield return new WaitForSeconds(0.5f);
        missleShoot.FireOnce();
    }

}
