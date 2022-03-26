using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public Camera cameraOfGame;

    [SerializeField]
    private GameObject LeftBorder;
    [SerializeField]
    private GameObject RightBorder;
    [SerializeField]
    private GameObject TopBorder;
    [SerializeField]
    private Platform platform;
    [SerializeField]
    private Ball ball;

    [HideInInspector]
    public bool gameIsOn;
    [HideInInspector]
    public float vertScreenSize;
    [HideInInspector]
    public float horisScreenSize;

    private const int VERTICAL_ENEMY_MAX_COUNT = 7;
    private const int HORIZONTAL_ENEMY_MAX_COUNT = 4;
    private Dictionary<Vector2, Vector2> allPositionsForEnemies; //first is index of position, second is coordinates

    private const float PLATFORM_DISTANCE_FROM_BOTTOM = 3;

    [HideInInspector]
    public GameObject ObjectPulled;
    [HideInInspector]
    public List<GameObject> ObjectPulledList;

    // Start is called before the first frame update
    void Start()
    {
        allPositionsForEnemies = new Dictionary<Vector2, Vector2>();
        gameIsOn = false;
        cameraOfGame = Camera.main;
        //determine the sizes of view screen
        vertScreenSize = cameraOfGame.orthographicSize*2;
        horisScreenSize = vertScreenSize * Screen.width / Screen.height;
        setTheBordersToScreenFrame();
        activateThePlatform();
        activateTheBall();
        setTheEnemies();
    }

    private void setTheBordersToScreenFrame()
    {
        LeftBorder.transform.position = new Vector2(-horisScreenSize / 2 , 0);
        RightBorder.transform.position = new Vector2(horisScreenSize / 2 , 0);
        TopBorder.transform.position = new Vector2(0, vertScreenSize / 2);
    }

    private void activateThePlatform()
    {
        platform.gameManager = this;
        platform.transform.position = new Vector2(0, -vertScreenSize / 2 + platform.transform.localScale.y / 2 + PLATFORM_DISTANCE_FROM_BOTTOM);
        platform.leftBorderForPlatform = -horisScreenSize / 2 + LeftBorder.transform.localScale.x/2  + platform.transform.localScale.x/2;
        platform.rightBorderForPlatform = horisScreenSize / 2 - RightBorder.transform.localScale.x/2  - platform.transform.localScale.x/2;
        platform.gameObject.SetActive(true);

    }

    private void activateTheBall()
    {
        ball.transform.position = new Vector3(platform.platformTransform.position.x, platform.platformTransform.position.y+ platform.platformTransform.localScale.y/3, 0);
        ball.gameObject.SetActive(true);
    }

    private void setTheEnemies() {
        float stepOfHorizontalPositions = horisScreenSize / HORIZONTAL_ENEMY_MAX_COUNT;
        float stepOfVerticalPositions = vertScreenSize /2 / VERTICAL_ENEMY_MAX_COUNT; //vertical screen size is used by enemy only for a half (other half is for player maneuvers)
        Vector2 positionCoordinates = Vector2.zero;
        for (int i = 0; i < VERTICAL_ENEMY_MAX_COUNT; i++) {
            for (int j = 0; j < HORIZONTAL_ENEMY_MAX_COUNT; j++) {
                //top left position (first in dictionary of positions)
                if (i == 0 && j == 0)
                {
                    positionCoordinates = new Vector2(-horisScreenSize/2 + stepOfHorizontalPositions / 2, vertScreenSize/2 - stepOfVerticalPositions / 2- TopBorder.transform.localScale.y); //here is necessary some space from top border 
                }
                else if (j == 0)
                {
                    positionCoordinates = new Vector2(-horisScreenSize / 2 + stepOfHorizontalPositions / 2, positionCoordinates.y- stepOfVerticalPositions);
                }
                else
                {
                    positionCoordinates = new Vector2(positionCoordinates.x + stepOfHorizontalPositions, positionCoordinates.y);
                }

                allPositionsForEnemies.Add(new Vector2(i + 1, j + 1), positionCoordinates);
            }

        }

        foreach (var coordinates in allPositionsForEnemies)
        {
            gameObject.SetActive(false);
            ObjectPulledList = ObjectPuller.current.GetEnemyPullList();
            ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
            ObjectPulled.transform.position = coordinates.Value;
            ObjectPulled.SetActive(true);
        }

        //for (int i =0;i< allPositionsForEnemies.Count;i++)
        //{
        //    gameObject.SetActive(false);
        //    ObjectPulledList = ObjectPuller.current.GetDestroyEffectPullList();
        //    ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
        //    ObjectPulled.transform.position = allPositionsForEnemies.Keys[i];
        //    ObjectPulled.SetActive(true);
        //}

    }

    //called from platform script while player touches screen firs time
    public void startTheGame() {
        ball.startTheBall();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
