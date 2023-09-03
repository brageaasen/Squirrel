using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //public GameObject checkpointReachedUI;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.tag == "Player")
        {
            Player.lastCheckpointPos = this.transform.position;
        }
    }
}
