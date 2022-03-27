using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelParameters:MonoBehaviour
{
    public static LevelParameters current;
    private void Awake()
    {
        //creating static instance to call some properties and functions from outside directly
        current = this;
    }
    public Dictionary<Vector2, int> level1 = new Dictionary<Vector2, int>
    {
        [new Vector2(1, 1)] = 1,
        [new Vector2(1, 2)] = 1,
        [new Vector2(1, 3)] = 1,
        [new Vector2(1, 4)] = 1,
        [new Vector2(2, 1)] = 1,
        [new Vector2(2, 2)] = 1,
        [new Vector2(2, 3)] = 1,
        [new Vector2(2, 4)] = 1,
        [new Vector2(3, 2)] = 1,
        [new Vector2(3, 3)] = 1,
        [new Vector2(4, 1)] = 1,
        [new Vector2(4, 2)] = 1,
        [new Vector2(4, 3)] = 1,
        [new Vector2(4, 4)] = 1,
        [new Vector2(5, 2)] = 1,
        [new Vector2(5, 3)] = 1,
        [new Vector2(6, 1)] = 1,
        [new Vector2(6, 4)] = 1,
    };
}
