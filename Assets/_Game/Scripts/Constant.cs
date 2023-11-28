using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant
{
    //Standart Value
    public const int CUBE_VALUE = 2;
    public static Vector3 CUBE_SCALE = new Vector3(1, 1, 1);
    public const float CUBE_LIMIT = 0.5f;
    public const float CUBE_VALUE_ADD_LIMIT = 0.1f;
    public const float CUBE_VALUE_ADD_SCALE = 0.2f;
    public const int LEVEL_COUNT = 10;
    public static float CUBE_SPEED = 6f;
    public const float TIME_MERGE = 0.4f;

    //Other
    public const float SIGHT_DISTANCE = 10f;
    public const int VOLUMM_CUBE_IN_MAP = 100;
    public const int VOLUMN_ENEMY_IN_GAME = 10;

    //Map
    public const float LIMIT_MAP_X = 95f;
    public const float LIMIT_MAP_Z = 95f;

    //Tag
    public const string TAG_CUBE_ON_MAP = "CubeOnMap";
    public const string TAG_CUBE_CAN_MOVE = "CubeCanMove";
    public const string TAG_CHARACTER = "Character";
}
