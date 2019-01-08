using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{

    public float health;
    public float level;
    public bool inRange = false;
    public GameObject enemyHealthBar;
    public bool hasBossHealthBar;
    private Slider slider;
    public GameObject bloodEffect;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (hasBossHealthBar)
        {
            slider = enemyHealthBar.GetComponent<Slider>();
            slider.maxValue = health;
            slider.value = health;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            if (bloodEffect != null)
            {
                Instantiate(bloodEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }
            Destroy(gameObject);
            
        }
    }
    void OnTrigger2DEnter(Collider2D other)
    {
        Debug.Log("");
    }

    public void OnCollider2DEnter(Collider2D other)
    {
        float attackStrength = 5;
        Debug.Log("Trigger entered");

        if(other.tag == "Attack")
        {
            TakeDamage(attackStrength);
        }
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (slider != null)
        {
            slider.value = health;
        }
        
        
    }
    public void TogglePlayerInRange()
    {
        inRange = !inRange;
        if(inRange)
        {
            if (enemyHealthBar != null)
            {
                enemyHealthBar.SetActive(true);
            }
        }
        else
        {
            if (enemyHealthBar != null)
            {
                enemyHealthBar.SetActive(false);
            }
        }
    }
}
