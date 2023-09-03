using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class OwlBehaviour : MonoBehaviour
{
    // References
    private AIPath owlAI;
    private GameObject player;

    private Vector2 idlePosition = new Vector2(79.4f, 9.1f);
    public int activateDistance = 50;

    // Start is called before the first frame update
    void Start()
    {
        owlAI = GetComponent<AIPath>();
        player = GameObject.FindGameObjectWithTag("Player");
    }   

    // Update is called once per frame
    void Update()
    {
        // If Player is not in range of owl, disable movement. Else, enable movement
        if (Vector3.Distance(owlAI.transform.position, player.transform.position) > activateDistance)
        {
            owlAI.canMove = false;
            owlAI.destination = idlePosition;
        } else
        {
            owlAI.canMove = true;
            owlAI.destination = player.transform.position;
        }
    }
}
