using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothTime = 15.0f;
    private Vector3 velocity = Vector3.zero;

    public float time = 0;
    public float timer = 2.0f;
    public float increaseRotation = 1;
    public float decreaseRotation = -1;
    public bool rotate = false;
    public bool rightWallWalking;
    public bool leftWallWalking;
    public bool ceilingWalking;
    public bool groundWalking;
    public float currentRotation;
    public bool grounded;
    public float startRotationDegree;

    // Start is called before the first frame update
    void Start()
    {
        currentRotation = gameObject.transform.eulerAngles.z;
    }

    // Update is called once per framed
    void Update()
    {
        var playerRotationDegrees = target.transform.eulerAngles.z;
        var cameraRotationDegrees = gameObject.transform.eulerAngles.z;

        if (grounded)
        {
            if (playerRotationDegrees == cameraRotationDegrees)
            {
                startRotationDegree = cameraRotationDegrees;
                currentRotation = playerRotationDegrees;
            }

            if (startRotationDegree == 0 && playerRotationDegrees == 270)
            {
                if (cameraRotationDegrees != playerRotationDegrees)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, --currentRotation);

                }
            }
            else if (startRotationDegree == 0 && playerRotationDegrees == 90)
            {
                if (cameraRotationDegrees != playerRotationDegrees)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, ++currentRotation);

                }
            }
            else if (startRotationDegree == 90 && playerRotationDegrees == 0)
            {
                if (cameraRotationDegrees != playerRotationDegrees)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, --currentRotation);

                }
            }
            else if (startRotationDegree == 90 && playerRotationDegrees == 180)
            {
                if (cameraRotationDegrees != playerRotationDegrees)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, ++currentRotation);

                }
            }
            else if (startRotationDegree == 180 && playerRotationDegrees == 90)
            {
                if (cameraRotationDegrees != playerRotationDegrees)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, --currentRotation);

                }
            }
            else if (startRotationDegree == 180 && playerRotationDegrees == 270)
            {
                if (cameraRotationDegrees != playerRotationDegrees)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, ++currentRotation);

                }
            }
            else if (startRotationDegree == 270 && playerRotationDegrees == 180)
            {
                if (cameraRotationDegrees != playerRotationDegrees)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, --currentRotation);

                }
            }
            else if (startRotationDegree == 270 && playerRotationDegrees == 0)
            {
                if (cameraRotationDegrees != playerRotationDegrees)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, ++currentRotation);

                }
            }

            if (currentRotation % 360 == 0)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            #region Trippy Camera
            //if (playerRotationDegrees % 90 == 0)
            //{
            //    if (playerRotationDegrees - cameraRotationDegrees > 0)
            //    {  
            //        if(cameraRotationDegrees != playerRotationDegrees)
            //        {

            //                gameObject.transform.rotation = Quaternion.Euler(0, 0, ++currentRotation);
            //                Debug.Log("Increasing Rotation");
            //        }
            //    }
            //    else if(playerRotationDegrees - cameraRotationDegrees < 0)
            //    {

            //        if (cameraRotationDegrees != playerRotationDegrees)
            //        {
            //            gameObject.transform.rotation = Quaternion.Euler(0, 0, --currentRotation);
            //            Debug.Log("Decreasing Rotation");
            //        }
            //    }
            //}
            #endregion
        }

        time += Time.deltaTime;
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 5, -10));

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        #region Trippy Camera 2
        //Strange camera effect Polish this and use for later. Slightly changing the rotation of the camera to match the player. Was tring to just change the y axis

        //if (time > timer)
        //{
        //    if (gameObject.transform.rotation.z < target.rotation.z)
        //    {
        //        gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.x, gameObject.transform.position.y, gameObject.transform.rotation.z + 1);
        //        time = 0f;
        //    }
        //    else
        //    {
        //        gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.x, gameObject.transform.position.y, gameObject.transform.rotation.z -1);
        //        time = 0f;
        //    }
        //}
        #endregion
    }

    public void SetGrounded()
    {
        grounded = true;
    }

    public void NotGrounded()
    {
        grounded = false;
    }
}