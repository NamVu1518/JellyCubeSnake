using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache
{
    public static Dictionary<Collider, Cube> cacheCube = new Dictionary<Collider, Cube>();
    public static Dictionary<Cube, CubeMove> cacheCubeMove = new Dictionary<Cube, CubeMove>();


    public static Cube GetCubeFromCollider(Collider collider)
    {
        if (!cacheCube.ContainsKey(collider))
        {
            Cube cube = collider.gameObject.GetComponent<Cube>();

            cacheCube[collider] = cube;
        }

        return cacheCube[collider];
    }

    public static CubeMove GetCubeMoveFromCube(Cube cubeCanMove)
    {
        if (!cacheCubeMove.ContainsKey(cubeCanMove))
        {
            CubeMove cubeMove = cubeCanMove.gameObject.GetComponent<CubeMove>();

            cacheCubeMove[cubeCanMove] = cubeMove;
        }

        return cacheCubeMove[cubeCanMove];
    }
}
