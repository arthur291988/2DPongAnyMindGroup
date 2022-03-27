using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaBonus : MonoBehaviour
{
    Transform megaBonusTransform;
    private void OnEnable()
    {
        megaBonusTransform = transform;
    }
    // Update is called once per frame
    void Update()
    {
        megaBonusTransform.Translate(new Vector2(0,-10f)*Time.deltaTime, Space.World);
        if (megaBonusTransform.position.y < -GameManager.current.vertScreenSize / 2 - 1) gameObject.SetActive(false);
    }
}
