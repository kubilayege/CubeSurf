using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWP : WayPoint
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
            GameManager.instance.game.FinishLine();
    }
}
