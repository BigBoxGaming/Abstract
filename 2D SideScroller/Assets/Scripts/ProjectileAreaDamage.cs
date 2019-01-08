using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAreaDamage : MonoBehaviour
{
    public float projectileDamagePerSecond;
    // Start is called before the first frame update

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("EnemyHitBox"))
        { 
            other.SendMessageUpwards("TakeDamage", projectileDamagePerSecond);
        }
    }
}
