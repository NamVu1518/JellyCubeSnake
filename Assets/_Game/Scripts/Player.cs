using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : Charater
{
    private Vector3 vtDirect = Vector3.forward;

    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        if (JoyStick.GetDirect().sqrMagnitude >= 0.1f)
        {
            vtDirect = JoyStick.GetDirect().normalized;
        }


        tf.rotation = Quaternion.LookRotation(vtDirect.normalized);
        tf.Translate(Vector3.forward * Time.deltaTime * Constant.speed);
    }




    private void OnTriggerEnter(Collider other)
    {
        Cube cube = Cache.GetCubeFromCollider(other);   

        if (cube.Owner == this)
        {
            return;
        }

        if ((cube.Owner == null && cube.Level <= this.cube.Level) || (cube.Owner != this && cube.Level < this.cube.Level))
        {
            int levelCube = cube.Level;
            SimplePool.Despawn(cube);
            
            Cube cubeCanMove = SimplePool.Spawn(PoolType.cubeCanMove) as Cube;
            CubeManage.Ins.AddCube(this, cubeCanMove, cube.Level);
        }
        else
        {
            SkillManage.Ins.Push(tf, new Vector3(tf.position.x, cube.tf.position.y, tf.position.z) - cube.tf.position, 1f, 0.5f);
        }
    }
}
