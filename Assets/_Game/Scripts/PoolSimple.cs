using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolType
{
    none = 0,
    cube = 1
}
public static class SimplePool
{
    public static Dictionary<PoolType, Pool> keyPool = new Dictionary<PoolType, Pool>();

    public static void PreLoad(GameUnit gameUnit, Transform parent, int amout)
    {
        if (!keyPool.ContainsKey(gameUnit.poolType))
        {
            Pool pool = new Pool();
            pool.PreLoad(gameUnit, parent, amout);

            keyPool.Add(gameUnit.poolType, pool);
        }
        else
        {
            Pool pool = new Pool();
            pool.PreLoad(gameUnit, parent, amout);
        }
    }

    public static GameUnit Spawn(PoolType poolType)
    {
        GameUnit gameUnit = null;

        if (keyPool.ContainsKey(poolType))
        {
            gameUnit = keyPool[poolType].Spawn();
        }
        else
        {
            Debug.LogError(false);
        }

        return gameUnit;
    }

    public static GameUnit Spawn(PoolType poolType, Vector3 tf, Quaternion qt)
    {
        GameUnit gameUnit = null;

        if (keyPool.ContainsKey(poolType))
        {
            gameUnit = keyPool[poolType].Spawn();
        }
        else
        {
            Debug.LogError(false);
        }

        gameUnit.tf.position = tf;
        gameUnit.tf.rotation = qt;
        return gameUnit;
    }

    public static void Despawn(GameUnit gameUnit)
    {
        keyPool[gameUnit.poolType].Despawn(gameUnit);
    }

    public static void DespawnAll()
    {
        foreach (var key in keyPool.Keys)
        {
            if (key != PoolType.none)
            {
                keyPool[key].DespawnAll();
            }
        }
    }


    public class Pool
    {
        public List<GameUnit> gameUnits = new List<GameUnit>();

        public List<GameUnit> active = new List<GameUnit>();

        GameUnit prefab;
        Transform parent;
        public void PreLoad(GameUnit gameUnit, Transform parent, int amout)
        {
            prefab = gameUnit;

            for (int i = 0; i < amout; i++)
            {
                GameUnit unit = GameObject.Instantiate(gameUnit, parent);
                gameUnits.Add(unit);
                unit.gameObject.SetActive(false);
            }
        }


        public GameUnit Spawn()
        {
            GameUnit gameUnit = null;
            if (gameUnits.Count > 0)
            {
                gameUnit = gameUnits[0];
                gameUnits.RemoveAt(0);
                active.Add(gameUnit);
                gameUnit.gameObject.SetActive(true);
            }
            else
            {
                gameUnit = GameObject.Instantiate(prefab, parent);
                active.Add(gameUnit);
                gameUnit.gameObject.SetActive(true);
            }

            return gameUnit;
        }

        public void Despawn(GameUnit gameUnit)
        {
            gameUnits.Add(gameUnit);
            active.Remove(gameUnit);
            gameUnit.gameObject.SetActive(false);
        }

        public void DespawnAll()
        {
            for (int i = 0; i < active.Count; i++)
            {
                gameUnits.Add(active[i]);
                active[i].gameObject.SetActive(false);
                active.RemoveAt(i);

            }
        }
    }
}
