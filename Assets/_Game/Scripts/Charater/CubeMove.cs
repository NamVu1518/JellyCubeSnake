using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : Cube
{
    [SerializeField] private Cube cubeFollow;
    [SerializeField] private float speedControler;

    private bool isMerging;

    public bool IsMerging
    {
        get { return isMerging; }
        set { isMerging = value; }
    }

    public Cube CubeFollow
    {
        get { return cubeFollow; }
        set { cubeFollow = value; }
    }

    public void Update()
    {
        Vector3 dir = cubeFollow.tf.position - thisCube.tf.position;
        dir.y = 0;
        thisCube.tf.rotation = Quaternion.LookRotation(dir == Vector3.zero ? thisCube.tf.forward : dir);

        if (isMerging == false)
        {
            NormalMove();
        }
        else
        {
            MergerMove();
        }
    }

    public void MergerMove()
    {
        thisCube.tf.Translate(Vector3.forward * Constant.CUBE_SPEED * Time.deltaTime * speedControler * 1.5f);
    }

    public void NormalMove()
    {
        if (Vector3.Distance(thisCube.tf.position, cubeFollow.tf.position + (thisCube.tf.position.y - cubeFollow.tf.position.y) * Vector3.up) > (thisCube.Limit + cubeFollow.Limit))
        {
            thisCube.tf.Translate(Vector3.forward * Constant.CUBE_SPEED * Time.deltaTime * speedControler);
        }
    }

    public void OnInit(Cube cubeFollow, Charater charater, int level, Vector3 initPosition)
    {
        this.cubeFollow = cubeFollow;
        thisCube.OnInit(level, initPosition, charater);
        speedControler = 1f;
        isMerging = false;
    }
}
