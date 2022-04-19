
using System.Collections.Generic;
using UnityEngine;

public class ObjectPuller : MonoBehaviour
{

    public static ObjectPuller current;

    private int pullOfObjects2 = 2;
    private int pullOfObjects3 = 3;
    private int pullOfObjects35 = 35;
    private bool willGrow;

    [SerializeField]
    private GameObject ballBurstEffect;
    [SerializeField]
    private GameObject destroyEffect;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject enemyBall0;
    [SerializeField]
    private GameObject enemyBall1;
    [SerializeField]
    private GameObject enemyBall2;

    [HideInInspector]
    public List<GameObject> ballBurstEffectPull;
    [HideInInspector]
    public List<GameObject> destroyEffectPull;
    [HideInInspector]
    public List<GameObject> enemyPull;
    [HideInInspector]
    public List<GameObject> enemyBallPull0;
    [HideInInspector]
    public List<GameObject> enemyBallPull1;
    [HideInInspector]
    public List<GameObject> enemyBallPull2;

    private void Awake()
    {
        willGrow = true;
        current = this;
    }

    private void OnEnable()
    {
        ballBurstEffectPull = new List<GameObject>();
        destroyEffectPull = new List<GameObject>();
        enemyPull = new List<GameObject>();
        enemyBallPull0 = new List<GameObject>();
        enemyBallPull1 = new List<GameObject>();
        enemyBallPull2 = new List<GameObject>();

        for (int i = 0; i < pullOfObjects2; i++)
        {
            GameObject obj = (GameObject)Instantiate(ballBurstEffect);
            obj.SetActive(false);
            ballBurstEffectPull.Add(obj);
        }
        for (int i = 0; i < pullOfObjects3; i++)
        {
            GameObject obj = (GameObject)Instantiate(destroyEffect);
            obj.SetActive(false);
            destroyEffectPull.Add(obj);

            GameObject obj1 = (GameObject)Instantiate(enemyBall0);
            obj1.SetActive(false);
            enemyBallPull0.Add(obj1);

            GameObject obj2 = (GameObject)Instantiate(enemyBall1);
            obj2.SetActive(false);
            enemyBallPull1.Add(obj2);

            GameObject obj3 = (GameObject)Instantiate(enemyBall2);
            obj3.SetActive(false);
            enemyBallPull2.Add(obj3);

        }
        for (int i = 0; i < pullOfObjects35; i++)
        {
            GameObject obj = (GameObject)Instantiate(enemy);
            obj.SetActive(false);
            enemyPull.Add(obj);
        }
    }
    public List<GameObject> GetballBurstEffectPullList()
    {
        return ballBurstEffectPull;
    }
    public List<GameObject> GetDestroyEffectPullList()
    {
        return destroyEffectPull;
    }
    public List<GameObject> GetEnemyPullList()
    {
        return enemyPull;
    }
    public List<GameObject> GetEnemyBallPullList(byte levelOfBall)
    {
        if (levelOfBall == 0) return enemyBallPull0;
        else if (levelOfBall == 1) return enemyBallPull1;
        else return enemyBallPull2;
    }

    //universal method to set active proper game object from the list of GOs, it just needs to get correct List of game objects
    public GameObject GetGameObjectFromPull(List<GameObject> GOLists)
    {
        for (int i = 0; i < GOLists.Count; i++)
        {
            if (!GOLists[i].activeInHierarchy) return (GameObject)GOLists[i];
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(GOLists[0]);
            obj.SetActive(false);
            GOLists.Add(obj);
            return obj;
        }
        return null;
    }

}
