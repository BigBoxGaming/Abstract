using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _myRB;
    private Transform _playerTransform;

    public float speed = 50;

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
    public float playerCurrentFlipValue;

    public Camera mainCamera;
    public float currentRotation;

    void Start()
    {
        _myRB = GetComponent<Rigidbody2D>();
        playerCurrentFlipValue = transform.rotation.y;
        _playerTransform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 leftMovement = new Vector2(-speed, 0);
        Vector2 rightMovement = new Vector2(speed, 0);
        Vector2 jumpMovement = new Vector2(0, jumpForce);
        Vector2 jumpLeft = new Vector2(-speed, jumpForce);
        Vector2 jumpRight = new Vector2(speed, jumpForce);
        var _playerTransform = gameObject.GetComponent<Transform>();

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, ground);
        isRightWallWalking = Physics2D.OverlapCircle(groundCheck.position, checkRadius, rightWallCheckLayer);
        isLeftWallWalking = Physics2D.OverlapCircle(groundCheck.position, checkRadius, leftWallCheckLayer);
        isCeilingWalking = Physics2D.OverlapCircle(groundCheck.position, checkRadius, ceilingCheckLayer);

        timer += Time.deltaTime;



        //Allows player to rotate themselves in the air
        if (Input.GetKey(rotateLeft) && !(isLeftWallWalking || isCeilingWalking || isGrounded || isRightWallWalking))
        {
            _playerTransform.rotation = Quaternion.Euler(0, 0, currentRotation -= 5);
        }
        else if (Input.GetKey(rotateRight) && !(isLeftWallWalking || isCeilingWalking || isGrounded || isRightWallWalking))
        {
            _playerTransform.rotation = Quaternion.Euler(0, 0, currentRotation += 5);
        }


        //Helps with player camera rotation
        if (isRightWallWalking || isLeftWallWalking || isCeilingWalking || isGrounded)
        {
            mainCamera.SendMessage("SetGrounded");
        }
        else if (!(isRightWallWalking || isLeftWallWalking || isCeilingWalking || isGrounded))
        {
            mainCamera.SendMessage("NotGrounded");
        }

        if (timer > waitTime)
        {
            if (isRightWallWalking && !(isLeftWallWalking || isCeilingWalking || isGrounded))
            {
                Debug.Log("changing gravity Right Wall");

                Physics2D.gravity = new Vector2(-9.91F, 0);

                _playerTransform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, -90f);
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
            else if (isLeftWallWalking && !(isRightWallWalking || isCeilingWalking || isGrounded))
            {
                Debug.Log("changing gravity Left Wall");
                Physics2D.gravity = new Vector2(9.91F, 0);

                _playerTransform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 90f);
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
            else if (isCeilingWalking && !(isRightWallWalking || isLeftWallWalking || isGrounded))
            {
                Debug.Log("changing gravity ceiling");
                Physics2D.gravity = new Vector2(0, 9.91F);

                _playerTransform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 180f);
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
            else if (isGrounded && !(isRightWallWalking || isCeilingWalking || isLeftWallWalking))
            {
                Debug.Log("changing gravity ground");
                Physics2D.gravity = new Vector2(0, -9.81f);
                _playerTransform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
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
            if (!goingLeft || transform.localScale.x != 1)
            {
                FlipPlayerSprite();
                goingLeft = true;
            }
            _myRB.velocity = leftMovement;

            if (!goingLeft)
            {
                goingLeft = true;
                FlipPlayerSprite();
            }

            _myRB.velocity = leftMovement;
        }
        else if (Input.GetKey(right) && (isGrounded || isRightWallWalking || isLeftWallWalking || isCeilingWalking))
        {
            goingLeft = false;

            if (!goingRight || transform.localScale.x != -1)
            {

                FlipPlayerSprite();
                goingRight = true;

            }

            _myRB.velocity = rightMovement;

            
            goingRight = true;
        

        _myRB.velocity = rightMovement;
    }

            if (Input.GetKey(up) && (isGrounded || isRightWallWalking || isLeftWallWalking || isCeilingWalking))
            {
                if (goingLeft)
                {
                    goingUp = true;
                    _myRB.velocity = jumpLeft;
                }
                else if (goingRight)
                {
                    goingUp = true;
                    _myRB.velocity = jumpRight;
                }
                else
                {
                    goingUp = true;

                    _myRB.velocity = jumpMovement;
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


