using DG.Tweening;
using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CubeManage : Singleton<CubeManage>
{
    [SerializeField] private Player player;
    [SerializeField] private CameraFollow cam;

    // Them gach vao ran
    public void AddCubeMove(Charater charater, CubeMove cubeCanMove, int level)
    {
        if (charater.IsGrowing == false)
        {
            Node<Cube> nodeLoop = charater.LinkedListCube.head;

            while (nodeLoop.next != null && nodeLoop.next.data.Level >= level)
            {
                nodeLoop = nodeLoop.next;
            }

            //Set node
            Node<Cube> newNode = charater.LinkedListCube.AddAfter(nodeLoop, cubeCanMove);
            float tempLimit = Constant.CUBE_LIMIT + level * Constant.CUBE_VALUE_ADD_LIMIT;
            Vector3 posInit = newNode.pre.data.tf.position - (newNode.pre.data.Limit + tempLimit) * newNode.pre.data.tf.forward;

            if (newNode.next != null)
            {
                CubeMove cubeMoveNext = Cache.GetCubeMoveFromCube(newNode.next.data);
                cubeMoveNext.CubeFollow = newNode.data;
            }

            cubeCanMove.OnInit(newNode.pre.data, charater, level, posInit);

            CheckToMegreCube(charater, newNode.pre, newNode);

            SpawnCubeOnMap();
        }
        else
        {
            cubeCanMove.gameObject.SetActive(false);
            charater.QueueCube.Enqueue(cubeCanMove);
        }
    }

    public void CheckToMegreCube(Charater charater, Node<Cube> pre, Node<Cube> cur)
    {
        if (pre.data.Level == cur.data.Level)
        {
            charater.IsGrowing = true;
            MoveToFront(pre);
            StartCoroutine(MergeCube(charater, pre, cur));
        }
        else
        {
            charater.IsGrowing = false;

            if (charater.QueueCube.Count != 0)
            {
                CubeMove cubeCanMove = charater.QueueCube.Dequeue() as CubeMove;
                cubeCanMove.gameObject.SetActive(true);
                AddCubeMove(charater, cubeCanMove, cubeCanMove.Level);
            }
        }
    }

    private IEnumerator MergeCube(Charater charater, Node<Cube> pre, Node<Cube> cur)
    {
        yield return new WaitForSeconds(Constant.TIME_MERGE);

        pre.data.OnInit(++pre.data.Level, pre.data.tf.position);
        RemoveCubeForLevelUp(charater, cur);

        if (pre.pre != null)
        {
            CheckToMegreCube(charater, pre.pre, pre);
        }
        else
        {
            charater.IsGrowing = false;

            if (charater.QueueCube.Count != 0)
            {
                CubeMove cubeCamMove = charater.QueueCube.Dequeue() as CubeMove;
                cubeCamMove.gameObject.SetActive(true);
                AddCubeMove(charater, cubeCamMove, cubeCamMove.Level);
            }

            charater.IsGrowing = false;
        }
    }

    public void RemoveCubeForLevelUp(Charater charater, Node<Cube> nodeToRemove) 
    {
        if (nodeToRemove != charater.LinkedListCube.Head)
        {
            Node<Cube> nodeLoop = charater.LinkedListCube.head;

            while (nodeLoop.next != null && nodeLoop != nodeToRemove)
            {
                nodeLoop = nodeLoop.next;
            }

            if (nodeLoop.next != null)
            {
                CubeMove cubeMove = Cache.GetCubeMoveFromCube(nodeLoop.next.data);
                cubeMove.CubeFollow = nodeLoop.pre.data;
            }

            Node<Cube> removeNode = charater.LinkedListCube.RemoveNode(nodeLoop);
            SimplePool.Despawn(removeNode.data);
        }
    }

    private void MoveToFront(Node<Cube> targetNode)
    { 
        Node<Cube> curNode = targetNode.next;

        StartCoroutine(StopMoveToFront(curNode));

        while (curNode != null)
        {
            CubeMove cubeMove = Cache.GetCubeMoveFromCube(curNode.data);
            cubeMove.IsMerging = true;
            curNode = curNode.next;
        }
    }

    private IEnumerator StopMoveToFront(Node<Cube> stopNode)
    {
        yield return new WaitForSeconds(Constant.TIME_MERGE);

        while (stopNode != null)
        {
            CubeMove cubeMove = Cache.GetCubeMoveFromCube(stopNode.data);
            cubeMove.IsMerging = false;
            stopNode = stopNode.next;
        }
    }

    public void ToCubeOnMapWhenCharaterDead(Charater charater)
    {
        DoublyLinkedList<Cube> doublyLinkedList = charater.LinkedListCube;
        Node<Cube> nodeLoop = doublyLinkedList.Head;
        Queue<Cube> queueCubeCharater = charater.QueueCube;

        while (nodeLoop.next != null)
        {
            nodeLoop = nodeLoop.next;
            ConvertCubeCanMoveToCubeOnMap((CubeMove)nodeLoop.data);
        }

        while (queueCubeCharater.Count > 0)
        {
            ConvertCubeCanMoveToCubeOnMap(queueCubeCharater.Dequeue() as CubeMove);
        } 
    }

    private void ConvertCubeCanMoveToCubeOnMap(CubeMove cubeMove)
    {
        CubeOnMap cubeOnMap = SimplePool.Spawn(PoolType.cubeOnMap) as CubeOnMap;
        cubeOnMap.OnInit(cubeMove.Level, cubeMove.tf.position, null);
        SimplePool.Despawn(cubeMove);
    } 

    private void CheckToUpCamera(DoublyLinkedList<Cube> doublyLinkedList)
    {
        if (doublyLinkedList != player.LinkedListCube || player.LinkedListCube.head.data.Level > 20)
        {
            return;
        }

        switch(player.LinkedListCube.head.data.Level)
        {
            case 4:
                {
                    cam.SetRateOffset(0.25f);
                    break;
                }
            case 9:
                {
                    cam.SetRateOffset(0.5f);
                    break;
                }
            case 14:
                {
                    cam.SetRateOffset(0.75f);
                    break;
                }
            case 19:
                {
                    cam.SetRateOffset(1f);
                    break;
                }
            default: {
                    return;
                }
        }
    }

    public void SpawnCubeOnMap()
    {
        CubeOnMap cube = SimplePool.Spawn(PoolType.cubeOnMap) as CubeOnMap;
        cube.OnInit(RamdomLevel(), GoodPointForInit(), null);
    }

    public void SpawnEnemy()
    {
        Enemy enemy = SimplePool.Spawn(PoolType.cubeEnemy) as Enemy;
        enemy.OnInit();
    }

    public Vector3 RandomAPointInMap()
    {
        float rdX = Random.Range(-Constant.LIMIT_MAP_X, Constant.LIMIT_MAP_X);
        float rdZ = Random.Range(-Constant.LIMIT_MAP_Z, Constant.LIMIT_MAP_Z);

        return new Vector3(rdX, 0, rdZ);
    }

    public Vector3 GoodPointForInit()
    {
        int lenght = 1;
        Vector3 rdPoint = Vector3.zero;

        while (lenght > 0)
        {
            rdPoint = RandomAPointInMap();
            Collider[] colliders = Physics.OverlapSphere(rdPoint, 5f, LayerMask.GetMask("CubeOnMap"));
            lenght = colliders.Length;
        }

        return rdPoint;
    }

    public int RamdomLevel()
    {
        int rd = Random.Range(0, 100);
        int level = 0;

        if (rd < 30)
        {
            level = 0;
        }
        else if (rd < 50)
        {
            level = 1;
        }
        else if (rd < 60)
        {
            level = 2;
        }
        else if (rd < 65)
        {
            level = 3;
        }
        else if (rd < 70)
        {
            level = 4;
        }
        else if (rd < 85)
        {
            level = 5;
        }
        else if (rd < 90)
        {
            level = 6;
        }
        else if (rd < 94)
        {
            level = 7;
        }
        else if (rd < 97)
        {
            level = 8;
        }
        else if (rd < 100)
        {
            level = 9;
        }

        return level;
    }

    public Cube FindNearestCubeCanEat(Cube cubeEnemy)
    {
        Cube des;

        List<GameUnit> listActiveCubeOnMap = SimplePool.keyPool[PoolType.cubeOnMap].active;

        Vector3 cubePosInit = listActiveCubeOnMap[0].tf.position;
        cubePosInit.y = cubeEnemy.tf.position.y;
        float dis = Vector3.Distance(cubePosInit, cubeEnemy.tf.position);
        des = listActiveCubeOnMap[0] as Cube;

        for (int i = 1; i < listActiveCubeOnMap.Count; i++)
        {
            Cube cube = (Cube)listActiveCubeOnMap[i];
            Vector3 cubePos = cube.tf.position;
            cubePos.y = cubeEnemy.tf.position.y;

            if (cubeEnemy.Level >= cube.Level && Vector3.Distance(cubeEnemy.tf.position, cubePos) < dis)
            {
                des = cube;
                dis = Vector3.Distance(cubeEnemy.tf.position, cubePos);
            }
        }

        return des;
    }
}
