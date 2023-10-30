using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
    DoublyLinkedList<int> asd = new DoublyLinkedList<int>();

    // Start is called before the first frame update
    void Start()
    {
        asd.AddHead(0);
        asd.AddTail(1);
        asd.AddTail(2);
        asd.AddTail(3);
        asd.AddTail(4);
        asd.AddAt(123, 1);
        int x = 123123;
        asd.AddTail(x);
        asd.RemoveNode(x);
        asd.RemoveAt(0);



        Node<int> z = asd.GetNodeAt(3);
        asd.RemoveNode(z);
        List<int> dsa = asd.Tolist();

        foreach (int i in dsa)
        {
            Debug.Log(i);
        }
    }
}
