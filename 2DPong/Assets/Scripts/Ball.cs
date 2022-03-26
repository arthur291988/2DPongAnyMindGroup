using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [HideInInspector]
    public Transform ballTransform;
    [HideInInspector]
    public Rigidbody2D ballRigidbody;

    [HideInInspector]
    public float startImpulseOfBall;

    [HideInInspector]
    public float rotationSpeed;
    [HideInInspector]
    public GameManager gameManager;



    private void OnEnable()
    {
        rotationSpeed = 0;
        startImpulseOfBall = 10;
        ballTransform = transform;
        ballRigidbody = GetComponent<Rigidbody2D>();

        //ball is steady on enable while it gets the command
        ballRigidbody.bodyType = RigidbodyType2D.Kinematic;

    }

    public void startTheBall()
    {
        ballRigidbody.bodyType = RigidbodyType2D.Dynamic;
        ballRigidbody.AddForce(new Vector2(Random.Range(-0.3f,0.3f),1) * startImpulseOfBall, ForceMode2D.Impulse);
        rotationSpeed = GameManager.BALL_ROTATION_SPEED;
    }

    private void FixedUpdate()
    {
        if (gameManager.gameIsOn) ballRigidbody.MoveRotation(ballRigidbody.rotation - rotationSpeed * Time.fixedDeltaTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotationSpeed > 500) rotationSpeed = Mathf.Lerp(rotationSpeed, 500,0.003f);
    }
}
