
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Cube : GameUnit
{
    [SerializeField] private Cube cube;
    [SerializeField] private TMP_Text textValue;
    [SerializeField] private Renderer cubeRenderer;

    private Charater owner;
    private int level;
    private float limit;
    
    public Cube thisCube
    {
        get { return cube; }
        set { cube = value; }
    }

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

    public virtual void OnInit(int level, Charater owner)
    {
        this.Owner = owner;

        OnInit(level, CubeManage.Ins.RandomAPointInMap());
    }

    public virtual void OnInit(int level, Vector3 initPosition)
    {
        float rdYRotation = Random.Range(0f, 360f);

        this.Level = level;
        this.cubeRenderer.material = DataManage.Ins.GetMaterial(level > 9 ? level % 9 - 1 : level);
        this.tf.localScale = new Vector3(Constant.CUBE_SCALE.x + level * Constant.CUBE_VALUE_ADD_SCALE, Constant.CUBE_SCALE.y + level * Constant.CUBE_VALUE_ADD_SCALE, Constant.CUBE_SCALE.z + level * Constant.CUBE_VALUE_ADD_SCALE);
        this.textValue.SetText(Mathf.Pow(Constant.CUBE_VALUE, level + 1).ToString());
        this.limit = Constant.CUBE_LIMIT + level * Constant.CUBE_VALUE_ADD_LIMIT;
        this.tf.rotation = Quaternion.Euler(new Vector3(tf.rotation.x, rdYRotation, tf.rotation.z));
        this.tf.position = new Vector3(initPosition.x, limit, initPosition.z);
    }

    public virtual void OnInit(int level, Vector3 initPosition, Charater owner)
    {
        this.owner = owner;

        OnInit(level, initPosition);
    }
}
