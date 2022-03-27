using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public Camera cameraOfGame;

    public static GameManager current;

    [SerializeField]
    private GameObject LeftBorder;
    [SerializeField]
    private GameObject RightBorder;
    [SerializeField]
    private GameObject TopBorder;
    [SerializeField]
    private Platform platform;

    public Ball ball;

    [HideInInspector]
    public bool gameIsOn;
    [HideInInspector]
    public float vertScreenSize;
    [HideInInspector]
    public float horisScreenSize;

    private const float PLATFORM_DISTANCE_FROM_BOTTOM = 3;
    public /*static*/ float BALL_ROTATION_SPEED = 2000;
    private const int VERTICAL_ENEMY_MAX_COUNT = 6;
    private const int HORIZONTAL_ENEMY_MAX_COUNT = 4;
    private const string YOU_WIN_TEXT = "You Win!";
    private const string GAME_OVER_TEXT = "Game Over";

    private Dictionary<Vector2, Vector2> allPositionsForEnemies; //first is index of position, second is coordinates
    public /*static*/ List<GameObject> allEnemies;


    [HideInInspector]
    public GameObject ObjectPulled;
    [HideInInspector]
    public List<GameObject> ObjectPulledList;

    [SerializeField]
    private List<GameObject> availableBalls;
    private int indexOfCurrentBall;

    public GameObject winLosePanel;
    [SerializeField]
    private Text winLoseMessage;


    private void Awake()
    {
        //creating static instance to call some properties and functions from outside directly
        current = this;
    }

        // Start is called before the first frame update
        void Start()
    {
        indexOfCurrentBall = 2;
        allEnemies = new List<GameObject>();
        allPositionsForEnemies = new Dictionary<Vector2, Vector2>();
        gameIsOn = false;
        cameraOfGame = Camera.main;
        //determine the sizes of view screen
        vertScreenSize = cameraOfGame.orthographicSize*2;
        horisScreenSize = vertScreenSize * Screen.width / Screen.height;
        setTheBordersToScreenFrame();
        activateThePlatform();
        activateTheBall(false);
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

    private void activateTheBall(bool isNextBall)
    {
        ball.transform.position = new Vector2(platform.platformTransform.position.x, platform.platformTransform.position.y+ platform.platformTransform.localScale.y/3);
        if (!isNextBall)
        {
            ball.gameManager = this;
        }
        else ball.ballTrail.Clear();
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
                    positionCoordinates = new Vector2(-horisScreenSize/2 + stepOfHorizontalPositions / 2, vertScreenSize/2 - stepOfVerticalPositions - TopBorder.transform.localScale.y); //here is necessary some space from top border to UI elements
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
            ObjectPulledList = ObjectPuller.current.GetEnemyPullList();
            ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
            ObjectPulled.transform.position = coordinates.Value;
            allEnemies.Add(ObjectPulled);
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
    public void startTheGame()
    {
        //condition to check if game starts from the beginning
        if (!availableBalls[0].activeInHierarchy)
        {
            for (int i = 0; i < availableBalls.Count; i++) availableBalls[i].SetActive(true);
        }
        ball.startTheBall();
    }


    public void reduceAvailableBalls() {
        availableBalls[indexOfCurrentBall].SetActive(false);
        if (indexOfCurrentBall > 0)
        {
            indexOfCurrentBall--;
            activateTheBall(true);
        }
        else
        {
            gameIsOn = false;
            winLosePanel.SetActive(true);
            winLoseMessage.text = GAME_OVER_TEXT;
        }
    }

    public void checkIfWin() {
        if (allEnemies.Count < 1)
        {
            gameIsOn = false;
            winLosePanel.SetActive(true);
            winLoseMessage.text = YOU_WIN_TEXT;
            ball.disactivateTheBall(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
