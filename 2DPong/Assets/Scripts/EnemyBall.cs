using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBall : MonoBehaviour
{
    public byte ballLevel;
    private Transform ballTransform;
    private Rigidbody2D ballRigidbody;
    private const float BALL_ROTATION_SPEED = 1800;
    private void OnEnable()
    {
        ballTransform = transform;
        if (ballRigidbody==null) ballRigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(disactivateInTime());
    }

    IEnumerator disactivateInTime() {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ballTransform.position.y < -GameManager.current.vertScreenSize / 2 - 1)
        {
            StopCoroutine(disactivateInTime());
            gameObject.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        ballRigidbody.MoveRotation(ballRigidbody.rotation - BALL_ROTATION_SPEED * Time.fixedDeltaTime);
    }

}
