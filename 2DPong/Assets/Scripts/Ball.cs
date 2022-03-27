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
        trapCheckInProcess = false; 
        rotationSpeed = 0;
        startImpulseOfBall = 10; 
        ballTransform = transform;
        ballRigidbody = GetComponent<Rigidbody2D>();
        if (ballTrail == null) ballTrail = GetComponent<TrailRenderer>();
        if (megaBallEffect == null) megaBallEffect = GetComponent<ParticleSystem>();
        //ball is steady on enable while it gets the command
        ballRigidbody.bodyType = RigidbodyType2D.Kinematic;

    }

    public void startTheBall()
    {
        ballRigidbody.bodyType = RigidbodyType2D.Dynamic;
        ballRigidbody.AddForce(new Vector2(Random.Range(-0.3f,0.3f),1) * startImpulseOfBall, ForceMode2D.Impulse);
        rotationSpeed = GameManager.current.BALL_ROTATION_SPEED;
    }

    public void disactivateTheBall(bool win)
    {
        ballBurstEffect();
        ballRigidbody.bodyType = RigidbodyType2D.Kinematic; 
        gameObject.SetActive(false);
        gameManager.gameIsOn = false;
        if (!win) gameManager.reduceAvailableBalls();
    }

    //used to give breaking impulse to the ball so it can move in vertical direction if it was horizontal trap (when the Y axis velocity is very low or zero)
    private IEnumerator breakeHorizontalTrap() {
        trapCheckInProcess = true; //used to prevent multiple calls for check
        yield return new WaitForSeconds(3);
        ballRigidbody.velocity = Vector2.zero; //clear velocity to prevent double impulse
        if (ballRigidbody.velocity.y < 1.2f && ballRigidbody.velocity.y > -1.2f)
        {
            ballBurstEffect();
            float yAxisVelocity = Random.Range(0, 2) == 0 ? 1 : -1;
            ballRigidbody.AddForce(new Vector2(Random.Range(-0.3f, 0.3f), yAxisVelocity) * startImpulseOfBall, ForceMode2D.Impulse);
            rotationSpeed = GameManager.current.BALL_ROTATION_SPEED;
        }
        trapCheckInProcess = false;
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
        Debug.Log(ballRigidbody.velocity.y);
        //rotation speed of ball decreases with time 
        if (rotationSpeed > 500) rotationSpeed = Mathf.Lerp(rotationSpeed, 500,0.003f);

        if (gameManager.gameIsOn && ballRigidbody.velocity.y < 1.2f && ballRigidbody.velocity.y > -1.2f && !trapCheckInProcess) StartCoroutine(breakeHorizontalTrap());

        if (ballTransform.position.y <= -gameManager.vertScreenSize / 2 && gameManager.gameIsOn) disactivateTheBall(false);
    }
}
