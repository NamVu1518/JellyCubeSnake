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
            CubeManage.Ins.SpawnCube();
        }
    }
}
