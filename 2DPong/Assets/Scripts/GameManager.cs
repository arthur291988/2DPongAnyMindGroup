using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public Camera cameraOfGame;
    public static GameManager current;

    [HideInInspector]
    public GameObject ObjectPulled;
    [HideInInspector]
    public List<GameObject> ObjectPulledList;


    [SerializeField]
    private GameObject LeftBorder;
    [SerializeField]
    private GameObject RightBorder;
    [SerializeField]
    private GameObject TopBorder;
    [HideInInspector]
    public float vertScreenSize;
    [HideInInspector]
    public float horisScreenSize;

    public Platform platform;
    public Ball ball;

    [HideInInspector]
    public bool gameIsOn;
    private int currentLevel;

    private const float PLATFORM_DISTANCE_FROM_BOTTOM = 3;
    [HideInInspector]
    public float ballRotationSpeed;
    [HideInInspector]
    public float ballMoveSpeed;

    private const float BALL_ROTATION_SPEED = 2000;
    private const float BALL_MOVE_SPEED_1_LEVEL = 10;
    private const float BALL_MOVE_SPEED_2_LEVEL = 13;
    private const float BALL_MOVE_SPEED_3_LEVEL = 15;
    private const int VERTICAL_ENEMY_MAX_COUNT = 6;
    private const int HORIZONTAL_ENEMY_MAX_COUNT = 4;

    [HideInInspector]
    public Dictionary<Vector2, Vector2> allPositionsForEnemiesWithIndexes; //first is index of position, second is coordinates
    [HideInInspector]
    public Dictionary<Vector2, Enemy> allEnemiesWithPositionIndexes;

    //used to pick randome enemy to throw ball/suriken
    [HideInInspector]
    public List<Enemy> all3LevelEnemies;
    [HideInInspector]
    public List<Enemy> all2LevelEnemies;

    [SerializeField]
    private List<GameObject> availableBalls;
    private int indexOfCurrentBall;

    public GameObject winLosePanel;
    [SerializeField]
    private Text winLoseMessage;
    private const string YOU_WIN_TEXT = "You Win!";
    private const string GAME_OVER_TEXT = "Game Over";

    [SerializeField]
    private Text scoreText;
    private int scoreCount;
    [HideInInspector]
    public int scoreBasis;
    [HideInInspector]
    public int enemiesDestroyedInOneAir;
    private const int DOUBLE_SCORE_CONDITION = 2;
    private const int TRIPLE_SCORE_CONDITION = 4;
    private const int DEFAULT_SCORE_BASIS = 2;
    private const int DOUBLE_SCORE = 4;
    private const int TRIPLE_SCORE = 6;
    private int baseAchievementScoreOfLevel;
    [SerializeField]
    private List<GameObject> achievementTokens;
    private float achievementTokensColorR = 0.93f;

    [SerializeField]
    private GameObject megaBall;


    private void Awake()
    {
        //creating static instance to call some properties and functions from outside directly
        current = this;
        ballRotationSpeed = BALL_ROTATION_SPEED;
    }

    // Start is called before the first frame update
    void Start()
    {
        indexOfCurrentBall = 2;
        all3LevelEnemies = new List<Enemy>();
        all2LevelEnemies = new List<Enemy>();
        allPositionsForEnemiesWithIndexes = new Dictionary<Vector2, Vector2>();
        allEnemiesWithPositionIndexes = new Dictionary<Vector2, Enemy>();
        gameIsOn = false;
        cameraOfGame = Camera.main;
        //determine the sizes of view screen
        vertScreenSize = cameraOfGame.orthographicSize * 2;
        horisScreenSize = vertScreenSize * Screen.width / Screen.height;
        setTheBordersToScreenFrame();
        activateThePlatform();
        setAllEnemyPositions();
        startTheLevel(3); //TO CALL FROM BUTTON
        activateTheBall(false);
        StartCoroutine(startTheMegaBallTimer());
    }

    private void setTheBordersToScreenFrame()
    {
        LeftBorder.transform.position = new Vector2(-horisScreenSize / 2, 0);
        RightBorder.transform.position = new Vector2(horisScreenSize / 2, 0);
        TopBorder.transform.position = new Vector2(0, vertScreenSize / 2);
    }

    private void activateThePlatform()
    {
        platform.gameManager = this;
        platform.transform.position = new Vector2(0, -vertScreenSize / 2 + platform.transform.localScale.y / 2 + PLATFORM_DISTANCE_FROM_BOTTOM);
        platform.leftBorderForPlatform = -horisScreenSize / 2 + LeftBorder.transform.localScale.x / 2 + platform.transform.localScale.x / 2;
        platform.rightBorderForPlatform = horisScreenSize / 2 - RightBorder.transform.localScale.x / 2 - platform.transform.localScale.x / 2;
        platform.gameObject.SetActive(true);
    }

    private void activateTheBall(bool isNextBall)
    {
        ball.transform.position = new Vector2(platform.platformTransform.position.x, platform.platformTransform.position.y + platform.platformTransform.localScale.y / 3);
        if (!isNextBall)
        {
            ball.gameManager = this;
        }
        else ball.ballTrail.Clear();
        ball.gameObject.SetActive(true);
    }
    private IEnumerator startTheMegaBallTimer()
    {
        megaBall.transform.position = new Vector2(Random.Range(-horisScreenSize / 2 + 1, horisScreenSize / 2 - 1), vertScreenSize / 2 + 1); //set mega bonus higher than scene and in randome point in horizontal axis
        yield return new WaitForSeconds(Random.Range(6f, 20f));
        megaBall.SetActive(true);
    }

    //called only once while start of application
    private void setAllEnemyPositions()
    {
        float stepOfHorizontalPositions = horisScreenSize / HORIZONTAL_ENEMY_MAX_COUNT;
        float stepOfVerticalPositions = vertScreenSize / 2 / VERTICAL_ENEMY_MAX_COUNT; //vertical screen size is used by enemy only for a half (other half is for player maneuvers)
        Vector2 positionCoordinates = Vector2.zero;
        for (int i = 0; i < VERTICAL_ENEMY_MAX_COUNT; i++)
        {
            for (int j = 0; j < HORIZONTAL_ENEMY_MAX_COUNT; j++)
            {
                //top left position (first in dictionary of positions)
                if (i == 0 && j == 0)
                {
                    positionCoordinates = new Vector2(-horisScreenSize / 2 + stepOfHorizontalPositions / 2, vertScreenSize / 2 - stepOfVerticalPositions - TopBorder.transform.localScale.y); //here is necessary some space from top border to UI elements
                }
                else if (j == 0)
                {
                    positionCoordinates = new Vector2(-horisScreenSize / 2 + stepOfHorizontalPositions / 2, positionCoordinates.y - stepOfVerticalPositions);
                }
                else
                {
                    positionCoordinates = new Vector2(positionCoordinates.x + stepOfHorizontalPositions, positionCoordinates.y);
                }

                allPositionsForEnemiesWithIndexes.Add(new Vector2(i + 1, j + 1), positionCoordinates);
            }
        }
    }

    private void setTheEnemies(int level) {
        foreach (var coordinates in allPositionsForEnemiesWithIndexes)
        {
            if (LevelParameters.allLevels[level].ContainsKey(coordinates.Key))
            {
                ObjectPulledList = ObjectPuller.current.GetEnemyPullList();
                ObjectPulled = ObjectPuller.current.GetGameObjectFromPull(ObjectPulledList);
                ObjectPulled.transform.position = coordinates.Value;
                Enemy enemy = ObjectPulled.GetComponent<Enemy>();
                enemy.enemyLevel = LevelParameters.allLevels[level][coordinates.Key];
                enemy.indexOfThisEnemy = coordinates.Key;
                allEnemiesWithPositionIndexes.Add(coordinates.Key, enemy);
                if (enemy.enemyLevel==2) all2LevelEnemies.Add(enemy);
                if (enemy.enemyLevel==3) all3LevelEnemies.Add(enemy);
                ObjectPulled.SetActive(true);
            }
        }

        //setting the reference to each enemy from left, right, up, and down to use in mega ball effect
        foreach (var enenmy in allEnemiesWithPositionIndexes)
        {
            enenmy.Value.fixNearEnemiesInList();
        }
    }


    private void startTheLevel(int level)
    {
        currentLevel = level;
        ballMoveSpeed = level > 5 ? BALL_MOVE_SPEED_3_LEVEL : level > 2 ? BALL_MOVE_SPEED_2_LEVEL : BALL_MOVE_SPEED_1_LEVEL;
        allEnemiesWithPositionIndexes.Clear();
        all2LevelEnemies.Clear();
        all3LevelEnemies.Clear();
        scoreCount = 0;
        scoreBasis = DEFAULT_SCORE_BASIS;
        enemiesDestroyedInOneAir = 0;
        scoreText.text = scoreCount.ToString();
        setTheEnemies(level);
        foreach (var enenmy in allEnemiesWithPositionIndexes)
        {
            baseAchievementScoreOfLevel += DEFAULT_SCORE_BASIS * enenmy.Value.enemyLevel;
        }
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

    public void goToMenuButton() {
        for (int i = 0; i < achievementTokens.Count; i++)
        {
            achievementTokens[i].GetComponent<RawImage>().color = new Color(achievementTokensColorR, 0, 0, 0.2f);
            achievementTokens[i].SetActive(false);
        }
        winLosePanel.SetActive(false);
    }

    public void restartTheGameButton() {
        startTheLevel(currentLevel);
    }

    public void countTheScore() {
        scoreCount += scoreBasis;
        scoreText.text = scoreCount.ToString();
    }
    public void incrementScoreBasis()
    {
        if (enemiesDestroyedInOneAir > DOUBLE_SCORE_CONDITION && scoreBasis < DOUBLE_SCORE) scoreBasis = DOUBLE_SCORE;
        else if (enemiesDestroyedInOneAir > TRIPLE_SCORE_CONDITION && scoreBasis < TRIPLE_SCORE) scoreBasis = TRIPLE_SCORE;
    }
    public void resetScoreBasis()
    {
        enemiesDestroyedInOneAir = 0;
        scoreBasis = DEFAULT_SCORE_BASIS;
    }

    public void enemyAttacks ()
    {
        //pick one enenmy to attack the player

        if (all3LevelEnemies.Count > 0)
        {
            if (all3LevelEnemies.Count == 1) all3LevelEnemies[0].attackWithBall();
            else all3LevelEnemies[Random.Range(0, all3LevelEnemies.Count)].attackWithBall();
        }
        else if (all2LevelEnemies.Count > 0) {
            if (all2LevelEnemies.Count == 1) all2LevelEnemies[0].attackWithBall();
            else all2LevelEnemies[Random.Range(0, all2LevelEnemies.Count)].attackWithBall();
        }
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
            winLoseMessage.text = GAME_OVER_TEXT;
            foreach (var enenmy in allEnemiesWithPositionIndexes) enenmy.Value.gameObject.SetActive(false);
            winLosePanel.SetActive(true);
        }
    }

    public void checkIfWin() {
        if (allEnemiesWithPositionIndexes.Count < 1)
        {
            gameIsOn = false;
            winLoseMessage.text = YOU_WIN_TEXT;
            winLosePanel.SetActive(true);
            int achievementsCount = 0;
            if (scoreCount > baseAchievementScoreOfLevel * 2) achievementsCount = 3;
            else if (scoreCount > baseAchievementScoreOfLevel * 1.5f) achievementsCount = 2;
            else achievementsCount = 1;
            for (int i = 0; i < achievementTokens.Count; i++)
            {
                if (i < achievementsCount) achievementTokens[i].GetComponent<RawImage>().color = new Color(achievementTokensColorR, 0, 0, 1);
                achievementTokens[i].SetActive(true);
            }

            ball.disactivateTheBall(true);
        }
    }

}
