using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 startingPos;
    public Vector3 offset;
    private GameObject target;
    private bool move;
    private float speed;
    [Header("Camera Offsets")]
    public float defaultback;
    public float defaultupward;
    public float defaultright;
    private float back;
    private float upward;
    private float right;
    public void Start()
    {
        startingPos = transform.position;
    }
    public void Follow(GameObject player)
    {
        back = defaultback;
        upward = defaultupward;
        right = defaultright;
        transform.parent.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        target = player;
        offset = target.transform.right * right - target.transform.forward * back + target.transform.up * upward;
        transform.parent.position = transform.parent.position + offset;
    }

    public void Update()
    {
        if(target != null && move)
        {
            //RecalculateOffset();
            var nextPos = transform.parent.position + transform.parent.forward * speed * Time.deltaTime;
            transform.parent.position = Vector3.Lerp(transform.parent.position, nextPos /*+ OffsetValue()*/, Time.deltaTime * 200);
        }
    }

    private Vector3 OffsetValue()
    {
        if (Vector3.Distance(target.transform.position, transform.parent.position) < offset.magnitude)
            return offset * Time.deltaTime * speed;
        else
        {
            //transform.parent.position = transform.parent.position - offset;
            //RecalculateOffset();
            //transform.parent.position = transform.parent.position + offset;
            return Vector3.zero;
        } 
    }

    public void StopMoving()
    {
        move = false;
    }

    public void StartMoving(float speed)
    {
        move = true;
        this.speed = speed;
    }

    public void AdjustCameraPos(int backAdjust, int upAdjust)
    {
        back += backAdjust;
        upward += upAdjust;

        if(back > defaultback * 1.3f)
            RecalculateOffset();
    }

    public void RecalculateOffset()
    {
        transform.parent.position = transform.parent.position - offset;
        offset = target.transform.right * right - target.transform.forward * back + target.transform.up * upward;
        transform.parent.position = transform.parent.position + offset;
    }
}
