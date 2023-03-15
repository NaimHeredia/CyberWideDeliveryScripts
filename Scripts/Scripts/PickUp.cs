using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Script to handle player pick ups
*/

public class PickUp : MonoBehaviour
{
    // Variables
    // Player Reference
    private GameObject player;
    private AudioClip pickUpClip;

    private void Awake()
    {
        // Populater Player Reference
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        // When player collides with the pickup 
        if (other.gameObject.CompareTag("Player"))
        {
            // Player obtains a teleporter (bool on PlayerBody)
            player.GetComponent<PlayerMove>().hasTeleporter = true;

            AudioManager.Instance.PlaySFX(pickUpClip);

            //Pickup Gets Destroyed
            Destroy(gameObject);
        }
    }
}
