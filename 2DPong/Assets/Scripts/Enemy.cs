
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
    private SpriteAtlas enemyTypesSprites;
    private SpriteRenderer enemySpriteRenderer;
    private Color colorOfEnemy;

    [HideInInspector]
    public int enemyLevel;
    [HideInInspector]
    public int enemyHP;
    [HideInInspector]
    public Vector2 indexOfThisEnemy;
    private List<Vector2> nearEnemyIndexes;
    private Vector2 positionOfThisEnemy;

    private string enemyLevelString; // to cache the level of enemy 

    [HideInInspector]
    public GameObject ObjectPulled;
    [HideInInspector]
    public List<GameObject> ObjectPulledList;


    private void OnEnable()
    {
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
        enemyHP = enemyLevel;
        enemyLevelString = enemyLevel.ToString();
        changeTheSpriteOfEnemy(enemyLevelString);
        if (enemyLevel == 1) colorOfEnemy = new Color(0.2f, 0.2f, 0.2f, 1); //dark enemy Color
        if (enemyLevel == 2) colorOfEnemy = new Color(0, 0.4f, 0.65f, 1); //blue enemy Color
        if (enemyLevel == 3) colorOfEnemy = new Color(0.65f, 0, 0.1f, 1); //red enemy Color
        nearEnemyIndexes = new List<Vector2>();
        positionOfThisEnemy = transform.position;
    }

    private void disactivateEnemy()
    {
        GameManager.current.ninjaDestroySound.Play();
        GameManager.current.allEnemiesWithPositionIndexes.Remove(indexOfThisEnemy);
        if (enemyLevel == 2) GameManager.current.all2LevelEnemies.Remove(this);
        else if (enemyLevel == 3) GameManager.current.all3LevelEnemies.Remove(this);
        GameManager.current.checkIfWin();
        nearEnemyIndexes.Clear();
        gameObject.SetActive(false);
        ObjectPulledList = ObjectPuller.current.GetDestroyEffectPullList();
        ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
        ObjectPulled.transform.position = positionOfThisEnemy;
        MainModule main = ObjectPulled.GetComponent<ParticleSystem>().main;
        main.startColor = colorOfEnemy;
        ObjectPulled.SetActive(true);
    }


    public void changeTheSpriteOfEnemy(string newSprite)
    {
        enemySpriteRenderer.sprite = enemyTypesSprites.GetSprite(newSprite);
    }

    //get references to enemies that stay in one step from up, down, right, left of this enemy. To hit them all with mega ball effect 
    public void fixNearEnemiesInList()
    {
        Vector2 indexShif = Vector2.right;
        for (int i = 0; i < 4; i++)
        {
            if (i == 2) indexShif = Vector2.up;
            if (GameManager.current.allEnemiesWithPositionIndexes.ContainsKey(indexOfThisEnemy + indexShif)) nearEnemyIndexes.Add(indexOfThisEnemy + indexShif);
            indexShif *= -1;
        }
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
                ObjectPulled.transform.position = positionOfThisEnemy;
                ObjectPulled.SetActive(true);
                enemyHP = 1; //mega ball destroys enemy in one hit
            }
            reduceHPOfEnenmy(false);
        }
    }

    public void attackWithBall()
    {
        ObjectPulledList = ObjectPuller.current.GetEnemyBallPullList();
        ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
        ObjectPulled.transform.position = positionOfThisEnemy;
        ObjectPulled.GetComponent<TrailRenderer>().Clear();
        ObjectPulled.SetActive(true);
        float xAxisVelocity = Random.Range(0, 2) == 0 ? 1 : -1;
        ObjectPulled.GetComponent<Rigidbody2D>().AddForce(new Vector2(xAxisVelocity, -1) * GameManager.current.ballMoveSpeed, ForceMode2D.Impulse);

        //second throw can make only 3-rd level enemy and towards platform
        if (enemyLevel > 2)
        {
            ObjectPulledList = ObjectPuller.current.GetEnemyBallPullList();
            ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
            ObjectPulled.transform.position = positionOfThisEnemy;
            ObjectPulled.GetComponent<TrailRenderer>().Clear();
            ObjectPulled.SetActive(true);
            ObjectPulled.GetComponent<Rigidbody2D>().AddForce(((Vector2)GameManager.current.platform.platformTransform.position - positionOfThisEnemy).normalized * GameManager.current.ballMoveSpeed, ForceMode2D.Impulse);
        }
    }

    public void reduceHPOfEnenmy(bool megaHit)
    {
        enemyHP--;
        if (megaHit)
        {
            ObjectPulledList = ObjectPuller.current.GetballBurstEffectPullList();
            ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
            ObjectPulled.transform.position = positionOfThisEnemy;
            ObjectPulled.SetActive(true);
        }
        if (enemyHP > 0) GameManager.current.ninjaOuchSound.Play();
        GameManager.current.enemiesDestroyedInOneAir++;
        GameManager.current.incrementScoreBasis();
        GameManager.current.countTheScore();

        if (enemyHP < 1) disactivateEnemy(); //argument is for using with backToMenu function
        else
        {
            changeTheSpriteOfEnemy(enemyLevelString + (enemyLevel - enemyHP).ToString());
        }
    }

    private void megaHitProcessing()
    {
        foreach (Vector2 index in nearEnemyIndexes)
            if (GameManager.current.allEnemiesWithPositionIndexes.ContainsKey(index) && GameManager.current.allEnemiesWithPositionIndexes[index].isActiveAndEnabled) GameManager.current.allEnemiesWithPositionIndexes[index].reduceHPOfEnenmy(true);
    }

}
