
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using static UnityEngine.ParticleSystem;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public Transform ballTransform;
    [HideInInspector]
    public Rigidbody2D enemyRigidbody;
    private ParticleSystem destroyEffect;

    [SerializeField]
    private SpriteAtlas spriteAtlas;
    private SpriteRenderer spriteRenderer;

    [HideInInspector]
    public int enemyLevel;
    [HideInInspector]
    public int enemyHP;
    [HideInInspector]
    public Vector2 indexOfThisEnemy;
    private List<Vector2> nearEnemyIndexes;
    private Vector2 positionOfThisEnenmy;

    private string enemyLevelString; // to cache the level of enemy 

    private string EnemyLevel1 = "1";
    private string EnemyLevel2 = "2";
    private string EnemyLevel21 = "21"; //enenmy level 1 with one hit
    private string EnemyLevel3 = "3";
    private string EnemyLevel31 = "31"; //enenmy level 2 with one hit
    private string EnemyLevel32 = "32";//enenmy level 2 with two hits

    [HideInInspector]
    public GameObject ObjectPulled;
    [HideInInspector]
    public List<GameObject> ObjectPulledList;

    private Color colorOfEnemy;

    private void OnEnable()
    {
        spriteRenderer =GetComponent<SpriteRenderer>();
        enemyHP = enemyLevel;
        enemyLevelString = enemyLevel.ToString();
        ChengeTheSpriteOfEnemy(enemyLevelString);
        if (enemyLevel == 1) colorOfEnemy = new Color(0.2f,0.2f,0.2f,1); //dark enemy Color
        if (enemyLevel == 2) colorOfEnemy = new Color(0, 0.4f, 0.65f, 1); //blue enemy Color
        if (enemyLevel == 3) colorOfEnemy = new Color(0.65f, 0, 0.1f, 1); //red enemy Color
        nearEnemyIndexes = new List<Vector2>();
        positionOfThisEnenmy = transform.position;
    }


    public void ChengeTheSpriteOfEnemy(string newSprite)
    {
        spriteRenderer.sprite = spriteAtlas.GetSprite(newSprite);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ball>(out Ball ball))
        {
            if (ball.megaBallEffect.isPlaying)
            {
                ball.megaBallEffect.Clear();
                ball.megaBallEffect.Stop();
                megaHitProcessing();
                ObjectPulledList = ObjectPuller.current.GetballBurstEffectPullList();
                ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
                ObjectPulled.transform.position = positionOfThisEnenmy;
                ObjectPulled.SetActive(true);
            }
            reduceHPOfEnenmy(false);
        }
    }

    public void attackWithBall() {
        ObjectPulledList = ObjectPuller.current.GetEnemyBallPullList();
        ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
        ObjectPulled.transform.position = positionOfThisEnenmy;
        ObjectPulled.GetComponent<TrailRenderer>().Clear();
        ObjectPulled.SetActive(true);
        float xAxisVelocity = Random.Range(0, 2) == 0 ? 1 : -1;
        ObjectPulled.GetComponent<Rigidbody2D>().AddForce(new Vector2(xAxisVelocity, -1) * GameManager.current.ballMoveSpeed, ForceMode2D.Impulse);

        //second throw can make only 3-rd level enemy and towards platform
        if (enemyLevel > 2) {
            ObjectPulledList = ObjectPuller.current.GetEnemyBallPullList();
            ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
            ObjectPulled.transform.position = positionOfThisEnenmy;
            ObjectPulled.GetComponent<TrailRenderer>().Clear();
            ObjectPulled.SetActive(true);
            ObjectPulled.GetComponent<Rigidbody2D>().AddForce(((Vector2)GameManager.current.platform.platformTransform.position - positionOfThisEnenmy).normalized * GameManager.current.ballMoveSpeed, ForceMode2D.Impulse);
        }
    }

    public void reduceHPOfEnenmy(bool megaHit)
    {
        enemyHP--;
        GameManager.current.enemiesDestroyedInOneAir++;
        GameManager.current.incrementScoreBasis();
        GameManager.current.countTheScore();
        if (megaHit)
        {
            ObjectPulledList = ObjectPuller.current.GetballBurstEffectPullList();
            ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
            ObjectPulled.transform.position = transform.position;
            ObjectPulled.SetActive(true);
        }
        if (enemyHP < 1) disactivateEnemy(); //argument is for using with backToMenu function
        else
        {
            ChengeTheSpriteOfEnemy(enemyLevelString + (enemyLevel - enemyHP).ToString());
        }
    }

    private void megaHitProcessing() {
        foreach (Vector2 index in nearEnemyIndexes) 
            if (GameManager.current.allEnemiesWithPositionIndexes.ContainsKey(index)&&GameManager.current.allEnemiesWithPositionIndexes[index].isActiveAndEnabled) GameManager.current.allEnemiesWithPositionIndexes[index].reduceHPOfEnenmy(true);
    }

    public void fixNearEnemiesInList() {
        Vector2 indexShif = Vector2.right;
        for (int i = 0; i < 4; i++)
        {
            if (i > 1) indexShif = Vector2.up;
            if (GameManager.current.allEnemiesWithPositionIndexes.ContainsKey(indexOfThisEnemy + indexShif)) nearEnemyIndexes.Add(indexOfThisEnemy + indexShif);
            indexShif *= -1;
        }
    }

    private void disactivateEnemy()
    {
        GameManager.current.allEnemiesWithPositionIndexes.Remove(indexOfThisEnemy);
        if (enemyLevel == 2) GameManager.current.all2LevelEnemies.Remove(this);
        else if (enemyLevel == 3) GameManager.current.all3LevelEnemies.Remove(this);
        GameManager.current.checkIfWin();
        nearEnemyIndexes.Clear();
        gameObject.SetActive(false);
        ObjectPulledList = ObjectPuller.current.GetDestroyEffectPullList();
        ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
        ObjectPulled.transform.position = transform.position;
        MainModule main = ObjectPulled.GetComponent<ParticleSystem>().main;
        main.startColor = colorOfEnemy;
        ObjectPulled.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
