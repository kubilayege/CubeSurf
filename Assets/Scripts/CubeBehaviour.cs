using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    public Player player;


    private void Update()
    {
        if(player != null)
        {
            transform.localPosition = new Vector3(0, transform.localPosition.y, 0);
            //transform.rotation = Quaternion.identity;

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (player == null)
            return;

        if (collision.collider.CompareTag("Obstacle"))
        {
            player.RemoveCube(this.gameObject);
            player = null;
        }


        if (collision.collider.CompareTag("Cube"))
        {
            player.AddNewCube(collision.collider.gameObject);
        }

        if (player != null && player.currentCubes.Contains(this.gameObject) && collision.other.CompareTag("Ground"))
        {
            player.StartLine(gameObject );
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if(player != null && player.currentCubes.Contains(this.gameObject) && collision.other.CompareTag("Ground"))
        {
            player.DrawLine(gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Pool") && player != null)
        {
            player.RemoveCube(gameObject);
            Destroy(gameObject);
        }
    }

}
