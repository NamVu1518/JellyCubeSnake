using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControl : MonoBehaviour
{
    [SerializeField] PoolAmout[] listPool;
    void Awake()
    {
        for (int i = 1; i < listPool.Length; i++)
        {
            SimplePool.PreLoad(listPool[i].unit, listPool[i].parent, listPool[i].amout);
        }
    }
}

[System.Serializable]
public class PoolAmout
{
    public GameUnit unit;
    public Transform parent;
    public int amout;
}
