using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Charater : Cube
{
    [SerializeField] protected float speedControler;
    [SerializeField] protected RectTransform tf_name;

    protected Transform tf_mainCam;

    protected DoublyLinkedList<Cube> linkedListCube = new DoublyLinkedList<Cube>();
    protected Queue<Cube> queueCube = new Queue<Cube>();

    protected bool isGrowing;

    public bool IsGrowing
    {
        get { return this.isGrowing; } 
        set { this.isGrowing = value; }
    }

    public Queue<Cube> QueueCube
    {
        get { return this.queueCube; }
        set { this.queueCube = value; }
    }

    public DoublyLinkedList<Cube> LinkedListCube
    {
        get { return this.linkedListCube; }
        set { this.linkedListCube = value; }
    }

    public void NameSeeCamera()
    {
        this.tf_name.rotation = Quaternion.LookRotation(this.tf_name.position - this.tf_mainCam.position);
    }

    public virtual void OnInit()
    {
        this.tf_mainCam = Camera.main.transform;
        this.speedControler = 1f;
        this.isGrowing = false;
        this.queueCube.Clear();
        this.linkedListCube.RemoveAll();
        this.thisCube.OnInit(0, this);
        this.linkedListCube.AddHead(this.gameObject.GetComponent<Cube>());
    }

    public virtual void OnDead() 
    {
        CubeManage.Ins.ToCubeOnMapWhenCharaterDead(this);
        this.OnDespawn();
    }

    public virtual void OnDespawn()
    {

    }

    public void TriggerWithCubeOnMap(Cube cube)
    {
        if (cube.Level <= this.thisCube.Level)
        {
            int levelCube = cube.Level;
            SimplePool.Despawn(cube);

            CubeMove cubeCanMove = SimplePool.Spawn(PoolType.cubeCanMove) as CubeMove;
            CubeManage.Ins.AddCubeMove(this, cubeCanMove, cube.Level);
        }
        else
        {
            SkillManage.Ins.Push(tf, new Vector3(tf.position.x, cube.tf.position.y, tf.position.z) - cube.tf.position, 1f, 0.5f);
        }
    }

    public void TriggerWithCubeCanMove(Cube cube)
    {
        if (cube.Owner == this)
        {
            return;
        }

        if (cube.Level <= this.thisCube.Level)
        {
            int levelCube = cube.Level;
            Charater charater = cube.Owner;

            charater.linkedListCube.RemoveNode(cube);
            SimplePool.Despawn(cube);

            CubeMove cubeCanMove = SimplePool.Spawn(PoolType.cubeCanMove) as CubeMove;
            CubeManage.Ins.AddCubeMove(this, cubeCanMove, cube.Level);
        }
        else
        {
            //OnDead
        }
    }

    public void TriggerWithCharater(Cube cube)
    {
        if (cube.Level < this.thisCube.Level)
        {
            Charater charater = (Charater)cube;
            int levelCube = charater.Level;
            
            CubeMove cubeCanMove = SimplePool.Spawn(PoolType.cubeCanMove) as CubeMove;
            CubeManage.Ins.AddCubeMove(this, cubeCanMove, cube.Level);

            charater.OnDead();
        }
        else if (cube.Level == this.thisCube.Level)
        {
            SkillManage.Ins.Push(tf, new Vector3(tf.position.x, cube.tf.position.y, tf.position.z) - cube.tf.position, 1f, 0.5f);
        }
    }

    public void Trigger(Collider other)
    {
        Cube cube = Cache.GetCubeFromCollider(other);

        if (other.gameObject.CompareTag(Constant.TAG_CUBE_ON_MAP))
        {
            TriggerWithCubeOnMap(cube);
        }
        else if (other.gameObject.CompareTag(Constant.TAG_CUBE_CAN_MOVE))
        {
            TriggerWithCubeCanMove(cube);
        }
        else if (other.gameObject.CompareTag(Constant.TAG_CHARACTER))
        {
            TriggerWithCharater(cube);
        }
    }
}
