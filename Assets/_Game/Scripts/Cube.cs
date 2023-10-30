
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cube : GameUnit
{
    [SerializeField] private TMP_Text textValue;
    [SerializeField] private Renderer cubeRenderer;

    private Charater owner;
    private int level;
    private float limit;
    
    public Charater Owner
    {
        get { return owner; }
        set { owner = value; }
    }

    public float Limit
    {
        get { return limit; } 
        set { limit = value; }
    }

    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    public void OnInIt(int level, Charater owner)
    {
        float rdX = Random.Range(-45f, 45f);
        float rdZ = Random.Range(-45f, 45f);

        OnInit(level, new Vector3(rdX, 0, rdZ), owner);
    }

    public void OnInit(int level, Vector3 initPosition)
    {
        float rdYRotation = Random.Range(0f, 360f);

        this.Level = level;
        cubeRenderer.material = DataManage.Ins.GetMaterial(level > 9 ? level % 9 - 1 : level);
        tf.localScale = new Vector3(Constant.scale.x + level * Constant.scaleAdd, Constant.scale.y + level * Constant.scaleAdd, Constant.scale.z + level * Constant.scaleAdd);
        textValue.SetText(Mathf.Pow(Constant.value, level + 1).ToString());
        limit = Constant.limit + level * Constant.limitAdd;
        tf.rotation = Quaternion.Euler(new Vector3(tf.rotation.x, rdYRotation, tf.rotation.z));
        tf.position = new Vector3(initPosition.x, limit, initPosition.z);
    }

    public void OnInit(int level, Vector3 initPosition, Charater owner)
    {
        this.owner = owner;

        OnInit(level, initPosition);
    }
}
