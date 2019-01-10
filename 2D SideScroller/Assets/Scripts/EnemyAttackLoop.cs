using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackLoop : MonoBehaviour
{
    public GameObject[] attackList;
    public GameObject target;
    public Transform spawnPoint;
    bool sent = false;

    public GameObject currentAttack;
    // Update is called once per frame

    void Start()
    {
        if (attackList.Length > 0)
        {
            currentAttack = attackList[0];
        }
        
    }
    void Update()
    {
        if (!sent)
        {
            if (target != null)
            {

                var newProjectile = Instantiate(currentAttack, spawnPoint.transform.position, Quaternion.identity);
                newProjectile.SendMessage("SetTarget", target);
                sent = true;
            }
        }
    }

    public void AttackLoopSetPlayerInRange(GameObject player)
    {
        target = player;
    }
    public void AttackLoopPlayerOutOfRange(GameObject player)
    {

    }
    public void PhysicalAttackHits()
    {

    }
}
