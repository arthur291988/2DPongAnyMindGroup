
using UnityEngine;

public class Platform : MonoBehaviour
{
    Camera cameraOfGame;
    private Transform platformTransform;

    private float yPositionOfPlatform;

    private float leftBorderForPlatform;
    private float rightBorderForPlatform;


    private void OnEnable()
    {
        cameraOfGame = Camera.main;
        platformTransform = transform;
        yPositionOfPlatform = platformTransform.position.y;

        //fixing the movemet limit points on X axis for platform
        leftBorderForPlatform = -GameManager.horisScreenSize / 2 + platformTransform.localScale.x/2;
        rightBorderForPlatform = GameManager.horisScreenSize / 2 - platformTransform.localScale.x / 2;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            //moving the platform with touch/mouse position if it is held down/touched
            platformTransform.position = new Vector3 (cameraOfGame.ScreenToWorldPoint(Input.mousePosition).x, yPositionOfPlatform, 0);

            //holding the platform inside the screen borders
            if (platformTransform.position.x < leftBorderForPlatform) platformTransform.position = new Vector3(leftBorderForPlatform, yPositionOfPlatform, 0);
            if (platformTransform.position.x > rightBorderForPlatform) platformTransform.position = new Vector3(rightBorderForPlatform, yPositionOfPlatform, 0);
        }
    }

}
