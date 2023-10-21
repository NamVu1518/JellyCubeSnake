using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DataManage : Singleton<DataManage>
{
    [SerializeField] private Scriptable scriptable;

    public int MaterialCount()
    {
        return scriptable.MaterialConut;
    }

    public Material GetMaterial(int index)
    {
        return scriptable.GetMaterial(index);
    }
}
