using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Charater
{
    [SerializeField] private NavMeshAgent meshAgent;
    [SerializeField] private LayerMask layerMaskCubeOnMap;
    [SerializeField] private LayerMask layerMaskCherater;
    [SerializeField] private Vector3 position;

    private Cube desCube;
    private IStateMachine<Enemy> currentState;
    private Collider[] colliderCubeOnMapInSigth;
    private Collider[] colliderEnemyInSigth;
    private Cube farestCube;

    [SerializeField] private string text;

    public Cube DesCube
    {
        get { return desCube; }
        set { desCube = value; }
    }

    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        meshAgent.speed = Constant.CUBE_SPEED * speedControler;

        if (GameManage.Ins.State == GameState.onPlaying)
        {
            colliderCubeOnMapInSigth = Physics.OverlapSphere(tf.position, Constant.SIGHT_DISTANCE, layerMaskCubeOnMap);
            colliderEnemyInSigth = Physics.OverlapSphere(tf.position, Constant.SIGHT_DISTANCE, layerMaskCherater);

            SightCubeOnMap();
            currentState.OnRun(this);

            text = currentState.ToString();
        }

        NameSeeCamera();
    }

    public override void OnInit()
    {
        base.OnInit();

        SetDes(thisCube);
        farestCube = thisCube;
        ChangeState(new PatrolState());
    }

    private void SightCubeOnMap()
    {
        if (Vector3.Distance(thisCube.tf.position, farestCube.tf.position + (farestCube.tf.position.y - thisCube.tf.position.y) * Vector3.up) >= Constant.SIGHT_DISTANCE)
        {
            Cache.GetObstacleFromCube(farestCube).enabled = true;
            farestCube = thisCube;
        }

        if (colliderCubeOnMapInSigth.Length > 0)
        {
            for (int i = 0; i < colliderCubeOnMapInSigth.Length; i++)
            {
                Cube cubeCheck = Cache.GetCubeFromCollider(colliderCubeOnMapInSigth[i]);

                if (thisCube.Level >= cubeCheck.Level)
                {
                    Cache.GetObstacleFromCube(cubeCheck).enabled = false;

                    if (Vector3.Distance(thisCube.tf.position, farestCube.tf.position + (farestCube.tf.position.y - thisCube.tf.position.y) * Vector3.up)
                        < Vector3.Distance(thisCube.tf.position, cubeCheck.tf.position + (cubeCheck.tf.position.y - thisCube.tf.position.y) * Vector3.up))
                    {
                        farestCube = cubeCheck;
                    }
                }
            }
        }
    }

    public void SightEnemy()
    {
        if (colliderEnemyInSigth.Length > 0)
        {
            Cube charater = Cache.GetCubeFromCollider(colliderEnemyInSigth[0]);

            if (charater.Level < thisCube.Level)
            {
                SetDes(charater);
                ChangeState(new AttackState());
            }
        }
    }

    public void Patrol()
    {
        if (desCube.Level >= this.Level)
        {
            SetDes(CubeManage.Ins.FindNearestCubeCanEat(thisCube));
            return;
        }

        if(colliderCubeOnMapInSigth.Length <= 0)
        {
            SetDes(CubeManage.Ins.FindNearestCubeCanEat(thisCube));
            return;
        }

        int level = -1;
        Cube cubeDesTemp = null;

        for (int i = 0; i < colliderCubeOnMapInSigth.Length; i++)
        {
            Cube cubeCheck = Cache.GetCubeFromCollider(colliderCubeOnMapInSigth[i]);

            if (cubeCheck.Level <= thisCube.Level && cubeCheck.Level > level)
            {
                cubeDesTemp = cubeCheck;
                level = cubeCheck.Level;
            }
        }

        if (cubeDesTemp != null)
        {
            desCube = cubeDesTemp;
            SetDes(desCube);
        }
        else
        {
            SetDes(CubeManage.Ins.FindNearestCubeCanEat(thisCube));
        }
    }

    public void Attack()
    {
        if (desCube.Level >= this.Level)
        {
            SetDes(CubeManage.Ins.FindNearestCubeCanEat(thisCube));
            return;
        }

        SetDes(desCube);
    }

    private void SetDes(Cube des)
    {
        this.desCube = des;
        Vector3 vtDes = desCube.tf.position;
        vtDes.y = 0;

        meshAgent.SetDestination(vtDes);
    }

    public bool IsDes => Vector3.Distance(thisCube.tf.position, desCube.tf.position) < 0.1f;

    public void ChangeState(IStateMachine<Enemy> newState)
    {
        if (currentState != null)
        {
            currentState.OnOut(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnIn(this);
        }
    }

    public override void OnDead()
    {
        CubeManage.Ins.SpawnEnemy();

        base.OnDead();
    }

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        Trigger(other);
    }
}
