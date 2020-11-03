using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointControl : MonoBehaviour
{
    public List<WayPoint> wps;
    private void Awake()
    {
        wps = new List<WayPoint>();
        foreach (var wp in transform.GetComponentsInChildren<WayPoint>())
        {
            wps.Add(wp);
        }
    }
    
    public float GetLenght()
    {
        WayPoint lastWP = null;
        var lenght = 0f;
        foreach (var wp in wps)
        {
            if (wp == lastWP)
                break;

            if(lastWP == null)
            {
                lastWP = wp;
                continue;
            }

            lenght += Vector3.Distance(lastWP.position, wp.position);
            lastWP = wp;
        }
        return lenght;
    }
}
