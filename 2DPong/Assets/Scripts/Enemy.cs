using System.Collections;
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

    [SerializeField]
    private SpriteAtlas enemyTypesSprites;
    private SpriteRenderer enemySpriteRenderer;
    private Color colorOfEnemy; //this one is used for destroy effect
    private Color colorOfEnemyTransparent;
    private Color colorOfEnemyNormal;

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

    private GameObject thisGo;
    private ParticleSystem dissapearEffect;

    private GameObject goldEnemyBackPicture;

    private bool isInGoldReflectionMode;


    private void OnEnable()
    {
        thisGo = gameObject;
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
        enemyHP = enemyLevel;
        enemyLevelString = enemyLevel.ToString();
        changeTheSpriteOfEnemy(enemyLevelString);
        if (enemyLevel == 1) colorOfEnemy = new Color(0.2f, 0.2f, 0.2f, 1); //dark enemy Color
        if (enemyLevel == 2) colorOfEnemy = new Color(0, 0.4f, 0.65f, 1); //blue enemy Color
        if (enemyLevel == 3) colorOfEnemy = new Color(0.65f, 0, 0.1f, 1); //red enemy Color
        if (enemyLevel == 4)
        {
            colorOfEnemy = Color.grey; //grey enemy Color
            colorOfEnemyTransparent = new Color(1,1,1,0.3f);
            colorOfEnemyNormal = Color.white;
            dissapearEffect = GetComponent<ParticleSystem>();
            MainModule main = dissapearEffect.main;
            main.startColor = colorOfEnemyNormal;

            StartCoroutine(transparentIn(Random.Range(6, 8)));
        }
        if (enemyLevel == 5)
        {
            colorOfEnemy = Color.yellow; //gold enemy Color
            colorOfEnemyTransparent = new Color(1, 1, 1, 0.3f); //is used to give transparency to the ball while it hits featured enemy
            goldEnemyBackPicture = transform.GetChild(0).gameObject;
            dissapearEffect = GetComponent<ParticleSystem>();
            MainModule main = dissapearEffect.main;
            main.startColor = colorOfEnemy;
            StartCoroutine(FivethLevelEnemyFeatureIn(Random.Range(8, 12)));
        }
        nearEnemyIndexes = new List<Vector2>();
        positionOfThisEnemy = transform.position;
    }

    private void disactivateEnemy()
    {
        GameManager.current.ninjaDestroySound.Play();
        GameManager.current.allEnemiesWithPositionIndexes.Remove(indexOfThisEnemy);
        if (enemyLevel == 2) GameManager.current.all2LevelEnemies.Remove(this);
        else if (enemyLevel == 3) GameManager.current.all3LevelEnemies.Remove(this);
        else if (enemyLevel == 4) GameManager.current.all4LevelEnemies.Remove(this);
        else if (enemyLevel == 5) GameManager.current.all5LevelEnemies.Remove(this);
        GameManager.current.checkIfWin();
        nearEnemyIndexes.Clear();
        thisGo.SetActive(false);
        ObjectPulledList = ObjectPuller.current.GetDestroyEffectPullList();
        ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
        ObjectPulled.transform.position = positionOfThisEnemy;
        MainModule main = ObjectPulled.GetComponent<ParticleSystem>().main;
        main.startColor = colorOfEnemy;
        if (enemyLevel == 4)
        {
            thisGo.layer = 6;
            enemySpriteRenderer.color = colorOfEnemyNormal;
        }
        if (enemyLevel == 5)
        {
            isInGoldReflectionMode = false; 
            goldEnemyBackPicture.SetActive(false);
        }
        if (enemyLevel>=4) StopAllCoroutines();
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
            if (!isInGoldReflectionMode)
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
            else {
                GameManager.current.surikenSound.Play();
                isInGoldReflectionMode = false;
                goldEnemyBackPicture.SetActive(false);
                StopAllCoroutines();
                StartCoroutine(FivethLevelEnemyFeatureIn(Random.Range(8, 12)));
                ball.thisGO.layer = 6;
                ball.ballSpriteRenderer.color = colorOfEnemyTransparent;
            }
        }
    }

    public void attackWithBall()
    {
        if (enemyLevel <= 3)
        {
            ObjectPulledList = ObjectPuller.current.GetEnemyBallPullList(0);
            ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
            ObjectPulled.transform.position = positionOfThisEnemy;
            ObjectPulled.GetComponent<TrailRenderer>().Clear();
            ObjectPulled.GetComponent<EnemyBall>().ballLevel = 0;
            ObjectPulled.SetActive(true);
            float xAxisVelocity = Random.Range(0, 2) == 0 ? 1 : -1;
            ObjectPulled.GetComponent<Rigidbody2D>().AddForce(new Vector2(xAxisVelocity, -1) * GameManager.current.ballMoveSpeed, ForceMode2D.Impulse);

            //second throw can make only 3-rd level enemy and towards platform
            if (enemyLevel > 2)
            {
                ObjectPulledList = ObjectPuller.current.GetEnemyBallPullList(0);
                ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
                ObjectPulled.transform.position = positionOfThisEnemy;
                ObjectPulled.GetComponent<TrailRenderer>().Clear();
                ObjectPulled.GetComponent<EnemyBall>().ballLevel = 0;
                ObjectPulled.SetActive(true);
                ObjectPulled.GetComponent<Rigidbody2D>().AddForce(((Vector2)GameManager.current.platform.platformTransform.position - positionOfThisEnemy).normalized * GameManager.current.ballMoveSpeed, ForceMode2D.Impulse);
            }
        }
        else if (enemyLevel == 4)
        {
            ObjectPulledList = ObjectPuller.current.GetEnemyBallPullList(1);
            ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
            ObjectPulled.transform.position = positionOfThisEnemy;
            ObjectPulled.GetComponent<TrailRenderer>().Clear();
            ObjectPulled.GetComponent<EnemyBall>().ballLevel = 1;
            ObjectPulled.SetActive(true);
            ObjectPulled.GetComponent<Rigidbody2D>().AddForce(((Vector2)GameManager.current.platform.platformTransform.position - positionOfThisEnemy).normalized * GameManager.current.ballMoveSpeed, ForceMode2D.Impulse);
        }
        else
        {
            ObjectPulledList = ObjectPuller.current.GetEnemyBallPullList(2);
            ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
            ObjectPulled.transform.position = positionOfThisEnemy;
            ObjectPulled.GetComponent<TrailRenderer>().Clear();
            ObjectPulled.GetComponent<EnemyBall>().ballLevel = 2;
            ObjectPulled.SetActive(true);
            ObjectPulled.GetComponent<Rigidbody2D>().AddForce(((Vector2)GameManager.current.platform.platformTransform.position - positionOfThisEnemy).normalized * GameManager.current.ballMoveSpeed, ForceMode2D.Impulse);
        }
    }

    //this two behaviours are used with 4-th level enemies to make them invisible to ball 
    private IEnumerator transparentIn(float time)
    {
        yield return new WaitForSeconds(time);
        dissapearEffect.Play();
        thisGo.layer = 1;
        enemySpriteRenderer.color = colorOfEnemyTransparent;
        StartCoroutine(transparentOut());


    }
    private IEnumerator transparentOut ()
    {
        yield return new WaitForSeconds(Random.Range(3f, 7f));
        thisGo.layer = 6;
        enemySpriteRenderer.color = colorOfEnemyNormal;
        StartCoroutine(transparentIn(Random.Range(6,12)));
    }

    private IEnumerator FivethLevelEnemyFeatureIn(float time)
    {
        yield return new WaitForSeconds(time);
        dissapearEffect.Play();
        isInGoldReflectionMode = true;
        goldEnemyBackPicture.SetActive(true);
        StartCoroutine(FivethLevelEnemyFeatureOut());
    }

    private IEnumerator FivethLevelEnemyFeatureOut()
    {
        yield return new WaitForSeconds(Random.Range(3f, 5f));
        isInGoldReflectionMode = false;
        goldEnemyBackPicture.SetActive(false);
        StartCoroutine(FivethLevelEnemyFeatureIn(Random.Range(8, 12)));
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
