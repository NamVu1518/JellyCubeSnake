using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    [SerializeField] private Cube cube;
    [SerializeField] private Cube cubeFollow;

    public Cube CubeFollow
    {
        get { return cubeFollow; }
        set { cubeFollow = value; }
    }

    public Cube Cube
    {
        get { return cube; }    
    }

    public void Update()
    {
        Vector3 dir = cubeFollow.tf.position - cube.tf.position;
        dir.y = 0;
        cube.tf.rotation = Quaternion.LookRotation(dir == Vector3.zero ? cube.tf.forward : dir);

        if (Vector3.Distance(cube.tf.position, cubeFollow.tf.position + (cube.tf.position.y - cubeFollow.tf.position.y) * Vector3.up) > (cube.Limit + cubeFollow.Limit))
        {
            cube.tf.Translate(Vector3.forward * Time.deltaTime * Constant.speed);
        }
    }

    public void OnInit(Cube cubeFollow, Charater charater, int level, Vector3 initPosition)
    {
        this.cubeFollow = cubeFollow;
        cube.OnInit(level, initPosition, charater);
    }
}
