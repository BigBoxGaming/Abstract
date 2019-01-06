using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class PlayerController : MonoBehaviour
{

    public float speed = 50;
    private Rigidbody2D myRB;

    //Input Variables
    public KeyCode interact;
    public KeyCode inventoryKey;
    public KeyCode left;
    public KeyCode right;
    public KeyCode down;
    public KeyCode up;
    public KeyCode rotateRight;
    public KeyCode rotateLeft;
    public float jumpForce;
    public Physics myPhysics;

    public bool goingLeft;
    public bool goingRight;
    public bool goingDown;
    public bool goingUp;
    public bool isGrounded;
    public bool isRightWallWalking;
    public bool isLeftWallWalking;
    public bool isCeilingWalking;
    public Physics globalPhysics;

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask ground;
    public LayerMask rightWallCheckLayer;
    public LayerMask leftWallCheckLayer;
    public LayerMask ceilingCheckLayer;

    public float waitTime = 1.0f;
    public float timer = 0.0f;
    public LayerMask currentLayerMask;
    public LayerMask newLayerMask;
    private Transform playerTransform;
    public float playerCurrentFlipValue;

    //Attacks
    

    public Camera mainCamera;
    public float currentRotation;
   

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        playerCurrentFlipValue = transform.rotation.y;
    }

    void Update()
    {
        

        timer += Time.deltaTime;
        
        playerTransform = gameObject.GetComponent<Transform>();

        //Allows player to rotate themselves in the air
        if (Input.GetKey(rotateLeft) && !(isLeftWallWalking || isCeilingWalking || isGrounded || isRightWallWalking))
        {
            
            playerTransform.rotation = Quaternion.Euler(0, 0, currentRotation -= 5);
        }
        if (Input.GetKey(rotateRight) && !(isLeftWallWalking || isCeilingWalking || isGrounded || isRightWallWalking))
        {
            
            playerTransform.rotation = Quaternion.Euler(0, 0, currentRotation += 5);
        }


        //Helps with player camera rotation
        if (isRightWallWalking || isLeftWallWalking || isCeilingWalking || isGrounded)
        {
            mainCamera.SendMessage("SetGrounded");
        }
        if (!(isRightWallWalking || isLeftWallWalking || isCeilingWalking || isGrounded))
        {
            mainCamera.SendMessage("NotGrounded");
        }


    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 leftMovement = new Vector2(-speed, 0);
        Vector2 rightMovement = new Vector2(speed, 0);
        Vector2 jumpMovement = new Vector2(0, jumpForce);
        Vector2 jumpLeft = new Vector2(-speed, jumpForce);
        Vector2 jumpRight = new Vector2(speed, jumpForce);
        var playerTransform = gameObject.GetComponent<Transform>();

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, ground);
        isRightWallWalking = Physics2D.OverlapCircle(groundCheck.position, checkRadius, rightWallCheckLayer);
        isLeftWallWalking = Physics2D.OverlapCircle(groundCheck.position, checkRadius, leftWallCheckLayer);
        isCeilingWalking = Physics2D.OverlapCircle(groundCheck.position, checkRadius, ceilingCheckLayer);

        
        if (timer > waitTime)
        {
            if (isRightWallWalking && !(isLeftWallWalking || isCeilingWalking || isGrounded))
            {
                Debug.Log("changing gravity Right Wall");

                Physics2D.gravity = new Vector2(-9.91F, 0);

              playerTransform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, -90f);
                leftMovement = new Vector2(0, speed);
                rightMovement = new Vector2(0, -speed);
                jumpMovement = new Vector2(jumpForce, 0);
                jumpLeft = new Vector2(jumpForce, speed);
                jumpRight = new Vector2(jumpForce, -speed);

                newLayerMask = rightWallCheckLayer;
                if (currentLayerMask != newLayerMask)
                {
                    currentLayerMask = rightWallCheckLayer;
                    timer = 0.0f;
                }
                




            }
            if (isLeftWallWalking && !(isRightWallWalking || isCeilingWalking || isGrounded))
            {
                Debug.Log("changing gravity Left Wall");
                Physics2D.gravity = new Vector2(9.91F, 0);

                playerTransform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 90f);
                leftMovement = new Vector2(0, -speed);
                rightMovement = new Vector2(0, speed);
                jumpMovement = new Vector2(-jumpForce, 0);
                jumpLeft = new Vector2(-jumpForce, -speed);
                jumpRight = new Vector2(-jumpForce, speed);

                newLayerMask = leftWallCheckLayer;
                if (currentLayerMask != newLayerMask)
                {
                    currentLayerMask = leftWallCheckLayer;
                    timer = 0.0f;
                }

                

              
            }
            if (isCeilingWalking && !(isRightWallWalking || isLeftWallWalking || isGrounded))
            {
                Debug.Log("changing gravity ceiling");
                Physics2D.gravity = new Vector2(0, 9.91F);

                playerTransform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 180f);
                leftMovement = new Vector2(speed, 0);
                rightMovement = new Vector2(-speed, 0);
                jumpMovement = new Vector2(0, -jumpForce);
                jumpLeft = new Vector2(speed, -jumpForce);
                jumpRight = new Vector2(-speed, -jumpForce);


                newLayerMask = ceilingCheckLayer;
                if (currentLayerMask != newLayerMask)
                {
                    currentLayerMask = ceilingCheckLayer;
                    timer = 0.0f;
                }
              
                
            }
            if (isGrounded && !(isRightWallWalking || isCeilingWalking || isLeftWallWalking))
            {
                Debug.Log("changing gravity ground");
                Physics2D.gravity = new Vector2(0, -9.81f);
               playerTransform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
                leftMovement = new Vector2(-speed, 0);
                rightMovement = new Vector2(speed, 0);
                jumpMovement = new Vector2(0, jumpForce);
                jumpLeft = new Vector2(-speed, jumpForce);
                jumpRight = new Vector2(speed, jumpForce);

                newLayerMask = ground;
                if (currentLayerMask != newLayerMask)
                {
                    currentLayerMask = ground;
                    timer = 0.0f;
                }
               
            }
            
        }
        //Grounded Controls
        if (Input.GetKey(left) && (isGrounded || isRightWallWalking || isLeftWallWalking || isCeilingWalking))
            {
            goingRight = false;
            if(!goingLeft)
            {
               
                
                
                goingLeft = true;
                FlipPlayerSprite();


            }
          
                myRB.velocity = leftMovement;
            }
        if (Input.GetKey(right) && (isGrounded || isRightWallWalking || isLeftWallWalking || isCeilingWalking))
            {

            goingLeft = false;

            if (!goingRight)
            {
                
                FlipPlayerSprite();
                goingRight = true;
            
            }
                myRB.velocity = rightMovement;
            }
        
            if (Input.GetKey(up) && (isGrounded || isRightWallWalking || isLeftWallWalking || isCeilingWalking))
            {
            if(goingLeft)
            {
                goingUp = true;
                myRB.velocity = jumpLeft;
            }
            else if(goingRight)
            {
                goingUp = true;
                myRB.velocity = jumpRight;
            }
            else
            {
                goingUp = true;
               
                myRB.velocity = jumpMovement; 
            }  
            
            }
          

    }
    
    void FlipPlayerSprite()
    {
        Debug.Log("flig is called");
        goingRight = !goingRight;
       

            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        
        
    }

}
