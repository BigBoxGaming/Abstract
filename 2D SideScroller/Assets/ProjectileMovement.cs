using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float projectileSpeed;
    private Rigidbody2D projectileRB;

    public Vector3 mousePosition;
    public float timeUntilMovementStops;
    public float timer;
    private float playerScale;

    private bool facingRight;
   
    
    
    // Start is called before the first frame update
    void Start()
    {
        projectileRB = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
        var step = projectileSpeed * Time.deltaTime;
        timer += Time.deltaTime;
       
        if (timer < timeUntilMovementStops)
        {
            if (facingRight)
            {
                projectileRB.velocity = transform.right * projectileSpeed;
            }
            else
            {
                projectileRB.velocity = transform.right * -projectileSpeed;
            }
        }
        else
        {
            projectileRB.velocity = Vector2.zero;
        }
    }

    public void FacingRight()
    {
        facingRight = true;
    }
    public void NotFacingRight()
    {
        facingRight = false;
    }
}
