using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class Cache
{
    public static Dictionary<Collider, Cube> cacheCubeFromCollider = new Dictionary<Collider, Cube>();
    public static Dictionary<Cube, CubeMove> cacheCubeMove = new Dictionary<Cube, CubeMove>();
    public static Dictionary<Cube, NavMeshObstacle> cacheNavMeshObstacle = new Dictionary<Cube, NavMeshObstacle>();
    public static Dictionary<Collision, Cube> cacheCubeFromCollision = new Dictionary<Collision, Cube>();

    public static Cube GetCubeFromCollider(Collider collider)
    {
        if (!cacheCubeFromCollider.ContainsKey(collider))
        {
            Cube cube = collider.gameObject.GetComponent<Cube>();

            cacheCubeFromCollider[collider] = cube;
        }

        return cacheCubeFromCollider[collider];
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

    public static NavMeshObstacle GetObstacleFromCube(Cube cube)
    {
        if (!cacheNavMeshObstacle.ContainsKey(cube))
        {
            NavMeshObstacle nmo = cube.gameObject.GetComponent<NavMeshObstacle>();

            cacheNavMeshObstacle[cube] = nmo;
        }

        return cacheNavMeshObstacle[cube];
    }

    public static Cube GetCubeFromCollision(Collision collision)
    {
        if (!cacheCubeFromCollision.ContainsKey(collision))
        {
            Cube cube = collision.gameObject.GetComponent<Cube>();

            cacheCubeFromCollision[collision] = cube;
        }

        return cacheCubeFromCollision[collision];
    }
}
