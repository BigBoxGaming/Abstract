using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : Attack
{
    
    Rigidbody2D projectileRB;

    public int numberOfProjectiles;
    public float projectileSpeed;
    public float areaDamagePerSec;
    Vector3 targetPosition;
    //Selections for Easy Monster Creation
    public bool isHoming;
    public bool isBullet;
    public bool isShootAndStop;
    public bool hasAreaEffectDamage;


    // Start is called before the first frame update
    void Start()
    {
        projectileRB = gameObject.GetComponent<Rigidbody2D>();
        targetPosition = target.transform.position;
        Debug.Log(targetPosition);
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
            //transform.position = Vector2.MoveTowards(transform.position, targetPosition, projectileSpeed * Time.deltaTime);
            projectileRB.AddForce(Vector2.one);
           
        }
    }
}
