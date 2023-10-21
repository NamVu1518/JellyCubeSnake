using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    [SerializeField] private Cube cube;
    private Cube cubeFollow;

    public Cube Cube
    {
        get { return cube; }    
    }

    public void Update()
    {
        cube.tf.rotation = Quaternion.LookRotation(cubeFollow.tf.position - cube.tf.position);

        if (Vector3.Distance(cube.tf.position, cubeFollow.tf.position + (cube.tf.position.y - cubeFollow.tf.position.y) * Vector3.up) > (cube.Limit + cubeFollow.Limit))
        {
            cube.tf.Translate(Vector3.forward * Time.deltaTime * Constant.speed);
        }
    }

    public void OnBeingEaten(Cube cubeFollow, Charater charater)
    {
        this.cubeFollow = cubeFollow;
        cube.Owner = charater;
    }
}
