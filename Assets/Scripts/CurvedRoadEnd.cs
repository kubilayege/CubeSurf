using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedRoadEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CubeBehaviour>(out CubeBehaviour cube) && cube.player != null && cube.player.currentCubes.Contains(cube.gameObject))
        {
            cube.player.StopTurning();
        }
    }
}
