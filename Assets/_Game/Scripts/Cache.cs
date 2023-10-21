using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache
{
    public static Dictionary<Collider, Cube> cacheCube = new Dictionary<Collider, Cube>();
    public static Dictionary<Collider, CubeMove> cacheCubeMove = new Dictionary<Collider, CubeMove>();


    public static Cube GetCubeFromCollider(Collider collider)
    {
        if (!cacheCube.ContainsKey(collider))
        {
            Cube cube = collider.gameObject.GetComponent<Cube>();

            cacheCube[collider] = cube;
        }

        return cacheCube[collider];
    }

    public static CubeMove GetCubeMoveFromCollider(Collider collider)
    {
        if (!cacheCubeMove.ContainsKey(collider))
        {
            CubeMove cubeMove = collider.gameObject.GetComponent<CubeMove>();

            cacheCubeMove[collider] = cubeMove;
        }

        return cacheCubeMove[collider];
    }
}
