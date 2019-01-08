using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : Attack
{
    
    GameObject target;
    Rigidbody2D projectileRB;

    public int numberOfProjectiles;
    public float projectileSpeed;
    public bool isHoming;
    public bool isBullet;


    // Start is called before the first frame update
    void Start()
    {
        projectileRB = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isHoming)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, projectileSpeed * Time.deltaTime);
        }
        else if(isBullet)
        {

        }
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }
}
