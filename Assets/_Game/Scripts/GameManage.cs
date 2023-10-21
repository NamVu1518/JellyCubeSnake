using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class GameManage : Singleton<GameManage>
{
    private void Start()
    {
        for (int i = 0; i < 50; i++)
        {
            SpawnCube();
        }
    }

    private void SpawnCube()
    {
        int rd = Random.Range(0, 100);
        int level = 0;

        if (rd < 30)
        {
            level = 0;
        }
        else if (rd < 50)
        {
             level = 1;
        }
        else if (rd < 60)
        {
            level = 2;
        }
        else if (rd < 65)
        {
            level = 3;
        }
        else if (rd < 70)
        {
            level = 4;
        }
        else if (rd < 85)
        {
            level = 5;
        }
        else if (rd < 90)
        {
            level = 6;
        }
        else if (rd < 94)
        {
            level = 7;
        }
        else if (rd < 97)
        {
            level = 8;
        }
        else if (rd < 100)
        {
            level = 9;
        }
   
        Cube cube = SimplePool.Spawn(PoolType.cube) as Cube;
        cube.OnInIt(level);
    }
}
