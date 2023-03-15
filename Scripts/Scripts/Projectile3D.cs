using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*    
    Script for the player's projectile
*/

public class Projectile3D : MonoBehaviour
{        
    // Variables
    public float lifetime = 0.5f;

    // Player Reference
    private GameObject player;
    private PlayEffect fireEffect;

    private void Awake()
    {
        // Populates the Player reference
        player = player = GameObject.FindGameObjectWithTag("Player");

        // Coroutine to Kill the projectile (using lifetime variable)
        StartCoroutine(KillProjectile());
    }

    private void Update()
    {        
        // When a projectile exists on screen and the fire button is pressed a second time teleportation of the player occurs
        if (Input.GetMouseButtonDown(0))
        {
            // Teleport
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            player.transform.position = transform.position;

            // Destroy Projectile
            Destroy(gameObject);

            // Decrease Count
            player.GetComponent<PlayerMove>().BulletCount = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Projectile gets destroyed if it collides with anything other than the player
        if (!collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
            player.GetComponent<PlayerMove>().BulletCount = 0;
        }        
    }

    // Coroutine
    IEnumerator KillProjectile()
    {
        // Destroy the Projectile

        yield return new WaitForSeconds(lifetime/2);
        StartCoroutine(Flicker());
        
        yield return new WaitForSeconds(lifetime);
        
        Destroy(gameObject);

        // Decrease Projectile
        player.GetComponent<PlayerMove>().BulletCount = 0;
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            yield return new WaitForSeconds(0.1f);
            gameObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            yield return new WaitForSeconds(0.1f);
        }
    }
}
