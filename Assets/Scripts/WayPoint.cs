using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WayPoint : MonoBehaviour
{
    public Vector3 position;
    private void Start()
    {
        position = transform.position;
    }
}
