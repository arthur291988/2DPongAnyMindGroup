
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
        [new Vector2(1, 1)] = 1,
        [new Vector2(1, 2)] = 2,
        [new Vector2(1, 3)] = 2,
        [new Vector2(1, 4)] = 1,

        [new Vector2(2, 2)] = 1,
        [new Vector2(2, 3)] = 1,

        [new Vector2(3, 2)] = 1,
        [new Vector2(3, 3)] = 1,

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

        [new Vector2(3, 1)] = 1,
        [new Vector2(3, 2)] = 2,
        [new Vector2(3, 3)] = 2,
        [new Vector2(3, 4)] = 1,

        [new Vector2(4, 2)] = 2,
        [new Vector2(4, 3)] = 2,

        [new Vector2(5, 1)] = 1,
        [new Vector2(5, 2)] = 1,
        [new Vector2(5, 3)] = 1,
        [new Vector2(5, 4)] = 1,

        [new Vector2(6, 1)] = 1,
        [new Vector2(6, 4)] = 1,
    };
    public static Dictionary<Vector2, int> level4 = new Dictionary<Vector2, int>
    {
        [new Vector2(1, 1)] = 2,
        [new Vector2(1, 2)] = 2,
        [new Vector2(1, 3)] = 2,
        [new Vector2(1, 4)] = 2,

        [new Vector2(2, 2)] = 2,
        [new Vector2(2, 3)] = 2,

        [new Vector2(3, 2)] = 2,
        [new Vector2(3, 3)] = 2,

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
        [new Vector2(1, 1)] = 2,
        [new Vector2(1, 2)] = 3,
        [new Vector2(1, 3)] = 3,
        [new Vector2(1, 4)] = 2,

        [new Vector2(2, 1)] = 2,
        [new Vector2(2, 2)] = 2,
        [new Vector2(2, 3)] = 2,
        [new Vector2(2, 4)] = 2,

        [new Vector2(3, 1)] = 1,
        [new Vector2(3, 2)] = 2,
        [new Vector2(3, 3)] = 2,
        [new Vector2(3, 4)] = 1,

        [new Vector2(4, 1)] = 2,
        [new Vector2(4, 2)] = 2,
        [new Vector2(4, 3)] = 2,
        [new Vector2(4, 4)] = 2,

        [new Vector2(5, 2)] = 2,
        [new Vector2(5, 3)] = 2,

        [new Vector2(6, 1)] = 1,
        [new Vector2(6, 2)] = 1,
        [new Vector2(6, 3)] = 1,
        [new Vector2(6, 4)] = 1,
    };
    public static Dictionary<Vector2, int> level6 = new Dictionary<Vector2, int>
    {
        [new Vector2(1, 1)] = 2,
        [new Vector2(1, 2)] = 3,
        [new Vector2(1, 3)] = 3,
        [new Vector2(1, 4)] = 2,

        [new Vector2(2, 1)] = 3,
        [new Vector2(2, 2)] = 2,
        [new Vector2(2, 3)] = 2,
        [new Vector2(2, 4)] = 3,

        [new Vector2(3, 1)] = 2,
        [new Vector2(3, 2)] = 1,
        [new Vector2(3, 3)] = 1,
        [new Vector2(3, 4)] = 2,

        [new Vector2(4, 1)] = 2,
        [new Vector2(4, 2)] = 2,
        [new Vector2(4, 3)] = 2,
        [new Vector2(4, 4)] = 2,

        [new Vector2(5, 1)] = 2,
        [new Vector2(5, 2)] = 2,
        [new Vector2(5, 3)] = 2,
        [new Vector2(5, 4)] = 2,

        [new Vector2(6, 1)] = 2,
        [new Vector2(6, 2)] = 1,
        [new Vector2(6, 3)] = 1,
        [new Vector2(6, 4)] = 2,
    };
    public static Dictionary<Vector2, int> level7 = new Dictionary<Vector2, int>
    {
        [new Vector2(1, 1)] = 3,
        [new Vector2(1, 2)] = 2,
        [new Vector2(1, 3)] = 1,
        [new Vector2(1, 4)] = 3,

        [new Vector2(2, 1)] = 3,
        [new Vector2(2, 2)] = 1,
        [new Vector2(2, 3)] = 1,
        [new Vector2(2, 4)] = 3,

        [new Vector2(3, 2)] = 2,
        [new Vector2(3, 3)] = 2,

        [new Vector2(4, 1)] = 1,
        [new Vector2(4, 2)] = 3,
        [new Vector2(4, 3)] = 3,
        [new Vector2(4, 4)] = 1,

        [new Vector2(5, 2)] = 2,
        [new Vector2(5, 3)] = 2,

        [new Vector2(6, 1)] = 2,
        [new Vector2(6, 4)] = 2,
    };
    public static Dictionary<Vector2, int> level8 = new Dictionary<Vector2, int>
    {
        [new Vector2(1, 1)] = 3,
        [new Vector2(1, 2)] = 3,
        [new Vector2(1, 3)] = 3,
        [new Vector2(1, 4)] = 3,

        [new Vector2(2, 2)] = 1,
        [new Vector2(2, 3)] = 1,

        [new Vector2(3, 1)] = 3,
        [new Vector2(3, 2)] = 2,
        [new Vector2(3, 3)] = 2,
        [new Vector2(3, 4)] = 3,

        [new Vector2(4, 1)] = 2,
        [new Vector2(4, 2)] = 3,
        [new Vector2(4, 3)] = 3,
        [new Vector2(4, 4)] = 2,

        [new Vector2(5, 1)] = 3,
        [new Vector2(5, 2)] = 1,
        [new Vector2(5, 3)] = 1,
        [new Vector2(5, 4)] = 3,

        [new Vector2(6, 2)] = 2,
        [new Vector2(6, 3)] = 2,
    };
    public static Dictionary<Vector2, int> level9 = new Dictionary<Vector2, int>
    {
        [new Vector2(1, 1)] = 3,
        [new Vector2(1, 2)] = 4,
        [new Vector2(1, 3)] = 4,
        [new Vector2(1, 4)] = 3,

        [new Vector2(2, 1)] = 3,
        [new Vector2(2, 2)] = 3,
        [new Vector2(2, 3)] = 3,
        [new Vector2(2, 4)] = 3,

        [new Vector2(3, 1)] = 3,
        [new Vector2(3, 2)] = 3,
        [new Vector2(3, 3)] = 3,
        [new Vector2(3, 4)] = 3,

        [new Vector2(4, 2)] = 2,
        [new Vector2(4, 3)] = 2,

        [new Vector2(5, 1)] = 2,
        [new Vector2(5, 2)] = 2,
        [new Vector2(5, 3)] = 2,
        [new Vector2(5, 4)] = 2,

        [new Vector2(6, 1)] = 2,
        [new Vector2(6, 4)] = 2,
    };
    public static Dictionary<Vector2, int> level10 = new Dictionary<Vector2, int>
    {
        [new Vector2(1, 1)] = 2,
        [new Vector2(1, 2)] = 3,
        [new Vector2(1, 3)] = 3,
        [new Vector2(1, 4)] = 2,

        [new Vector2(2, 2)] = 4,
        [new Vector2(2, 3)] = 4,

        [new Vector2(3, 1)] = 4,
        [new Vector2(3, 2)] = 3,
        [new Vector2(3, 3)] = 3,
        [new Vector2(3, 4)] = 4,

        [new Vector2(4, 1)] = 2,
        [new Vector2(4, 2)] = 2,
        [new Vector2(4, 3)] = 2,
        [new Vector2(4, 4)] = 2,

        [new Vector2(5, 2)] = 3,
        [new Vector2(5, 3)] = 3,

        [new Vector2(6, 1)] = 3,
        [new Vector2(6, 2)] = 3,
        [new Vector2(6, 3)] = 3,
        [new Vector2(6, 4)] = 3,
    };
    public static Dictionary<Vector2, int> level11 = new Dictionary<Vector2, int>
    {
        [new Vector2(1, 1)] = 4,
        [new Vector2(1, 2)] = 4,
        [new Vector2(1, 3)] = 4,
        [new Vector2(1, 4)] = 4,

        [new Vector2(2, 1)] = 3,
        [new Vector2(2, 2)] = 3,
        [new Vector2(2, 3)] = 3,
        [new Vector2(2, 4)] = 3,

        [new Vector2(3, 1)] = 3,
        [new Vector2(3, 2)] = 3,
        [new Vector2(3, 3)] = 3,
        [new Vector2(3, 4)] = 3,

        [new Vector2(4, 1)] = 2,
        [new Vector2(4, 2)] = 4,
        [new Vector2(4, 3)] = 4,
        [new Vector2(4, 4)] = 2,

        [new Vector2(5, 2)] = 2,
        [new Vector2(5, 3)] = 2,

        [new Vector2(6, 1)] = 4,
        [new Vector2(6, 2)] = 3,
        [new Vector2(6, 3)] = 3,
        [new Vector2(6, 4)] = 4,
    };
    public static Dictionary<Vector2, int> level12 = new Dictionary<Vector2, int>
    {
        [new Vector2(1, 1)] = 3,
        [new Vector2(1, 2)] = 5,
        [new Vector2(1, 3)] = 5,
        [new Vector2(1, 4)] = 3,

        [new Vector2(2, 1)] = 3,
        [new Vector2(2, 2)] = 3,
        [new Vector2(2, 3)] = 3,
        [new Vector2(2, 4)] = 3,

        [new Vector2(3, 1)] = 1,
        [new Vector2(3, 2)] = 4,
        [new Vector2(3, 3)] = 4,
        [new Vector2(3, 4)] = 1,

        [new Vector2(4, 1)] = 4,
        [new Vector2(4, 2)] = 1,
        [new Vector2(4, 3)] = 1,
        [new Vector2(4, 4)] = 4,

        [new Vector2(5, 1)] = 3,
        [new Vector2(5, 2)] = 4,
        [new Vector2(5, 3)] = 4,
        [new Vector2(5, 4)] = 3,

        [new Vector2(6, 1)] = 2,
        [new Vector2(6, 2)] = 2,
        [new Vector2(6, 3)] = 2,
        [new Vector2(6, 4)] = 2,
    };
    public static Dictionary<Vector2, int> level13 = new Dictionary<Vector2, int>
    {
        [new Vector2(1, 1)] = 5,
        [new Vector2(1, 2)] = 4,
        [new Vector2(1, 3)] = 4,
        [new Vector2(1, 4)] = 5,

        [new Vector2(2, 1)] = 5,
        [new Vector2(2, 2)] = 2,
        [new Vector2(2, 3)] = 2,
        [new Vector2(2, 4)] = 5,

        [new Vector2(3, 2)] = 3,
        [new Vector2(3, 3)] = 3,

        [new Vector2(4, 1)] = 3,
        [new Vector2(4, 2)] = 1,
        [new Vector2(4, 3)] = 1,
        [new Vector2(4, 4)] = 3,

        [new Vector2(5, 2)] = 2,
        [new Vector2(5, 3)] = 2,

        [new Vector2(6, 1)] = 2,
        [new Vector2(6, 4)] = 3,
    };
    public static Dictionary<Vector2, int> level14 = new Dictionary<Vector2, int>
    {
        [new Vector2(1, 1)] = 5,
        [new Vector2(1, 2)] = 5,
        [new Vector2(1, 3)] = 5,
        [new Vector2(1, 4)] = 5,

        [new Vector2(2, 2)] = 4,
        [new Vector2(2, 3)] = 4,

        [new Vector2(3, 2)] = 2,
        [new Vector2(3, 3)] = 1,

        [new Vector2(4, 1)] = 1,
        [new Vector2(4, 2)] = 3,
        [new Vector2(4, 3)] = 3,
        [new Vector2(4, 4)] = 1,

        [new Vector2(5, 1)] = 2,
        [new Vector2(5, 2)] = 2,
        [new Vector2(5, 3)] = 2,
        [new Vector2(5, 4)] = 2,

        [new Vector2(6, 2)] = 4,
        [new Vector2(6, 3)] = 4,
    };
    public static Dictionary<Vector2, int> level15 = new Dictionary<Vector2, int>
    {
        [new Vector2(1, 1)] = 5,
        [new Vector2(1, 2)] = 3,
        [new Vector2(1, 3)] = 4,
        [new Vector2(1, 4)] = 2,

        [new Vector2(2, 2)] = 4,
        [new Vector2(2, 3)] = 4,

        [new Vector2(3, 1)] = 4,
        [new Vector2(3, 2)] = 4,
        [new Vector2(3, 3)] = 4,
        [new Vector2(3, 4)] = 4,

        [new Vector2(4, 2)] = 2,
        [new Vector2(4, 3)] = 2,

        [new Vector2(5, 1)] = 3,
        [new Vector2(5, 2)] = 1,
        [new Vector2(5, 3)] = 1,
        [new Vector2(5, 4)] = 3,

        [new Vector2(6, 1)] = 2,
        [new Vector2(6, 4)] = 2,
    };
    public static Dictionary<Vector2, int> level16 = new Dictionary<Vector2, int>
    {
        [new Vector2(1, 1)] = 5,
        [new Vector2(1, 2)] = 5,
        [new Vector2(1, 3)] = 5,
        [new Vector2(1, 4)] = 5,

        [new Vector2(2, 1)] = 4,
        [new Vector2(2, 2)] = 5,
        [new Vector2(2, 3)] = 5,
        [new Vector2(2, 4)] = 4,

        [new Vector2(3, 1)] = 5,
        [new Vector2(3, 2)] = 5,
        [new Vector2(3, 3)] = 5,
        [new Vector2(3, 4)] = 5,

        [new Vector2(4, 1)] = 4,
        [new Vector2(4, 2)] = 4,
        [new Vector2(4, 3)] = 4,
        [new Vector2(4, 4)] = 4,

        [new Vector2(5, 1)] = 5,
        [new Vector2(5, 2)] = 5,
        [new Vector2(5, 3)] = 5,
        [new Vector2(5, 4)] = 5,

        [new Vector2(6, 1)] = 4,
        [new Vector2(6, 2)] = 3,
        [new Vector2(6, 3)] = 3,
        [new Vector2(6, 4)] = 4,
    };
    //public static Dictionary<Vector2, int> level17 = new Dictionary<Vector2, int>
    //{
    //    [new Vector2(1, 1)] = 4,
    //    [new Vector2(1, 2)] = 4,
    //    [new Vector2(1, 3)] = 4,
    //    [new Vector2(1, 4)] = 4,

    //    [new Vector2(2, 1)] = 3,
    //    [new Vector2(2, 2)] = 3,
    //    [new Vector2(2, 3)] = 3,
    //    [new Vector2(2, 4)] = 3,

    //    [new Vector2(3, 1)] = 3,
    //    [new Vector2(3, 2)] = 3,
    //    [new Vector2(3, 3)] = 3,
    //    [new Vector2(3, 4)] = 3,

    //    [new Vector2(4, 1)] = 4,
    //    [new Vector2(4, 2)] = 4,
    //    [new Vector2(4, 3)] = 4,
    //    [new Vector2(4, 4)] = 4,

    //    [new Vector2(5, 2)] = 3,
    //    [new Vector2(5, 3)] = 3,

    //    [new Vector2(6, 1)] = 3,
    //    [new Vector2(6, 2)] = 3,
    //    [new Vector2(6, 3)] = 3,
    //    [new Vector2(6, 4)] = 3,
    //};
    //public static Dictionary<Vector2, int> level18 = new Dictionary<Vector2, int>
    //{
    //    [new Vector2(1, 1)] = 3,
    //    [new Vector2(1, 2)] = 5,
    //    [new Vector2(1, 3)] = 5,
    //    [new Vector2(1, 4)] = 3,

    //    [new Vector2(2, 1)] = 4,
    //    [new Vector2(2, 2)] = 4,
    //    [new Vector2(2, 3)] = 4,
    //    [new Vector2(2, 4)] = 4,

    //    [new Vector2(3, 1)] = 5,
    //    [new Vector2(3, 2)] = 2,
    //    [new Vector2(3, 3)] = 2,
    //    [new Vector2(3, 4)] = 5,

    //    [new Vector2(4, 1)] = 2,
    //    [new Vector2(4, 2)] = 2,
    //    [new Vector2(4, 3)] = 2,
    //    [new Vector2(4, 4)] = 2,

    //    [new Vector2(5, 1)] = 1,
    //    [new Vector2(5, 2)] = 1,
    //    [new Vector2(5, 3)] = 1,
    //    [new Vector2(5, 4)] = 1,

    //    [new Vector2(6, 1)] = 2,
    //    [new Vector2(6, 2)] = 2,
    //    [new Vector2(6, 3)] = 2,
    //    [new Vector2(6, 4)] = 2,
    //};
    //public static Dictionary<Vector2, int> level19 = new Dictionary<Vector2, int>
    //{
    //    [new Vector2(1, 1)] = 5,
    //    [new Vector2(1, 2)] = 4,
    //    [new Vector2(1, 3)] = 4,
    //    [new Vector2(1, 4)] = 5,

    //    [new Vector2(2, 1)] = 3,
    //    [new Vector2(2, 2)] = 3,
    //    [new Vector2(2, 3)] = 3,
    //    [new Vector2(2, 4)] = 3,

    //    [new Vector2(3, 2)] = 2,
    //    [new Vector2(3, 3)] = 2,

    //    [new Vector2(4, 1)] = 3,
    //    [new Vector2(4, 2)] = 4,
    //    [new Vector2(4, 3)] = 4,
    //    [new Vector2(4, 4)] = 2,

    //    [new Vector2(5, 2)] = 4,
    //    [new Vector2(5, 3)] = 4,

    //    [new Vector2(6, 1)] = 2,
    //    [new Vector2(6, 4)] = 4,
    //};
    //public static Dictionary<Vector2, int> level20 = new Dictionary<Vector2, int>
    //{
    //    [new Vector2(1, 1)] = 4,
    //    [new Vector2(1, 2)] = 4,
    //    [new Vector2(1, 3)] = 4,
    //    [new Vector2(1, 4)] = 4,

    //    [new Vector2(2, 2)] = 5,
    //    [new Vector2(2, 3)] = 5,

    //    [new Vector2(3, 2)] = 5,
    //    [new Vector2(3, 3)] = 5,

    //    [new Vector2(4, 1)] = 3,
    //    [new Vector2(4, 2)] = 3,
    //    [new Vector2(4, 3)] = 3,
    //    [new Vector2(4, 4)] = 3,

    //    [new Vector2(5, 1)] = 4,
    //    [new Vector2(5, 2)] = 5,
    //    [new Vector2(5, 3)] = 5,
    //    [new Vector2(5, 4)] = 4,

    //    [new Vector2(6, 2)] = 3,
    //    [new Vector2(6, 3)] = 3,
    //};
    //public static Dictionary<Vector2, int> level21 = new Dictionary<Vector2, int>
    //{
    //    [new Vector2(1, 1)] = 5,
    //    [new Vector2(1, 2)] = 5,
    //    [new Vector2(1, 3)] = 5,
    //    [new Vector2(1, 4)] = 5,

    //    [new Vector2(2, 2)] = 4,
    //    [new Vector2(2, 3)] = 4,

    //    [new Vector2(3, 1)] = 3,
    //    [new Vector2(3, 2)] = 2,
    //    [new Vector2(3, 3)] = 4,
    //    [new Vector2(3, 4)] = 5,

    //    [new Vector2(4, 2)] = 4,
    //    [new Vector2(4, 3)] = 5,

    //    [new Vector2(5, 1)] = 3,
    //    [new Vector2(5, 2)] = 3,
    //    [new Vector2(5, 3)] = 5,
    //    [new Vector2(5, 4)] = 2,

    //    [new Vector2(6, 1)] = 4,
    //    [new Vector2(6, 4)] = 3,
    //};
    //public static Dictionary<Vector2, int> level22 = new Dictionary<Vector2, int>
    //{
    //    [new Vector2(1, 1)] = 5,
    //    [new Vector2(1, 2)] = 5,
    //    [new Vector2(1, 3)] = 5,
    //    [new Vector2(1, 4)] = 5,

    //    [new Vector2(2, 2)] = 4,
    //    [new Vector2(2, 3)] = 4,

    //    [new Vector2(3, 2)] = 3,
    //    [new Vector2(3, 3)] = 3,

    //    [new Vector2(4, 1)] = 2,
    //    [new Vector2(4, 2)] = 2,
    //    [new Vector2(4, 3)] = 2,
    //    [new Vector2(4, 4)] = 2,

    //    [new Vector2(5, 2)] = 1,
    //    [new Vector2(5, 3)] = 1,

    //    [new Vector2(6, 1)] = 3,
    //    [new Vector2(6, 2)] = 2,
    //    [new Vector2(6, 3)] = 1,
    //    [new Vector2(6, 4)] = 5,
    //};
    //public static Dictionary<Vector2, int> level23 = new Dictionary<Vector2, int>
    //{
    //    [new Vector2(1, 1)] = 5,
    //    [new Vector2(1, 2)] = 5,
    //    [new Vector2(1, 3)] = 5,
    //    [new Vector2(1, 4)] = 5,

    //    [new Vector2(2, 1)] = 5,
    //    [new Vector2(2, 2)] = 5,
    //    [new Vector2(2, 3)] = 5,
    //    [new Vector2(2, 4)] = 5,

    //    [new Vector2(3, 1)] = 5,
    //    [new Vector2(3, 2)] = 5,
    //    [new Vector2(3, 3)] = 3,
    //    [new Vector2(3, 4)] = 3,

    //    [new Vector2(4, 1)] = 4,
    //    [new Vector2(4, 2)] = 4,
    //    [new Vector2(4, 3)] = 4,
    //    [new Vector2(4, 4)] = 4,

    //    [new Vector2(5, 2)] = 3,
    //    [new Vector2(5, 3)] = 3,

    //    [new Vector2(6, 1)] = 2,
    //    [new Vector2(6, 2)] = 2,
    //    [new Vector2(6, 3)] = 2,
    //    [new Vector2(6, 4)] = 2,
    //};
    //public static Dictionary<Vector2, int> level24 = new Dictionary<Vector2, int>
    //{
    //    [new Vector2(1, 1)] = 5,
    //    [new Vector2(1, 2)] = 5,
    //    [new Vector2(1, 3)] = 5,
    //    [new Vector2(1, 4)] = 5,

    //    [new Vector2(2, 1)] = 4,
    //    [new Vector2(2, 2)] = 5,
    //    [new Vector2(2, 3)] = 5,
    //    [new Vector2(2, 4)] = 4,

    //    [new Vector2(3, 1)] = 5,
    //    [new Vector2(3, 2)] = 5,
    //    [new Vector2(3, 3)] = 5,
    //    [new Vector2(3, 4)] = 5,

    //    [new Vector2(4, 1)] = 4,
    //    [new Vector2(4, 2)] = 4,
    //    [new Vector2(4, 3)] = 4,
    //    [new Vector2(4, 4)] = 4,

    //    [new Vector2(5, 1)] = 5,
    //    [new Vector2(5, 2)] = 5,
    //    [new Vector2(5, 3)] = 5,
    //    [new Vector2(5, 4)] = 5,

    //    [new Vector2(6, 1)] = 4,
    //    [new Vector2(6, 2)] = 3,
    //    [new Vector2(6, 3)] = 3,
    //    [new Vector2(6, 4)] = 4,
    //};

    //all levels to call them from GameManager while clicking the start icon buttons. Buttons have int parameters that correspond to game level.  
    public static Dictionary<int, Dictionary<Vector2, int>> allLevels = new Dictionary<int, Dictionary<Vector2, int>>
    {
        [1] = level1,
        [2] = level2,
        [3] = level3,
        [4] = level4,
        [5] = level5,
        [6] = level6,
        [7] = level7,
        [8] = level8,
        [9] = level9,
        [10] = level10,
        [11] = level11,
        [12] = level12,
        [13] = level13,
        [14] = level14,
        [15] = level15,
        [16] = level16,
        //[17] = level17,
        //[18] = level18,
        //[19] = level19,
        //[20] = level20,
        //[21] = level21,
        //[22] = level22,
        //[23] = level23,
        //[24] = level24,
    };
}
