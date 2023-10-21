using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Transform")]
    [SerializeField] private Transform tfPlayer;
    [SerializeField] private Transform tfCamera;

    [SerializeField] private Vector3 offsetMin;
    [SerializeField] private Vector3 offsetMax;
    private Vector3 currentOffset;

    private void Start()
    {
        currentOffset = offsetMin;
        tfCamera.rotation = Quaternion.Euler(new Vector3(45, 0, 0));
    }
    void Update()
    {
        Vector3 vtFollow = tfPlayer.position + currentOffset;
        tfCamera.position = vtFollow;
    }

    public void SetRateOffset(float rate)
    {
        currentOffset = Vector3.Lerp(offsetMin, offsetMax, rate);
    }
}