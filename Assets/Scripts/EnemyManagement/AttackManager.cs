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

    public void DoAnAttack()
    {
        StartCoroutine(ConfigureAndStartAttackPattern());
    }

    IEnumerator ConfigureAndStartAttackPattern()
    {
        if (attackPattern == 0)
        {
            attackPattern = Random.Range(1, 5);
        }
        if (attackPattern == 1)
        {
            EnemyStartAttack(alienColor.red, side.left, 0, attackPatternRedL);
            yield return new WaitForSeconds(0.5f);
            EnemyStartAttack(alienColor.yellow, side.left, 0, attackPatternYellowL);
        }
        if (attackPattern == 2)
        {
            EnemyStartAttack(alienColor.red, side.right, 0, attackPatternRedR);
            yield return new WaitForSeconds(1.0f);
            EnemyStartAttack(alienColor.yellow, side.left, 0, attackPatternYellowL);
        }
        if (attackPattern == 3)
        {
            EnemyStartAttack(alienColor.red, side.left, 0, attackPatternRedL);
            yield return new WaitForSeconds(1.0f);
            EnemyStartAttack(alienColor.yellow, side.right, 0, attackPatternYellowR);
        }
        if (attackPattern == 4)
        {
            EnemyStartAttack(alienColor.red, side.right, 0, attackPatternRedR);
            yield return new WaitForSeconds(0.5f);
            EnemyStartAttack(alienColor.yellow, side.right, 0, attackPatternYellowR);
        }
        attackPattern++;
        if (attackPattern > 4)
        {
            attackPattern = 0;
        }
    }



    void EnemyStartAttack(alienColor color, side sidePosition, int groupId, GameObject attackPattern)
    {
        GameObject attacker = alienManifestScript.GetAlienFittingParamters(color, sidePosition, groupId);
        if (attacker != null)
        {
            EnemyMovement attackerMovement = attacker.GetComponent<EnemyMovement>();
            attackerMovement.SetAttackPattern(attackPattern);
            attackerMovement.Attack();
            StartCoroutine(ShootWhileAttack(attacker));
        }

    }

    IEnumerator ShootWhileAttack(GameObject attacker)
    {
        EnemyMissile missleShoot = attacker.GetComponent<EnemyMissile>();
        yield return new WaitForSeconds(0.5f);
        missleShoot.FireOnce();
    }

}
