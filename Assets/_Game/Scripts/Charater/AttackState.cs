using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IStateMachine<Enemy>
{
    float clock;

    public void OnIn(Enemy t)
    {
        clock = 0;
    }

    public void OnRun(Enemy t)
    {
        clock += Time.deltaTime;

        if (clock >= 5f || t.IsDes)
        {
            t.ChangeState(new PatrolState());
        }

        t.Attack();
    }

    public void OnOut(Enemy t)
    {

    }
}
