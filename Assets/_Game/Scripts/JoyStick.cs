using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick : MonoBehaviour
{
    [Header("Joystick")]
    [SerializeField] private Transform background;
    [SerializeField] private Transform joy;
    [SerializeField] private RectTransform panelLimitJoy;
    [SerializeField] private float numDisMax;

    private Vector3 vtFirstPosBackground;
    private static Vector3 directDrag;

    bool boolAcceptControl = true;

    void Start()
    {
        vtFirstPosBackground = background.position;
    }

    // Update is called once per frame
    void Update()
    {
        Drag();
    }

    private void Drag()
    {
        if (IsAcceptAttack())
        {
            if (Input.GetMouseButtonDown(0))
            {
                background.position = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                float numDis = Mathf.Clamp(Vector3.Distance(Input.mousePosition, background.position), 0f, numDisMax);
                Vector3 clampedOffset = (Input.mousePosition - background.position).normalized;

                directDrag = clampedOffset;
                joy.position = background.position + clampedOffset * numDis;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                background.position = vtFirstPosBackground;
                joy.position = vtFirstPosBackground;
                directDrag = Vector3.zero;
            }
        }
    }

    private bool IsAcceptAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(panelLimitJoy, Input.mousePosition))
            {
                boolAcceptControl = true;
            }
            else
            {
                boolAcceptControl = false;
            }
        }

        return boolAcceptControl;
    }
    public static Vector3 GetDirect()
    {
        if (directDrag != Vector3.zero)
        {
            return new Vector3(directDrag.x, 0, directDrag.y);
        }

        return Vector3.zero;
    }
}
