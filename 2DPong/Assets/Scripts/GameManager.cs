using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

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
    private int achievedLevel;

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
    private GameObject availableBallsPanel;
    [SerializeField]
    private List<GameObject> availableBalls;
    private int indexOfCurrentBall;

    public GameObject winLosePanel;
    [SerializeField]
    private Text winLoseMessage;
    private const string YOU_WIN_TEXT = "You Win!";
    private const string GAME_OVER_TEXT = "Game Over";

    [SerializeField]
    private GameObject scoreTitle;
    [SerializeField]
    private Text scoreText;
    private int scoreCount;
    [HideInInspector]
    public int scoreBasis;
    [HideInInspector]
    public int enemiesDestroyedInOneAir;
    private const int DOUBLE_SCORE_CONDITION = 1;
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
    [HideInInspector]
    public Transform megaBallTransform;

    [SerializeField]
    private GameObject levelsPanel;
    [SerializeField]
    private List<Levels> LevelIconsClasses;
    private string MenuBackGround = "0";
    private string Level12BackGroundNameInAtlas = "1";
    private string Level34BackGroundNameInAtlas = "2";
    private string Level56BackGroundNameInAtlas = "3";
    [SerializeField]
    private SpriteAtlas spriteAtlasOfBackgrounds;
    [SerializeField]
    private Image backgroundImage;

    public AudioSource surikenSound;
    public AudioSource ninjaOuchSound;
    public AudioSource ninjaDestroySound;
    public AudioSource megaBallSound;
    public AudioSource iceEffectSound;
    public AudioSource gameEndSound;

    private void Awake()
    {
        //creating static instance to call some properties and functions from outside directly
        current = this;
        ballRotationSpeed = BALL_ROTATION_SPEED;
    }

    // Start is called before the first frame update
    void Start()
    {
        achievedLevel = 0;
        indexOfCurrentBall = 2;
        all3LevelEnemies = new List<Enemy>();
        all2LevelEnemies = new List<Enemy>();
        allPositionsForEnemiesWithIndexes = new Dictionary<Vector2, Vector2>();
        allEnemiesWithPositionIndexes = new Dictionary<Vector2, Enemy>();
        gameIsOn = false;
        cameraOfGame = Camera.main;
        megaBallTransform = megaBall.transform;

        //determine the sizes of view screen
        vertScreenSize = cameraOfGame.orthographicSize * 2;
        horisScreenSize = vertScreenSize * Screen.width / Screen.height;

        changeTheSpriteOfBackground(MenuBackGround);
        LoadData();
        setTheBordersToScreenFrame();
        setAllEnemyPositions();
        levelsPanel.SetActive(true);
    }

    //called only once while start of application
    #region 
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

    private void setTheBordersToScreenFrame()
    {
        LeftBorder.transform.position = new Vector2(-horisScreenSize / 2, 0);
        RightBorder.transform.position = new Vector2(horisScreenSize / 2, 0);
        TopBorder.transform.position = new Vector2(0, vertScreenSize / 2);
    }
    #endregion

    //game start functions
    #region
    public void startTheLevel(int level)
    {
        if (level > 4) changeTheSpriteOfBackground(Level56BackGroundNameInAtlas);
        else if (level > 2) changeTheSpriteOfBackground(Level34BackGroundNameInAtlas);
        else changeTheSpriteOfBackground(Level12BackGroundNameInAtlas);
        indexOfCurrentBall = 2;
        for (int i = 0; i < availableBalls.Count; i++) if (!availableBalls[i].activeInHierarchy) availableBalls[i].SetActive(true);
        currentLevel = level;
        ballMoveSpeed = level > 4 ? BALL_MOVE_SPEED_3_LEVEL : level > 2 ? BALL_MOVE_SPEED_2_LEVEL : BALL_MOVE_SPEED_1_LEVEL;
        allEnemiesWithPositionIndexes.Clear();
        all2LevelEnemies.Clear();
        all3LevelEnemies.Clear();
        scoreCount = 0;
        scoreBasis = DEFAULT_SCORE_BASIS;
        enemiesDestroyedInOneAir = 0;
        scoreText.text = scoreCount.ToString();
        activateThePlatform();
        activateTheBall(false);
        setTheEnemies(level);
        baseAchievementScoreOfLevel = 0;
        foreach (var enenmy in allEnemiesWithPositionIndexes)
        {
            baseAchievementScoreOfLevel += DEFAULT_SCORE_BASIS * enenmy.Value.enemyLevel;
        }
        if (levelsPanel.activeInHierarchy) levelsPanel.SetActive(false);
        scoreTitle.SetActive(true);
        availableBallsPanel.SetActive(true);
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
    public IEnumerator startTheMegaBallTimer()
    {
        yield return new WaitForSeconds(Random.Range(6f, 20f));
        if (!megaBall.activeInHierarchy && gameIsOn)
        {
            megaBallTransform.position = new Vector2(Random.Range(-horisScreenSize / 2 + 1, horisScreenSize / 2 - 1), vertScreenSize / 2 + 1); //set mega bonus higher than scene and in randome point in horizontal axis
            megaBall.SetActive(true);
        }
    }

    private void setTheEnemies(int level)
    {
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
                if (enemy.enemyLevel == 2) all2LevelEnemies.Add(enemy);
                if (enemy.enemyLevel == 3) all3LevelEnemies.Add(enemy);
                ObjectPulled.SetActive(true);
            }
        }

        //setting the reference to each enemy from left, right, up, and down to use in mega ball effect
        foreach (var enenmy in allEnemiesWithPositionIndexes)
        {
            enenmy.Value.fixNearEnemiesInList();
        }
    }

    public void changeTheSpriteOfBackground(string newSprite)
    {
        backgroundImage.sprite = spriteAtlasOfBackgrounds.GetSprite(newSprite);
    }
    #endregion


    //game process functions
    #region
    public void countTheScore()
    {
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

    public void enemyAttacks()
    {
        //pick one enenmy to attack the player

        if (all3LevelEnemies.Count > 0)
        {
            if (all3LevelEnemies.Count == 1) all3LevelEnemies[0].attackWithBall();
            else all3LevelEnemies[Random.Range(0, all3LevelEnemies.Count)].attackWithBall();
        }
        else if (all2LevelEnemies.Count > 0)
        {
            if (all2LevelEnemies.Count == 1) all2LevelEnemies[0].attackWithBall();
            else all2LevelEnemies[Random.Range(0, all2LevelEnemies.Count)].attackWithBall();
        }
    }

    public void reduceAvailableBallsAndCheckTheLost()
    {
        availableBalls[indexOfCurrentBall].SetActive(false);
        if (indexOfCurrentBall > 0)
        {
            indexOfCurrentBall--;
            activateTheBall(true);
        }
        else
        {
            gameIsOn = false;
            if (megaBall.activeInHierarchy) megaBall.SetActive(false);
            winLoseMessage.text = GAME_OVER_TEXT;
            foreach (var enenmy in allEnemiesWithPositionIndexes) enenmy.Value.gameObject.SetActive(false);
            winLosePanel.SetActive(true);
            gameEndSound.Play();
        }
    }

    public void checkIfWin()
    {
        if (allEnemiesWithPositionIndexes.Count < 1)
        {
            calculateTheAchievements();
            gameIsOn = false;
            if (megaBall.activeInHierarchy) megaBall.SetActive(false);
            winLoseMessage.text = YOU_WIN_TEXT;
            winLosePanel.SetActive(true);
            ball.disactivateTheBall(true);
            gameEndSound.Play();
        }
    }
    #endregion


    //game end functions
    #region

    //achievements appear a moment later after palyer wins to reload the frame when checkIfWin function is called
    private void calculateTheAchievements()
    {
        int achievementsCount = 0;
        if (scoreCount > baseAchievementScoreOfLevel * 1.6f) achievementsCount = 3;
        else if (scoreCount > baseAchievementScoreOfLevel * 1.3f) achievementsCount = 2;
        else achievementsCount = 1;
        for (int i = 0; i < achievementTokens.Count; i++)
        {
            if (i < achievementsCount) achievementTokens[i].GetComponent<RawImage>().color = new Color(achievementTokensColorR, 0, 0, 1);
            achievementTokens[i].SetActive(true);
        }
        if (currentLevel > achievedLevel) achievedLevel = currentLevel;
        LevelIconsClasses[currentLevel - 1].achievementsScore = achievementsCount;
        LevelIconsClasses[currentLevel - 1].setAchievements();
        LevelIconsClasses[currentLevel - 1].setHighScore(scoreCount);
        //setting active the level that next after achieved one  
        if (achievedLevel < LevelIconsClasses.Count) if (!LevelIconsClasses[achievedLevel].button.interactable) LevelIconsClasses[achievedLevel].button.interactable = true;
    }
    public void restartTheGameButton()
    {
        for (int i = 0; i < achievementTokens.Count; i++)
        {
            achievementTokens[i].GetComponent<RawImage>().color = new Color(achievementTokensColorR, 0, 0, 0.2f);
            achievementTokens[i].SetActive(false);
        }
        winLosePanel.SetActive(false);
        startTheLevel(currentLevel);
    }

    public void goToMenuButton()
    {
        for (int i = 0; i < achievementTokens.Count; i++)
        {
            achievementTokens[i].GetComponent<RawImage>().color = new Color(achievementTokensColorR, 0, 0, 0.2f);
            achievementTokens[i].SetActive(false);
        }
        winLosePanel.SetActive(false);
        platform.disactivatePlatorm();
        levelsPanel.SetActive(true);
        changeTheSpriteOfBackground(MenuBackGround);
        scoreTitle.SetActive(false);
        availableBallsPanel.SetActive(false);
    }

    #endregion


    //saving the data of game while quitting the game(from mobile platform)
    private void OnApplicationPause(bool pause)
    {
        if (pause) saveTheData();
    }

    //for UNITY editor
    private void OnApplicationQuit()
    {
        saveTheData();
    }

    private void saveTheData()
    {
        PlayerPrefs.SetInt("achievedLevel", achievedLevel);
        for (int i = 0; i < achievedLevel; i++)
        {
            PlayerPrefs.SetInt("higScore" + i, LevelIconsClasses[i].highScore);
            PlayerPrefs.SetInt("achievementsScore" + i, LevelIconsClasses[i].achievementsScore);
        }
    }
    private void LoadData()
    {
        achievedLevel = PlayerPrefs.GetInt("achievedLevel", 0);
        for (int i = 0; i < achievedLevel; i++)
        {
            LevelIconsClasses[i].highScore = PlayerPrefs.GetInt("higScore" + i, 0);
            LevelIconsClasses[i].achievementsScore = PlayerPrefs.GetInt("achievementsScore" + i, 0);
            LevelIconsClasses[i].setAchievements();
            LevelIconsClasses[i].setHighScore(LevelIconsClasses[i].highScore);
            LevelIconsClasses[i].button.interactable = true;
        }
        //setting active the level that next after achieved one  
        if (achievedLevel < LevelIconsClasses.Count)
        {
            if (!LevelIconsClasses[achievedLevel].button.interactable) LevelIconsClasses[achievedLevel].button.interactable = true;
        }
    }

}
