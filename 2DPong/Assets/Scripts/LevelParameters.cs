
using System.Collections.Generic;
using UnityEngine;

public class LevelParameters : MonoBehaviour
{
    //position indexes of enemies on game scene board, with the levels of enemies
    public static Dictionary<Vector2, int> level1 = new Dictionary<Vector2, int>
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
    public static Dictionary<Vector2, int> level2 = new Dictionary<Vector2, int>
    {
        [new Vector2(1, 1)] = 2,
        [new Vector2(1, 2)] = 2,
        [new Vector2(1, 3)] = 2,
        [new Vector2(1, 4)] = 2,

        [new Vector2(2, 2)] = 1,
        [new Vector2(2, 3)] = 1,

        [new Vector2(3, 2)] = 2,
        [new Vector2(3, 3)] = 2,

        [new Vector2(4, 1)] = 1,
        [new Vector2(4, 2)] = 1,
        [new Vector2(4, 3)] = 1,
        [new Vector2(4, 4)] = 1,

        [new Vector2(5, 1)] = 1,
        [new Vector2(5, 2)] = 1,
        [new Vector2(5, 3)] = 1,
        [new Vector2(5, 4)] = 1,

        [new Vector2(6, 2)] = 1,
        [new Vector2(6, 3)] = 1,
    };
    public static Dictionary<Vector2, int> level3 = new Dictionary<Vector2, int>
    {
        [new Vector2(1, 1)] = 2,
        [new Vector2(1, 2)] = 2,
        [new Vector2(1, 3)] = 2,
        [new Vector2(1, 4)] = 2,

        [new Vector2(2, 2)] = 1,
        [new Vector2(2, 3)] = 1,

        [new Vector2(3, 1)] = 2,
        [new Vector2(3, 2)] = 2,
        [new Vector2(3, 3)] = 2,
        [new Vector2(3, 4)] = 2,

        [new Vector2(4, 2)] = 2,
        [new Vector2(4, 3)] = 2,

        [new Vector2(5, 1)] = 1,
        [new Vector2(5, 2)] = 1,
        [new Vector2(5, 3)] = 1,
        [new Vector2(5, 4)] = 1,

        [new Vector2(6, 1)] = 2,
        [new Vector2(6, 4)] = 2,
    };
    public static Dictionary<Vector2, int> level4 = new Dictionary<Vector2, int>
    {
        [new Vector2(1, 1)] = 1,
        [new Vector2(1, 2)] = 3,
        [new Vector2(1, 3)] = 3,
        [new Vector2(1, 4)] = 1,

        [new Vector2(2, 2)] = 2,
        [new Vector2(2, 3)] = 2,

        [new Vector2(3, 2)] = 3,
        [new Vector2(3, 3)] = 3,

        [new Vector2(4, 1)] = 2,
        [new Vector2(4, 2)] = 2,
        [new Vector2(4, 3)] = 2,
        [new Vector2(4, 4)] = 2,

        [new Vector2(5, 2)] = 1,
        [new Vector2(5, 3)] = 1,

        [new Vector2(6, 1)] = 2,
        [new Vector2(6, 2)] = 2,
        [new Vector2(6, 3)] = 2,
        [new Vector2(6, 4)] = 2,
    };
    public static Dictionary<Vector2, int> level5 = new Dictionary<Vector2, int>
    {
        [new Vector2(1, 1)] = 3,
        [new Vector2(1, 2)] = 3,
        [new Vector2(1, 3)] = 3,
        [new Vector2(1, 4)] = 3,

        [new Vector2(2, 1)] = 1,
        [new Vector2(2, 2)] = 3,
        [new Vector2(2, 3)] = 3,
        [new Vector2(2, 4)] = 1,

        [new Vector2(3, 1)] = 1,
        [new Vector2(3, 2)] = 3,
        [new Vector2(3, 3)] = 3,
        [new Vector2(3, 4)] = 1,

        [new Vector2(4, 1)] = 2,
        [new Vector2(4, 2)] = 2,
        [new Vector2(4, 3)] = 2,
        [new Vector2(4, 4)] = 2,

        [new Vector2(5, 2)] = 3,
        [new Vector2(5, 3)] = 3,

        [new Vector2(6, 1)] = 1,
        [new Vector2(6, 2)] = 1,
        [new Vector2(6, 3)] = 1,
        [new Vector2(6, 4)] = 1,
    };
    public static Dictionary<Vector2, int> level6 = new Dictionary<Vector2, int>
    {
        [new Vector2(1, 1)] = 3,
        [new Vector2(1, 2)] = 3,
        [new Vector2(1, 3)] = 3,
        [new Vector2(1, 4)] = 3,

        [new Vector2(2, 1)] = 3,
        [new Vector2(2, 2)] = 3,
        [new Vector2(2, 3)] = 3,
        [new Vector2(2, 4)] = 3,

        [new Vector2(3, 1)] = 1,
        [new Vector2(3, 2)] = 1,
        [new Vector2(3, 3)] = 1,
        [new Vector2(3, 4)] = 1,

        [new Vector2(4, 1)] = 2,
        [new Vector2(4, 2)] = 2,
        [new Vector2(4, 3)] = 2,
        [new Vector2(4, 4)] = 2,

        [new Vector2(5, 1)] = 2,
        [new Vector2(5, 2)] = 2,
        [new Vector2(5, 3)] = 2,
        [new Vector2(5, 4)] = 2,

        [new Vector2(6, 1)] = 2,
        [new Vector2(6, 2)] = 2,
        [new Vector2(6, 3)] = 2,
        [new Vector2(6, 4)] = 2,
    };

    //all levels to call them from GameManager while clicking the start icon buttons. Buttons have int parameters that correspond to game level.  
    public static Dictionary<int, Dictionary<Vector2, int>> allLevels = new Dictionary<int, Dictionary<Vector2, int>>
    {
        [1] = level1,
        [2] = level2,
        [3] = level3,
        [4] = level4,
        [5] = level5,
        [6] = level6,
    };
}
