
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

    [HideInInspector]
    public List<GameObject> ballBurstEffectPull;
    [HideInInspector]
    public List<GameObject> destroyEffectPull;
    [HideInInspector]
    public List<GameObject> enemyPull;



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
