using System.Collections;
using System.Collections.Generic;
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
        CubeMove cubeMove = Cache.GetCubeMoveFromCollider(other);   

        if (cubeMove.Cube.Owner != this)
        {
            cubeMove.enabled = true;
            cubeMove.OnBeingEaten(listCube[listCube.Count - 1], this);
            listCube.Add(cubeMove.Cube);
        }
    }
}
