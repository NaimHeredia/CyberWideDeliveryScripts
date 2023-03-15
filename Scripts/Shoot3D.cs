using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Script to instantiate the projectile by the player with a mouse click
*/

public class Shoot3D : MonoBehaviour
{
    // Variables
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private GameObject teleporterPrefab;
    [SerializeField]
    private float force;

    private PlayEffect fireEffect;

    // Player Reference
    private GameObject player;

    public AudioClip teleportFire;

    private void Awake()
    {
        // Populates the Player reference
        player = player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        // Projectile
        if (Input.GetMouseButtonDown(0))
        {
            AudioManager.Instance.PlaySFX(teleportFire);
            // Check Projectile count (Only 1 can exist on scene)
            if (player.GetComponent<PlayerMove>().BulletCount == 0)
            {
                GameObject spawnedProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

                // Set and limit velocity to 2D
                if (player.transform.localRotation.eulerAngles.y < 180)
                {
                    spawnedProjectile.GetComponent<Rigidbody>().velocity = spawnedProjectile.GetComponent<Transform>().right * force;
                }
                else if (player.transform.localRotation.eulerAngles.y >= 180)
                {
                    spawnedProjectile.GetComponent<Rigidbody>().velocity = -spawnedProjectile.GetComponent<Transform>().right * force;
                }

                // Decrease count
                player.GetComponent<PlayerMove>().BulletCount = 2;

            }
        }

        // Teleporter
        if (Input.GetMouseButtonDown(1))
        {
            if (player.GetComponent<PlayerMove>().hasTeleporter)
            {
                Instantiate(teleporterPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);

                player.GetComponent<PlayerMove>().hasTeleporter = false;
            }
        }
    }   
}
