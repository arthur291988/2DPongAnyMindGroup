using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Platform : MonoBehaviour
{
    Camera cameraOfGame;
    [HideInInspector]
    public Transform platformTransform;
    [HideInInspector]
    public GameManager gameManager;

    private float yPositionOfPlatform;
    private float PLATFORM_RIB_LIMIT_FROM_Y_AXIS = 0.3f;

    public float leftBorderForPlatform;
    public float rightBorderForPlatform;

    private float ballHitPlatformXPoint;
    
    //to set manually in inspector
    [SerializeField]
    private bool isSmallPlatform;

    [HideInInspector]
    public ParticleSystem megaBallEffect;
    private MainModule main;

    [HideInInspector]
    public bool isParalized;
    [HideInInspector]
    public bool isSlow;

    private byte enemyAttackStep;
    private const byte ENEMY_ATTACK_STEP_LIMIT_ONE = 2;
    private const byte ENEMY_ATTACK_STEP_LIMIT_TWO = 3;

    private void OnEnable()
    {
        if (cameraOfGame==null) cameraOfGame = Camera.main;
        if (platformTransform==null) platformTransform = transform;
        yPositionOfPlatform = platformTransform.position.y;
        if (megaBallEffect == null) megaBallEffect = GetComponent<ParticleSystem>();
        main = megaBallEffect.main;
        if (isSmallPlatform) StartCoroutine(disactivateSmallPlatformTime());
    }

    public void disactivatePlatorm()
    {
        isParalized = false;
        isSlow = false;
        StopCoroutine(paralisedTime());
        StopCoroutine(slowTime());
        megaBallEffect.Clear();
        megaBallEffect.Stop();
        gameObject.SetActive(false);
        enemyAttackStep = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ball>(out Ball ball)) {

            if (ball.thisGO.layer != 6)
            {
                gameManager.surikenSound.Play();
                //transfer mega ball effect to the ball
                if (megaBallEffect.isPlaying && !isParalized && !isSlow)
                {
                    ball.megaBallEffect.Play();
                    megaBallEffect.Stop();
                }
            }
            else {
                ball.thisGO.layer = 0;
                ball.ballSpriteRenderer.color = Color.white;
                if (!isParalized && !isSlow)
                {
                    gameManager.slowDownSound.Play();
                    StartCoroutine(slowTime());
                    if (megaBallEffect.isPlaying)
                    {
                        megaBallEffect.Stop();
                    }
                }
            }
            gameManager.resetScoreBasis();


            enemyAttackStep++;
            if (enemyAttackStep == ENEMY_ATTACK_STEP_LIMIT_ONE)
            {
                //each time player ball hits the platform, the enemy attacks
                gameManager.enemyAttacks(enemyAttackStep);
            }
            else if (enemyAttackStep == ENEMY_ATTACK_STEP_LIMIT_TWO)
            {
                //each time player ball hits the platform, the enemy attacks
                gameManager.enemyAttacks(enemyAttackStep);
                enemyAttackStep = 0;
            }

            //this value will determine if ball will move left or right depending to wich side of platform it hits
            //so player can manage the ball bounce direction
            ballHitPlatformXPoint = ball.ballTransform.position.x - platformTransform.position.x;
            Vector2 velocityOfBall = ball.ballRigidbody.velocity;

            //condition to check if ball is not hitting the rib/edge of platform, cause in that case velocity of it should not be managable and must follow the engine physics
            if (ball.ballTransform.position.y> yPositionOfPlatform+ PLATFORM_RIB_LIMIT_FROM_Y_AXIS)
            {
                //if ball moves slower than start impulse it will get start impulse after hit the platform
                ball.ballRigidbody.velocity = Vector2.zero;
                if (velocityOfBall.magnitude < ball.startImpulseOfBall) ball.ballRigidbody.AddForce(new Vector2(ballHitPlatformXPoint, 1).normalized * ball.startImpulseOfBall, ForceMode2D.Impulse);
                else ball.ballRigidbody.AddForce(new Vector2(ballHitPlatformXPoint, 1).normalized * velocityOfBall.magnitude, ForceMode2D.Impulse);
            }

            ball.rotationSpeed = gameManager.ballRotationSpeed;
        }
        else if (collision.gameObject.TryGetComponent<EnemyBall>(out EnemyBall enenmyBall))
        {
            enenmyBall.gameObject.SetActive(false);
            if (enenmyBall.ballLevel == 0 && !isSmallPlatform)
            {
                gameManager.surikenSound.Play();
                gameManager.platformSmall.enemyAttackStep = gameManager.platform.enemyAttackStep;
                disactivatePlatorm();
                gameManager.platform = gameManager.platformSmall;
                gameManager.platform.transform.position = platformTransform.position;
                gameManager.platform.gameObject.SetActive(true);
            }
            else if (enenmyBall.ballLevel == 1)
            {
                if (!isParalized && !isSlow)
                {
                    gameManager.slowDownSound.Play();
                    StartCoroutine(slowTime());
                }
            }
            else
            {
                if (!isParalized)
                {
                    gameManager.iceEffectSound.Play();
                    StartCoroutine(paralisedTime());
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<MegaBonus>(out MegaBonus megaBonus))
        {
            main.startColor = Color.yellow;
            megaBallEffect.Play();
            gameManager.megaBallSound.Play();
            megaBonus.gameObject.SetActive(false);
        }
    }

    private IEnumerator paralisedTime()
    {
        isParalized = true;
        main.startColor = Color.blue;
        megaBallEffect.Play();
        yield return new WaitForSeconds(3);
        megaBallEffect.Clear();
        megaBallEffect.Stop();
        isParalized = false;
    }
    private IEnumerator slowTime()
    {
        isSlow = true;
        main.startColor = Color.white;
        megaBallEffect.Play();
        yield return new WaitForSeconds(3);
        megaBallEffect.Clear();
        megaBallEffect.Stop();
        isSlow = false;
    }
    private IEnumerator disactivateSmallPlatformTime()
    {
        yield return new WaitForSeconds(7);
        gameManager.platformBig.enemyAttackStep = gameManager.platform.enemyAttackStep;
        disactivatePlatorm();
        gameManager.platform = gameManager.platformBig;
        gameManager.platform.platformTransform.position = platformTransform.position;
        gameManager.platform.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !gameManager.winLosePanel.activeInHierarchy && !isParalized)
        {
            //moving the platform with touch/mouse position if it is held down/touched
            if (isSlow) platformTransform.position = Vector2.Lerp(platformTransform.position, new Vector2(cameraOfGame.ScreenToWorldPoint(Input.mousePosition).x, yPositionOfPlatform), 0.01f); 
            else platformTransform.position = new Vector3(cameraOfGame.ScreenToWorldPoint(Input.mousePosition).x, yPositionOfPlatform, 0);

            //holding the platform inside the screen borders
            if (platformTransform.position.x < leftBorderForPlatform) platformTransform.position = new Vector2(leftBorderForPlatform, yPositionOfPlatform);
            if (platformTransform.position.x > rightBorderForPlatform) platformTransform.position = new Vector2(rightBorderForPlatform, yPositionOfPlatform);

            if (!gameManager.gameIsOn ) gameManager.ball.ballTransform.position = new Vector2(platformTransform.position.x, platformTransform.position.y + platformTransform.localScale.y / 3);
        }
        if (Input.GetMouseButtonDown(0) && !gameManager.winLosePanel.activeInHierarchy)
        {
            //if game is not started, it will here
            if (!gameManager.gameIsOn )
            {
                gameManager.surikenSound.Play();
                gameManager.gameIsOn = true;
                gameManager.ball.startTheBall();
            }
        }
    }

}
