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

    private bool trapCheckInProcess;
    private float trapCheckTimer;

    //is used to release the ball from horisontal trap
    private const float MIN_VERTICAL_ANGLE = 1.3f;

    [HideInInspector]
    public GameObject ObjectPulled;
    [HideInInspector]
    public List<GameObject> ObjectPulledList;
    [HideInInspector]
    public TrailRenderer ballTrail;
    [HideInInspector]
    public ParticleSystem megaBallEffect;


    private void OnEnable()
    {
        trapCheckTimer = 5;
        trapCheckInProcess = false; 
        rotationSpeed = 0;
        startImpulseOfBall = gameManager.ballMoveSpeed; 
        ballTransform = transform;
        ballRigidbody = GetComponent<Rigidbody2D>();
        if (ballTrail == null) ballTrail = GetComponent<TrailRenderer>();
        if (megaBallEffect == null) megaBallEffect = GetComponent<ParticleSystem>();
        //ball is steady on enable while it gets the command
        ballRigidbody.bodyType = RigidbodyType2D.Kinematic;
    }

    public void disactivateTheBall(bool win)
    {
        ballBurstEffect();
        ballRigidbody.bodyType = RigidbodyType2D.Kinematic;
        gameObject.SetActive(false);
        gameManager.gameIsOn = false;
        if (!win) gameManager.reduceAvailableBalls();
        if (gameManager.platform.megaBallEffect.isPlaying) gameManager.platform.megaBallEffect.Stop();
    }

    public void startTheBall()
    {
        ballRigidbody.bodyType = RigidbodyType2D.Dynamic;
        ballRigidbody.AddForce(new Vector2(Random.Range(-0.3f,0.3f),1) * startImpulseOfBall, ForceMode2D.Impulse);
        rotationSpeed = gameManager.ballRotationSpeed;
    }

    private void breakeHorizontalTrap()
    {
        ballBurstEffect();
        float yAxisVelocity = Random.Range(0, 2) == 0 ? 1 : -1;
        ballRigidbody.velocity = Vector2.zero; //clear velocity to prevent double impulse
        ballRigidbody.AddForce(new Vector2(Random.Range(-0.3f, 0.3f), yAxisVelocity) * startImpulseOfBall, ForceMode2D.Impulse);
        rotationSpeed = gameManager.ballRotationSpeed;
        trapCheckTimer = 5;
    }

    private void ballBurstEffect() {
        ObjectPulledList = ObjectPuller.current.GetballBurstEffectPullList();
        ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
        ObjectPulled.transform.position = transform.position;
        ObjectPulled.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (gameManager.gameIsOn) ballRigidbody.MoveRotation(ballRigidbody.rotation - rotationSpeed * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        //rotation speed of ball decreases with time 
        if (rotationSpeed > 500) rotationSpeed = Mathf.Lerp(rotationSpeed, 500,0.003f);

        if (gameManager.gameIsOn && ballRigidbody.velocity.y < MIN_VERTICAL_ANGLE && ballRigidbody.velocity.y > -MIN_VERTICAL_ANGLE) {
            trapCheckTimer -= Time.deltaTime;
            if (trapCheckTimer < 0) {
                breakeHorizontalTrap();
            }
        }
        else if (trapCheckTimer < 5) trapCheckTimer = 5;


        if (ballTransform.position.y <= -gameManager.vertScreenSize / 2 && gameManager.gameIsOn) disactivateTheBall(false);
    }
}
