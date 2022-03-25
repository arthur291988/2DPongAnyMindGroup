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
    private GameObject Platform;
    [SerializeField]
    private GameObject Ball;

    public static float vertScreenSize;
    public static float horisScreenSize;

    private const float PLATFORM_DISTANCE_FROM_BOTTOM = 5;

    // Start is called before the first frame update
    void Start()
    {
        cameraOfGame = Camera.main;
        //determine the sizes of view screen
        vertScreenSize = cameraOfGame.orthographicSize*2;
        horisScreenSize = vertScreenSize * Screen.width / Screen.height;
        setTheBordersToScreenFrame();
        activateThePlatform();
        activateTheBall();
    }

    private void setTheBordersToScreenFrame()
    {
        LeftBorder.transform.position = new Vector3(-horisScreenSize / 2 - LeftBorder.transform.localScale.x / 2, 0, 0);
        RightBorder.transform.position = new Vector3(horisScreenSize / 2 + RightBorder.transform.localScale.x / 2, 0, 0);
        TopBorder.transform.position = new Vector3(0, vertScreenSize / 2 + TopBorder.transform.localScale.y / 2, 0);
    }

    private void activateThePlatform()
    {
        Platform.transform.position = new Vector3(0, -vertScreenSize / 2 + Platform.transform.localScale.y / 2 + PLATFORM_DISTANCE_FROM_BOTTOM, 0);
        Platform.SetActive(true);
    }

    private void activateTheBall() {
        Ball.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
