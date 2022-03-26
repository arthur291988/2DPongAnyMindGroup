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

    private void OnEnable()
    {
        startImpulseOfBall = 10;
        ballTransform = transform;
        ballRigidbody = GetComponent<Rigidbody2D>();

        //ball is steady on enable while it gets the command
        ballRigidbody.bodyType = RigidbodyType2D.Kinematic;

    }

    public void startTheBall()
    {
        ballRigidbody.bodyType = RigidbodyType2D.Dynamic;
        ballRigidbody.AddForce(Vector2.up * startImpulseOfBall, ForceMode2D.Impulse);
    }

    //private void FixedUpdate()
    //{
    //    ballRigidbody.MoveRotation(Quaternion.Euler(0, 0, Time.fixedDeltaTime*2));
    //}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
