using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D enemyRigid;
    public bool inRange;
    public bool isClosestDistanceToPlayer;
    public float enemyClosestPositionToPlayer;
    public float jumpForce;
    public bool enemyShouldJumpNearPlayer;
    public GameObject player;
    public float movementSpeed;
    public float timeBetweenJumps;
    private float timer = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
        enemyRigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float step = movementSpeed * Time.deltaTime;
        if (inRange && Vector2.Distance(transform.position, player.transform.position) > enemyClosestPositionToPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);

        }
        if (enemyShouldJumpNearPlayer)
        {
            if (inRange && Vector2.Distance(transform.position, player.transform.position) <= enemyClosestPositionToPlayer)
            {
                if (timer >= timeBetweenJumps)
                {
                    enemyRigid.velocity = new Vector2(0, jumpForce);
                    timer = 0;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D newPlayer)
    {
        if (newPlayer.tag == "Player")
        {
            player = newPlayer.gameObject;
            inRange = true;
            isClosestDistanceToPlayer = false;
            Debug.Log("entered");
            this.gameObject.SendMessage("TogglePlayerInRange");
        }
    }
    private void OnTriggerExit2D(Collider2D newPlayer)
    {
        if (newPlayer.tag == "Player")
        {
            player = null;
            inRange = false;
            SendMessage("TogglePlayerInRange");

        }
    }
}

