using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        DoublyLinkedList<int> doublyLinkedList = new DoublyLinkedList<int>();
        doublyLinkedList.AddHead(0);
        doublyLinkedList.AddHead(1);
        Node<int> newNode = doublyLinkedList.AddHead(2);

        doublyLinkedList.AddAfter(newNode, 1234);

        Node<int> nodeLoop = doublyLinkedList.head;

        while (nodeLoop != null)
        {
            Debug.Log(nodeLoop.data);
            nodeLoop = nodeLoop.next;
        }
    }
}
