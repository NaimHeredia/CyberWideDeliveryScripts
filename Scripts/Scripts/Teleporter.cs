using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Script tto handle Teleporters (Portal left behind by player that allows them to return to this point at any time)
*/

public class Teleporter : MonoBehaviour
{
    // Variables
    // Player Reference
    private GameObject player;

    public AudioClip groundTeleport;

    private void Awake()
    {
        // Populate the Player reference
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        // Checks for input when the teleporter exists on screen
        if (Input.GetMouseButtonDown(1))
        {
            AudioManager.Instance.PlaySFX(groundTeleport);

            // Teleports the Player to the teleporter's position
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            player.transform.position = transform.position;

            // Destroys the Teleporter
            Destroy(gameObject);

            // Player does not have teleporter (bool on PlayerBody)
            player.GetComponent<PlayerMove>().hasTeleporter = false;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            // If they press X the Active teleporter would get destroyed
            Destroy(gameObject);
        }

    }
}
