using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matttestscript : MonoBehaviour
{
    public float MovementSpeed;
    public float TransitionTime;
    public Rigidbody2D _platformRigidbody;
    public float _time;
    public bool _isMovingRight;


    // Start is called before the first frame update
    void Start()
    {
        _platformRigidbody = gameObject.GetComponent<Rigidbody2D>();
        _isMovingRight = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _time += Time.deltaTime;

        if (_time >= TransitionTime)
        {
            _time = 0;
            _isMovingRight = !_isMovingRight;
            _platformRigidbody.velocity = Vector2.zero;
                
        }

            if (_isMovingRight)
            {
                _platformRigidbody.AddForce(Vector2.right * MovementSpeed);
            }
            else
            {
                _platformRigidbody.AddForce(Vector2.left * MovementSpeed);
            }


        


    }
}
