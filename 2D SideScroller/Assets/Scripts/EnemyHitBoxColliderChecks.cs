using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBoxColliderChecks : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {


    }
        void OnCollisionEnter2D(Collision2D newCollision)
        {
            Debug.Log("A collision is detected");
            if (newCollision.gameObject.tag == "Player")
            {
            Debug.Log("The player is detected");
                gameObject.SendMessage("PhysicalAttackHits", newCollision);
            }
        }
}
