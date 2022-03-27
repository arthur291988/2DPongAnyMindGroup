
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

    [HideInInspector]
    public float startImpulseOfBall;

    private Color colorOfEnemy;

    private void OnEnable()
    {
        spriteRenderer =GetComponent<SpriteRenderer>();
        enemyLevel = 1;
        enemyHP = enemyLevel;
        enemyLevelString = enemyLevel.ToString();
        ChengeTheSpriteOfEnemy(enemyLevelString);
        if (enemyLevel == 1) colorOfEnemy = new Color(0.2f,0.2f,0.2f,1); //dark enemy Color
        if (enemyLevel == 2) colorOfEnemy = new Color(0, 0.4f, 0.65f, 1); //blue enemy Color
        if (enemyLevel == 3) colorOfEnemy = new Color(0.65f, 0, 0.1f, 1); //red enemy Color
    }


    public void ChengeTheSpriteOfEnemy(string newSprite)
    {
        spriteRenderer.sprite = spriteAtlas.GetSprite(newSprite);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        enemyHP--;
        if (enemyHP < 1) disactivateEnemy(true); //argument is for using with back To Menu function
        else {
            ChengeTheSpriteOfEnemy(enemyLevelString + (enemyLevel-enemyHP).ToString());
        }
    }

    private void disactivateEnemy(bool isDestroyed) {
        if (isDestroyed) {
            GameManager.current.allEnemies.Remove(gameObject);
            GameManager.current.checkIfWin();
            gameObject.SetActive(false);
            ObjectPulledList = ObjectPuller.current.GetDestroyEffectPullList();
            ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
            ObjectPulled.transform.position = transform.position;
            MainModule main = ObjectPulled.GetComponent<ParticleSystem>().main;
            main.startColor = colorOfEnemy;
            ObjectPulled.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
