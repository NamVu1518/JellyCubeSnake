using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IStateMachine<Enemy>
{
    float clock = 0;

    public void OnIn(Enemy t)
    {
        clock = 0;
    }

    public void OnRun(Enemy t)
    {
        clock += Time.deltaTime;
        t.Patrol();

        if (clock >= 3f)
        {
            t.SightEnemy();
        }
    }

    public void OnOut(Enemy t)
    {
    }

    
}
