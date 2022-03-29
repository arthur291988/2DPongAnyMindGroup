
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [HideInInspector]
    public Transform ballTransform;
    [HideInInspector]
    public Rigidbody2D ballRigidbody;
    [HideInInspector]
    public GameManager gameManager;

    [HideInInspector]
    public float startImpulseOfBall;
    [HideInInspector]
    public float rotationSpeed;

    private float trapCheckTimer;
    private const float TRAP_CHECK_TIMER_LIMIT = 5;
    //is used to release the ball from horisontal trap
    private const float MIN_VERTICAL_ANGLE = 1.4f;

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
        trapCheckTimer = TRAP_CHECK_TIMER_LIMIT;
        rotationSpeed = 0; //on start the ball does not rotate
        startImpulseOfBall = gameManager.ballMoveSpeed;
        ballTransform = transform;
        ballRigidbody = GetComponent<Rigidbody2D>();
        if (ballTrail == null) ballTrail = GetComponent<TrailRenderer>();
        if (megaBallEffect == null) megaBallEffect = GetComponent<ParticleSystem>();
        //ball is steady on enable, before it gets the command to start
        ballRigidbody.bodyType = RigidbodyType2D.Kinematic;
    }

    public void disactivateTheBall(bool win)
    {
        ballBurstEffect();
        ballRigidbody.bodyType = RigidbodyType2D.Kinematic;
        gameObject.SetActive(false);
        gameManager.gameIsOn = false;
        if (!win)
        {
            gameManager.megaBallSound.Play();
            gameManager.reduceAvailableBallsAndCheckTheLost();
        }
        if (megaBallEffect.isPlaying) megaBallEffect.Stop();
        if (gameManager.platform.megaBallEffect.isPlaying)
        {
            gameManager.platform.megaBallEffect.Stop();
            if (gameManager.platform.isParalized) gameManager.platform.isParalized = false;
        }
    }

    public void startTheBall()
    {
        gameManager.StartCoroutine(gameManager.startTheMegaBallTimer());
        ballRigidbody.bodyType = RigidbodyType2D.Dynamic;
        ballRigidbody.AddForce(new Vector2(Random.Range(-0.3f, 0.3f), 1) * startImpulseOfBall, ForceMode2D.Impulse);
        rotationSpeed = gameManager.ballRotationSpeed;
    }

    private void breakeHorizontalTrapAndVelocitySlowDown()
    {
        ballBurstEffect();
        float yAxisVelocity = Random.Range(0, 2) == 0 ? 1 : -1;
        ballRigidbody.velocity = Vector2.zero; //clear velocity to prevent double impulse
        ballRigidbody.AddForce(new Vector2(Random.Range(-0.3f, 0.3f), yAxisVelocity) * startImpulseOfBall, ForceMode2D.Impulse);
        rotationSpeed = gameManager.ballRotationSpeed;
        gameManager.megaBallSound.Play();
        trapCheckTimer = 5;
    }

    private void ballBurstEffect()
    {
        ObjectPulledList = ObjectPuller.current.GetballBurstEffectPullList();
        ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
        ObjectPulled.transform.position = ballTransform.position;
        ObjectPulled.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyBall>(out EnemyBall enenmyBall))
        {
            gameManager.surikenSound.Play();
            if (megaBallEffect.isPlaying)
            {
                megaBallEffect.Clear();
                megaBallEffect.Stop();
            }
        }
    }

    private void FixedUpdate()
    {
        if (gameManager.gameIsOn) ballRigidbody.MoveRotation(ballRigidbody.rotation - rotationSpeed * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        //rotation speed of ball decreases with time 
        if (rotationSpeed > 500) rotationSpeed = Mathf.Lerp(rotationSpeed, 500, 0.003f);

        //monitoring the velocity reduce of ball or horisontal trap
        if (gameManager.gameIsOn && ((ballRigidbody.velocity.y < MIN_VERTICAL_ANGLE && ballRigidbody.velocity.y > -MIN_VERTICAL_ANGLE) || ballRigidbody.velocity.magnitude < startImpulseOfBall))
        {
            trapCheckTimer -= Time.deltaTime;
            if (trapCheckTimer < 0)
            {
                breakeHorizontalTrapAndVelocitySlowDown();
            }
        }
        else if (trapCheckTimer < 5) trapCheckTimer = 5;


        if (ballTransform.position.y <= -gameManager.vertScreenSize / 2 && gameManager.gameIsOn) disactivateTheBall(false);
    }
}
