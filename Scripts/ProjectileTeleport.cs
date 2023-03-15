using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Script to control projectile travel between portals
*/

public class ProjectileTeleport : MonoBehaviour
{
    // Variables

    //Associated Teleport (child)
    public Transform targerTeleport;  


    private void OnTriggerEnter(Collider other)
    {
        // If the collision is with a Projectile teleport it to the target Teleport
        if(other.CompareTag("Projectile"))
        {
            other.transform.position = targerTeleport.position;              
        }
    }
}
