using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    NOT USED
    Script for the player's projectile
*/

public class Projectile : MonoBehaviour
{
    // Variables
    public float force = 10.0f;
    public Rigidbody rb;
    public float lifetime = 0.05f;
    
    // Player Reference
    private GameObject player;

    // Set lifetime of the projectile and populate variables
    private void Awake()
    {
        Destroy(gameObject, lifetime);
        rb.velocity = transform.forward * force;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update checks to allow player to teleport to the projectile and destroy the projectile by a clicking the mouse
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            player.transform.position = transform.position;
            Destroy(gameObject);
        }
    }

    // Destroy the projectile on trigger enter
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
