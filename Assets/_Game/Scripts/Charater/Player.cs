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
        if (GameManage.Ins.State == GameState.onPlaying)
        {
            PlayerMove();
        }

        NameSeeCamera();
    }

    private void PlayerMove()
    {
        if (JoyStick.GetDirect().sqrMagnitude >= 0.1f)
        {
            vtDirect = JoyStick.GetDirect().normalized;
        }



        tf.rotation = Quaternion.LookRotation(vtDirect.normalized);
        tf.Translate(Vector3.forward * Constant.CUBE_SPEED * Time.deltaTime * speedControler)    ;
    }

    public override void OnDead()
    {
        base.OnDead();
    }

    public override void OnDespawn()
    {
        //
    }

    private void OnTriggerEnter(Collider other)
    {
        Trigger(other);
    }
}
