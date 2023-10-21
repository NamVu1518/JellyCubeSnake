using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charater : GameUnit
{
    [SerializeField] protected Cube cube;
    protected List<Cube> listCube = new List<Cube>();

    public void OnInit()
    {
        this.cube.OnInIt(0);
        this.listCube.Add(this.gameObject.GetComponent<Cube>());
    }
 }
