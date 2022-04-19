using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using UnityEngine.Advertisements;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour, IUnityAdsListener
{
    private string gameId = "4009839";
    private string mySurfacingId = "rewardedVideo";
    private string myInterstitialId = "video";
    private bool testMode = false;

    private const string leaderBoard = "CgkI7f-olcgcEAIQAQ";

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

    public Platform platformSmall;
    public Platform platformBig;
    [HideInInspector]
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
    private const float BALL_MOVE_SPEED_2_LEVEL = 11;
    private const float BALL_MOVE_SPEED_3_LEVEL = 12;
    private const float BALL_MOVE_SPEED_4_LEVEL = 13;
    private const float BALL_MOVE_SPEED_5_LEVEL = 14;
    private const float BALL_MOVE_SPEED_6_LEVEL = 15;
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
    [HideInInspector]
    public List<Enemy> all4LevelEnemies;
    [HideInInspector]
    public List<Enemy> all5LevelEnemies;

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
    [SerializeField]
    private GameObject levelTextGO;
    private Text levelText;
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
    private Text totalScore;

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

    [SerializeField]
    private GameObject marketPanel;
    [SerializeField]
    private List<GameObject> MenuObjects;

    [SerializeField]
    private List<Button> marketButtons;

    [SerializeField]
    private SpriteAtlas BallSprites;
    private Image menuBallImage;
    private SpriteRenderer ballSpriteRenderer;
    private int ballSkinIndex;
    private List<int> ballSkins;
    private List<int> boughtStuff;
    [HideInInspector]
    public int adsOff;

    public AudioSource surikenSound;
    public AudioSource ninjaOuchSound;
    public AudioSource ninjaDestroySound;
    public AudioSource megaBallSound;
    public AudioSource iceEffectSound;
    public AudioSource gameEndSound;
    public AudioSource slowDownSound;



    private void Awake()
    {
        //creating static instance to call some properties and functions from outside directly
        current = this;
        ballRotationSpeed = BALL_ROTATION_SPEED;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate(success=> {
            if (success) { }
            else { }
        });

        levelText = levelTextGO.GetComponent<Text>();
        adsOff = 0;
        ballSkins = new List<int> { 1, 2,};
        boughtStuff = new List<int>();
        achievedLevel = 0;
        indexOfCurrentBall = 2;
        all3LevelEnemies = new List<Enemy>();
        all2LevelEnemies = new List<Enemy>();
        all4LevelEnemies = new List<Enemy>();
        all5LevelEnemies = new List<Enemy>();
        allPositionsForEnemiesWithIndexes = new Dictionary<Vector2, Vector2>();
        allEnemiesWithPositionIndexes = new Dictionary<Vector2, Enemy>();
        gameIsOn = false;
        cameraOfGame = Camera.main;
        megaBallTransform = megaBall.transform;
        menuBallImage = MenuObjects[0].GetComponent<Image>();
        ballSpriteRenderer = ball.GetComponent<SpriteRenderer>();

        //determine the sizes of view screen
        vertScreenSize = cameraOfGame.orthographicSize * 2;
        horisScreenSize = vertScreenSize * Screen.width / Screen.height;

        changeTheSpriteOfBackground(MenuBackGround);
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.GetInt("saved", 0)>0) LoadData();
        changeBallSkin(true); //set the skin of ball the was saved
        reviseBoughtStuff();
        setTheBordersToScreenFrame();
        setAllEnemyPositions();
        levelsPanel.SetActive(true);
        updateTotalScore();
        StartCoroutine(setTheMarketPrices());

        initializeAds();
    }

    //game monetization functions
    #region 
    private IEnumerator setTheMarketPrices()
    {
        while (!IAPMgr.Instance.IsInitialized()) yield return null;

        if (marketButtons[1].interactable) marketButtons[1].transform.GetChild(0).GetComponent<Text>().text = IAPMgr.Instance.getProductPriceFromStore(IAPMgr.Instance.no_ads);
        if (marketButtons[2].interactable) marketButtons[2].transform.GetChild(0).GetComponent<Text>().text = IAPMgr.Instance.getProductPriceFromStore(IAPMgr.Instance.skin_3);
        if (marketButtons[3].interactable) marketButtons[3].transform.GetChild(0).GetComponent<Text>().text = IAPMgr.Instance.getProductPriceFromStore(IAPMgr.Instance.skin_4);
    }

    private void initializeAds()
    { 
        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        if (Advertisement.isSupported /*&& Application.platform == RuntimePlatform.Android*/)
        {
            Advertisement.Initialize(gameId, testMode);
        }
    }

    private void showInterstitials()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show(myInterstitialId);
        }
    }
    public void ShowRewardedVideo()
    {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady(mySurfacingId))
        {
            Advertisement.Show(mySurfacingId);
        }
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string surfacingId)
    {
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string surfacingId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            ballSkins.Add(3);
            marketButtons[0].interactable = false;
            marketButtons[0].GetComponentInChildren<Text>().text = "V";
            boughtStuff.Add(0);
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
        }
    }

    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidStart(string surfacingId)
    {
    }

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }
    #endregion

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
        if (level > 8) changeTheSpriteOfBackground(Level56BackGroundNameInAtlas);
        else if (level > 4) changeTheSpriteOfBackground(Level34BackGroundNameInAtlas);
        else changeTheSpriteOfBackground(Level12BackGroundNameInAtlas);
        indexOfCurrentBall = 2;
        for (int i = 0; i < availableBalls.Count; i++) if (!availableBalls[i].activeInHierarchy) availableBalls[i].SetActive(true);
        currentLevel = level;
        ballMoveSpeed = level > 12 ? BALL_MOVE_SPEED_6_LEVEL : level > 10 ? BALL_MOVE_SPEED_5_LEVEL : level > 7 ? BALL_MOVE_SPEED_4_LEVEL : level > 5 ? BALL_MOVE_SPEED_3_LEVEL : level > 2 ? BALL_MOVE_SPEED_2_LEVEL : BALL_MOVE_SPEED_1_LEVEL;
        allEnemiesWithPositionIndexes.Clear();
        all2LevelEnemies.Clear();
        all3LevelEnemies.Clear();
        all4LevelEnemies.Clear();
        all5LevelEnemies.Clear();
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
        if (levelsPanel.activeInHierarchy)
        {
            levelsPanel.SetActive(false);
            activateAllMenuObject(false);
        }
        levelText.text = currentLevel.ToString();
        levelTextGO.SetActive(true);
        scoreTitle.SetActive(true);
        availableBallsPanel.SetActive(true);
    }

    private void activateThePlatform()
    {
        platform = platformBig; //from the beginning the platform is equal to big platform
        platform.gameManager = this;
        platform.transform.position = new Vector2(0, -vertScreenSize / 2 + platform.transform.localScale.y / 2 + PLATFORM_DISTANCE_FROM_BOTTOM);
        platform.leftBorderForPlatform = -horisScreenSize / 2 + LeftBorder.transform.localScale.x / 2 + platform.transform.localScale.x / 2;
        platform.rightBorderForPlatform = horisScreenSize / 2 - RightBorder.transform.localScale.x / 2 - platform.transform.localScale.x / 2;
        platform.gameObject.SetActive(true); 
        
        //setting the small platform as well on start of game
        platformSmall.gameManager = this;
        platformSmall.transform.position = new Vector2(0, -vertScreenSize / 2 + platformSmall.transform.localScale.y / 2 + PLATFORM_DISTANCE_FROM_BOTTOM);
        platformSmall.leftBorderForPlatform = -horisScreenSize / 2 + LeftBorder.transform.localScale.x / 2 + platformSmall.transform.localScale.x / 2;
        platformSmall.rightBorderForPlatform = horisScreenSize / 2 - RightBorder.transform.localScale.x / 2 - platformSmall.transform.localScale.x / 2;
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
                if (enemy.enemyLevel == 4) all4LevelEnemies.Add(enemy);
                if (enemy.enemyLevel == 5) all5LevelEnemies.Add(enemy);
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

    private void updateTotalScore()
    {
        int score = 0;
        for (int i = 0; i < LevelIconsClasses.Count; i++)
        {
            score += LevelIconsClasses[i].highScore;
        }
        totalScore.text = score.ToString();
        Social.ReportScore(score,leaderBoard, (bool success)=>{ });
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

    public void enemyAttacks(byte step)
    {
        if (step == 3)
        {
            //pick one enenmy to attack the player
            if (Random.Range(0, 2) > 0)
            {
                if (all5LevelEnemies.Count > 0)
                {
                    if (all5LevelEnemies.Count == 1) all5LevelEnemies[0].attackWithBall();
                    else all5LevelEnemies[Random.Range(0, all5LevelEnemies.Count)].attackWithBall();
                }
                else if (all4LevelEnemies.Count > 0)
                {
                    if (all4LevelEnemies.Count == 1) all4LevelEnemies[0].attackWithBall();
                    else all4LevelEnemies[Random.Range(0, all4LevelEnemies.Count)].attackWithBall();
                }
            }
            else if (all4LevelEnemies.Count > 0)
            {
                if (all4LevelEnemies.Count == 1) all4LevelEnemies[0].attackWithBall();
                else all4LevelEnemies[Random.Range(0, all4LevelEnemies.Count)].attackWithBall();
            }
        }

        else if (step == 2) {
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
        //LevelIconsClasses[currentLevel - 1].achievementsScore = achievementsCount;
        LevelIconsClasses[currentLevel - 1].setAchievements(achievementsCount);
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
        showInterstitials();
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
        updateTotalScore();
        levelsPanel.SetActive(true);
        activateAllMenuObject(true);
        changeTheSpriteOfBackground(MenuBackGround);
        levelTextGO.SetActive(false);
        scoreTitle.SetActive(false);
        availableBallsPanel.SetActive(false);
        showInterstitials();
    }

    #endregion


    public void openCloseMarketPanel(bool open) {
        if (open) marketPanel.SetActive(true);
        else marketPanel.SetActive(false);
    }

    private void activateAllMenuObject(bool activate)
    {
        foreach (GameObject go in MenuObjects) go.SetActive(activate);
    }

    public void changeBallSkin(bool callFromStart)
    {
        if (!callFromStart)
        {
            ballSkinIndex++;
        }
        string skin;
        if (ballSkinIndex < ballSkins.Count) skin = ballSkins[ballSkinIndex].ToString();
        else
        {
            skin = "1";
            ballSkinIndex = 0;
        }
        ballSpriteRenderer.sprite = BallSprites.GetSprite(skin);
        menuBallImage.sprite = BallSprites.GetSprite(skin);
    }

    private void reviseBoughtStuff()
    {
        for (int i = 0; i < boughtStuff.Count; i++) {
            marketButtons[boughtStuff[i]].interactable = false;
            marketButtons[boughtStuff[i]].GetComponentInChildren<Text>().text = "V";
        }
    }

    public void buyTheStuff(int stuffNumber) {
        if (stuffNumber == 0)
        {
            ShowRewardedVideo();
        }
        else if (stuffNumber == 1)
        {
            IAPMgr.Instance.BuyNoAds();
        }
        else if (stuffNumber == 2)
        {
            IAPMgr.Instance.BuySkin3();
        }
        else if (stuffNumber == 3)
        {
            IAPMgr.Instance.BuySkin4();
        }
    }

    public void processThePurchase(int stuffNumber)
    {
        if (stuffNumber == 1)
        {
            adsOff = 1;
            marketButtons[stuffNumber].interactable = false;
            marketButtons[stuffNumber].GetComponentInChildren<Text>().text = "V";
            boughtStuff.Add(stuffNumber);
        }
        else if (stuffNumber == 2)
        {
            ballSkins.Add(4);
            marketButtons[stuffNumber].interactable = false;
            marketButtons[stuffNumber].GetComponentInChildren<Text>().text = "V";
            boughtStuff.Add(stuffNumber);
        }
        else if (stuffNumber == 3)
        {
            ballSkins.Add(5);
            marketButtons[stuffNumber].interactable = false;
            marketButtons[stuffNumber].GetComponentInChildren<Text>().text = "V";
            boughtStuff.Add(stuffNumber);
        }
    }

    public void showLeaderBoards() {
        Social.ShowLeaderboardUI();
    }


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

    public void saveTheData()
    {
        PlayerPrefs.SetInt("achievedLevel", achievedLevel);
        PlayerPrefs.SetInt("ballSkinIndex", ballSkinIndex);
        PlayerPrefs.SetInt("adsOff", adsOff);

        PlayerPrefs.SetInt("ballSkinsCount", ballSkins.Count);
        for (int i = 0; i < ballSkins.Count; i++) PlayerPrefs.SetInt("ballSkins" + i, ballSkins[i]);
        PlayerPrefs.SetInt("boughtStuffCount", boughtStuff.Count);
        for (int i = 0; i < boughtStuff.Count; i++) PlayerPrefs.SetInt("boughtStuff" + i, boughtStuff[i]);
        for (int i = 0; i < achievedLevel; i++)
        {
            PlayerPrefs.SetInt("higScore" + i, LevelIconsClasses[i].highScore);
            PlayerPrefs.SetInt("achievementsScore" + i, LevelIconsClasses[i].achievementsScore);
        }
        PlayerPrefs.SetInt("saved",1);
    }
    private void LoadData()
    {
        achievedLevel = PlayerPrefs.GetInt("achievedLevel", 0);
        ballSkinIndex = PlayerPrefs.GetInt("ballSkinIndex", 0);
        adsOff = PlayerPrefs.GetInt("adsOff", 0);
        for (int i = 0; i < achievedLevel; i++)
        {
            LevelIconsClasses[i].highScore = PlayerPrefs.GetInt("higScore" + i, 0);
            LevelIconsClasses[i].achievementsScore = PlayerPrefs.GetInt("achievementsScore" + i, 0);
            LevelIconsClasses[i].setAchievements(LevelIconsClasses[i].achievementsScore);
            LevelIconsClasses[i].setHighScore(LevelIconsClasses[i].highScore);
            LevelIconsClasses[i].button.interactable = true;
        }

        int ballSkinsCount = PlayerPrefs.GetInt("ballSkinsCount", 0);
        ballSkins.Clear();
        for (int i = 0; i < ballSkinsCount; i++) ballSkins.Add(PlayerPrefs.GetInt("ballSkins"+i, 0));

        int boughtStuffCount = PlayerPrefs.GetInt("boughtStuffCount", 0);
        for (int i = 0; i < boughtStuffCount; i++) boughtStuff.Add(PlayerPrefs.GetInt("boughtStuff" + i, 0));

        //setting active the level that next after achieved one  
        if (achievedLevel < LevelIconsClasses.Count)
        {
            if (!LevelIconsClasses[achievedLevel].button.interactable) LevelIconsClasses[achievedLevel].button.interactable = true;
        }
    }

}
