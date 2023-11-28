using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeOnMap : Cube
{
    public override void OnInit(int level, Charater owner)
    {
        this.Owner = owner;

        OnInit(level, CubeManage.Ins.RandomAPointInMap());
    }

    public override void OnInit(int level, Vector3 initPosition, Charater owner)
    {
        this.Owner = owner;

        OnInit(level, initPosition);
    }

    public override void OnInit(int level, Vector3 initPosition)
    {
        base.OnInit(level, initPosition);

        Cache.GetObstacleFromCube(this).enabled = true;
    }
}
