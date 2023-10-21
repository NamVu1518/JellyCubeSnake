using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScripttableData", menuName = "Scriptable Object")]
public class Scriptable : ScriptableObject
{
    public MaterialLevel materialLevel;

    public int MaterialConut
    {
        get { return materialLevel.arrMaterials.Length; }
    }

    public Material GetMaterial(int index)
    {
        return materialLevel.arrMaterials[index];   
    }
}

[System.Serializable]
public class MaterialLevel
{
    public Material[] arrMaterials;
}
