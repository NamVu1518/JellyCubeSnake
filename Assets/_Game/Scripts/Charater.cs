using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charater : GameUnit
{
    [SerializeField] protected Cube cube;
    protected DoublyLinkedList<Cube> linkedListCube = new DoublyLinkedList<Cube>();
    protected Queue<Cube> queueCube = new Queue<Cube>();

    protected bool isGrowing = false;

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

    public void OnInit()
    {
        this.cube.OnInIt(0, this);
        this.linkedListCube.AddHead(this.gameObject.GetComponent<Cube>());
    }
 }
