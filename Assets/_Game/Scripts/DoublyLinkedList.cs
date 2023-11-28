using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DoublyLinkedList<T>
{
    public Node<T> head;

    public DoublyLinkedList()
    {
        head = null;
    }

    public Node<T> Head
    {
        get { return head; }
    }

    public int Count()
    {
        int count = 0;
        Node<T> node = head;

        while(node != null)
        {
            count++;
            node = node.next;
        }

        return count;
    }

    public Node<T> AddHead(T data)
    {
        if (head == null)
        {
            head = new Node<T>(data);
        }
        else
        {
            Node<T> node = new Node<T>(data);
            node.next = head;
            head.pre = node;
            head = node;
        }

        return head;
    }

    public void AddTail(T data)
    {
        if (head == null)
        {
            head = new Node<T>(data);
        }
        else
        {
            Node<T> node = head;    

            while(node.next != null)
            {
                node = node.next;
            }
            node.next = new Node<T>(data);
            node.next.pre = node;
        }
    }

    public void AddAt(T data, int index)
    {
        if (head == null)
        {
            AddHead(data);
        }
        else if(index > Count() - 1) {
            AddTail(data);
        }
        else
        {
            Node<T> node = head;
            int count = 0;

            while (node.next != null && count != index - 1)
            {
                count++;
                node = node.next;
            }

            Node<T> newNode = new Node<T>(data);
            newNode.next = node.next;
            newNode.pre = node;
            node.next = newNode;
            newNode.next.pre = newNode;
        }
    }

    public Node<T> AddAfter(Node<T> node, T data) 
    {
        Node<T> nodeLoop = head;
 
        while(nodeLoop.next != null && nodeLoop != node)
        {
            nodeLoop = nodeLoop.next;
        }

        Node<T> newNode = new Node<T>(data);
        newNode.next = node.next;
        newNode.pre = node; 
        node.next = newNode;

        if (newNode.next != null)
        {
            newNode.next.pre = newNode;
        }

        return newNode;
    }

    public Node<T> AddBefore(Node<T> node, T data)
    {
        Node<T> nodeLoop = head;

        while (nodeLoop.next != null && nodeLoop != node)
        {
            nodeLoop = nodeLoop.next;
        }

        Node<T> newNode = new Node<T>(data);
        newNode.next = node;
        newNode.pre = node.pre;
        node.pre = newNode;

        if (node.pre != null)
        {
            node.pre.next = newNode;
        }

        return newNode;
    }

    public T GetAt(int index)
    {
        if (index > Count() - 1 || index < 0)
        {
            throw new IndexOutOfRangeException("Index was out of range");
        }

        Node<T> node = head;
        int count = 0;

        while (node != null && count != index)
        {
            count++;
            node = node.next;
        }

        return node.data;
    }

    public Node<T> GetNodeAt(int index)
    {
        if (index > Count() - 1 || index < 0)
        {
            throw new IndexOutOfRangeException("Index was out of range");
        }

        Node<T> node = head;
        int count = 0;

        while (node != null && count != index)
        {
            count++;
            node = node.next;
        }

        return node;
    }

    public void RemoveAll()
    {
        head = null;
    }

    public Node<T> RemoveTail()
    {
        Node<T> node = GetNodeAt(Count() - 1);
        node.pre.next = null;
        node.pre = null;

        return node;
    } 

    public Node<T> RemoveHead()
    {
        Node<T> nodeHead = head;

        head = head.next;
        head.pre = null;
        nodeHead.next = null;

        return nodeHead;
    }

    public T RemoveGet(int index)
    {
        if (index > Count() - 1 || index < 0)
        {
            throw new IndexOutOfRangeException("Index was out of range");
        }
        
        T data = GetNodeAt(index).data;
        RemoveAt(index);

        return data;
    }

    public void RemoveAt(int index)
    {
        if (index > Count() - 1 || index < 0)
        {
            throw new IndexOutOfRangeException("Index was out of range");
        }

        if (index == 0)
        {
            RemoveHead();
            return;
        }
        else if (index == Count() - 1)
        {
            RemoveTail();
        }
        else
        {
            Node<T> cur = head;

            int count = 0;

            while (count < index)
            {
                count++;
                cur = cur.next;
            }

            cur.pre.next = cur.next;

            if (cur.next != null)
            {
                cur.next.pre = cur.pre;
            }
        }
    }

    public Node<T> RemoveNode(Node<T> node)
    {
        Node<T> nodeLoop = head;

        while (nodeLoop.next != null && node != nodeLoop)
        {
            nodeLoop = nodeLoop.next;
        }

        if (nodeLoop.pre != null)
        {
            nodeLoop.pre.next = nodeLoop.next;
        }

        if (nodeLoop.next != null)
        {
            nodeLoop.next.pre = nodeLoop.pre;
        }

        nodeLoop.next = null;
        nodeLoop.pre = null;

        return nodeLoop;
    }

    public Node<T> RemoveNode(T data)
    {
        Node<T> nodeLoop = head;

        while (nodeLoop.next != null && !ReferenceEquals(data, nodeLoop.data))
        {
            nodeLoop = nodeLoop.next;
        }

        if (nodeLoop.pre != null)
        {
            nodeLoop.pre.next = nodeLoop.next;
        }

        if (nodeLoop.next != null)
        {
            nodeLoop.next.pre = nodeLoop.pre;
        }

        nodeLoop.next = null;
        nodeLoop.pre = null;

        return nodeLoop;
    }

    public T[] ToArray()
    {
        T[] arr = new T[Count()];
        Node<T> cur = head;

        for (int i = 0; i < Count(); i++)
        {
            arr[i] = cur.data;
            cur = cur.next;
        }

        return arr;
    }

    public List<T> Tolist()
    {
        List<T> list = new List<T>();
        Node<T> cur = head;

        for (int i = 0; i < Count(); i++)
        {
            list.Add(cur.data);
            cur = cur.next;
        }

        return list;
    }
}

public class Node<T>
{
    public T data;
    public Node<T> next;
    public Node<T> pre;

    public Node(T value) 
    { 
        data = value;
        next = null;
        pre = null;
    }
}
