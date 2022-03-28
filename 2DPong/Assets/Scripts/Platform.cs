using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Platform : MonoBehaviour
{
    Camera cameraOfGame;
    [HideInInspector]
    public Transform platformTransform;

    private float yPositionOfPlatform;

    public float leftBorderForPlatform;
    public float rightBorderForPlatform;
    private float ballHitPlatformXPoint;
    [HideInInspector]
    public GameManager gameManager;
    [HideInInspector]
    public ParticleSystem megaBallEffect;
    private MainModule main;
    [HideInInspector]
    public bool isParalized;

    private int enemyAttackGap;

    private void OnEnable()
    {
        if (cameraOfGame==null) cameraOfGame = Camera.main;
        if (platformTransform==null) platformTransform = transform;
        yPositionOfPlatform = platformTransform.position.y;
        if (megaBallEffect == null) megaBallEffect = GetComponent<ParticleSystem>();
        main = megaBallEffect.main;
    }

    public void disactivatePlatorm()
    {
        isParalized = false;
        StopCoroutine(paralisedTime());
        megaBallEffect.Clear();
        megaBallEffect.Stop();
        gameObject.SetActive(false);
        enemyAttackGap = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ball>(out Ball ball)) {

            //transfer mega ball effect to the ball
            if (megaBallEffect.isPlaying && !isParalized)
            {
                ball.megaBallEffect.Play();
                megaBallEffect.Stop();
            }
                gameManager.resetScoreBasis();


            enemyAttackGap++;
            if (enemyAttackGap > 2)
            {
                enemyAttackGap = 0;
                //each time player ball hits the platform, the enemy attacks
                gameManager.enemyAttacks();
            }

            //this value will determine if ball will move left or right depending to wich side of platform it hits
            //so player can manage the ball bounce direction
            ballHitPlatformXPoint = ball.ballTransform.position.x - platformTransform.position.x;
            Vector2 velosityOfBall = ball.ballRigidbody.velocity;
            ball.ballRigidbody.velocity = Vector2.zero;
            ball.ballRigidbody.AddForce(new Vector2(ballHitPlatformXPoint, 1).normalized * velosityOfBall.magnitude, ForceMode2D.Impulse);
            ball.rotationSpeed = gameManager.ballRotationSpeed;
        }
        else if (collision.gameObject.TryGetComponent<EnemyBall>(out EnemyBall enenmyBall))
        {
            if (!isParalized)
            {
                enenmyBall.gameObject.SetActive(false);
                StartCoroutine(paralisedTime());
            }
        }
    }

    IEnumerator paralisedTime()
    {
        isParalized = true;
        main.startColor = Color.blue;
        megaBallEffect.Play();
        yield return new WaitForSeconds(3);
        megaBallEffect.Clear();
        megaBallEffect.Stop();
        isParalized = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<MegaBonus>(out MegaBonus megaBonus))
        {
            main.startColor = Color.yellow;
            megaBallEffect.Play();
            megaBonus.gameObject.SetActive(false);
        }
    }



    private void Update()
    {
        if (Input.GetMouseButton(0) && !gameManager.winLosePanel.activeInHierarchy && !isParalized)
        {
            //moving the platform with touch/mouse position if it is held down/touched
            platformTransform.position = new Vector3(cameraOfGame.ScreenToWorldPoint(Input.mousePosition).x, yPositionOfPlatform, 0);

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
                gameManager.gameIsOn = true;
                gameManager.startTheGame();
            }
        }
    }

}
