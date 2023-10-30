using DG.Tweening;
using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;

public class CubeManage : Singleton<CubeManage>
{
    [SerializeField] private Player player;
    [SerializeField] private CameraFollow cam;

    public void AddCube(Charater charater, Cube cube, int level)
    {
        if (charater.IsGrowing == false)
        {
            Node<Cube> nodeLoop = charater.LinkedListCube.head;
            Node<Cube> newNode = new Node<Cube>(cube);

            cube.OnInIt(level, charater);

            while (nodeLoop.next != null && nodeLoop.next.data.Level >= level)
            {
                nodeLoop = nodeLoop.next;
            }

            newNode.next = nodeLoop.next;
            newNode.pre = nodeLoop;
            nodeLoop.next = newNode;

            if (newNode.next != null)
            {
                newNode.next.pre = newNode;
            }

            CubeMove cubeMoveNewNode = Cache.GetCubeMoveFromCube(newNode.data);
            cubeMoveNewNode.OnInit(nodeLoop.data, charater, level, nodeLoop.data.tf.position - (nodeLoop.data.Limit + newNode.data.Limit) * nodeLoop.data.tf.forward);

            if (newNode.next != null)
            {
                CubeMove cubeMoveNextNode = Cache.GetCubeMoveFromCube(newNode.next.data);
                cubeMoveNextNode.OnInit(newNode.data, charater, newNode.next.data.Level, newNode.next.data.tf.position);
            }

            MergeCube(charater, nodeLoop, newNode);

            SpawnCube();
        }
        else
        {
            cube.OnInIt(level, charater);
            cube.gameObject.SetActive(false);
            charater.QueueCube.Enqueue(cube);
        }
    }

    public void RemoveCube(Charater charater, Cube cube)
    {
        Node<Cube> nodeLoop = charater.LinkedListCube.head;
        
        while (nodeLoop.next != null && nodeLoop.data != cube)
        {
            nodeLoop = nodeLoop.next;
        }

        Node<Cube> preNode;
        preNode = nodeLoop.pre;

        charater.LinkedListCube.RemoveNode(preNode.next.data);

        if (preNode.next != null)
        {
            CubeMove cubeMove = Cache.GetCubeMoveFromCube(preNode.next.data);
            cubeMove.CubeFollow = preNode.data;

            MoveToFront(preNode);
        }
            
        SimplePool.Despawn(cube);
    }

    private void MoveToFront(Node<Cube> preNodePara)
    {
        Node<Cube> preNode = preNodePara;
        Node<Cube> curNode = preNode.next;
        Vector3 posDoMove = preNode.data.tf.position;
        posDoMove.y = curNode.data.Limit;

        while(curNode != null)
        {
            curNode.data.tf.DOMove(posDoMove, 0.25f);

            Vector3 posPreNode = preNode.data.tf.position;
            Vector3 posCurNode = curNode.data.tf.position;
            Vector3 dir = posPreNode - posCurNode;
            dir.y = 0;

            posDoMove -= dir.normalized * (preNode.data.Limit + curNode.data.Limit);

            preNode = curNode;
            curNode = curNode.next;
        }
    }

    public void MergeCube(Charater charater, Node<Cube> pre, Node<Cube> cur)
    {
        if (pre.data.Level != cur.data.Level)
        {
            charater.IsGrowing = false;

            if (charater.QueueCube.Count != 0)
            {
                Cube cube = charater.QueueCube.Dequeue();
                cube.gameObject.SetActive(true);
                AddCube(charater, cube, cube.Level);
            }
        }
        else
        {
            charater.IsGrowing = true;
            StartCoroutine(LevelUpCube(charater, pre, cur));
        }
    }

    private IEnumerator LevelUpCube(Charater charater, Node<Cube> pre, Node<Cube> cur)
    {
        yield return new WaitForSeconds(0.4f);

        pre.data.OnInit(++pre.data.Level, pre.data.tf.position);
        RemoveCube(charater, cur.data);

        CheckToUpCamera(charater.LinkedListCube);
        
        if (pre.pre != null)
        {
            MergeCube(charater, pre.pre, pre);
        }
        else
        {
            charater.IsGrowing = false;

            if (charater.QueueCube.Count != 0)
            {
                Cube cube = charater.QueueCube.Dequeue();
                cube.gameObject.SetActive(true);
                AddCube(charater, cube, cube.Level);
            }

            charater.IsGrowing = false;
        }
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

    public void SpawnCube()
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

        Cube cube = SimplePool.Spawn(PoolType.cube) as Cube;
        cube.OnInIt(level, null);
    }
}
