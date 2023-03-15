using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Script to instantiate the projectile by the player with a mouse click
*/

public class Shoot : MonoBehaviour
{
    // Variables
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float force;

    // Player reference
    private GameObject player;
    
    // Set player reference
    private void Awake()
    {        
        player = GameObject.FindGameObjectWithTag("Player");        

    }

    // Instantiate the Projectile only allowing one bullet to exist in the scene, controlling the bullet position and apply force to it's rigidbody for the projetcile to move forward
    void Update()
    {
        //Debug.Log(player.GetComponent<PlayerMove>().BulletCount);  // For Debug  
        if(Input.GetMouseButtonDown(0))
        {
            if (player.GetComponent<PlayerMove>().BulletCount == 0)
            {                
                Vector3 pos = transform.position;
                pos.z = player.transform.position.z;
                GameObject spawnedProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

                // Allow only for 2 directions for the projectile to travel (left or right)
                if (player.transform.localRotation.eulerAngles.y >= 0 && player.transform.localRotation.eulerAngles.y < 180)
                {
                    spawnedProjectile.GetComponent<Rigidbody2D>().velocity = spawnedProjectile.GetComponent<Transform>().right * force;
                }
                else if (player.transform.localRotation.eulerAngles.y >= 180 && player.transform.localRotation.eulerAngles.y <= 360)
                {
                    spawnedProjectile.GetComponent<Rigidbody2D>().velocity = -spawnedProjectile.GetComponent<Transform>().right * force;
                }

                // Increase bullet count to control the amount of bullets in the scene
                player.GetComponent<PlayerMove>().BulletCount++;
            }
        }        
    }
}
