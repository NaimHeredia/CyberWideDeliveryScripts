using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    NOT USED
    Script to create a 2D projectile
*/

public class Projectile2D : MonoBehaviour
{
    public float force = 10.0f;
    public Rigidbody2D rb;
    public float lifetime = 0.05f;


    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(killBullet());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            player.transform.position = transform.position;
            Destroy(gameObject);
            player.GetComponent<PlayerMove>().BulletCount--;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
            player.GetComponent<PlayerMove>().BulletCount--;
        }
    }    

    IEnumerator killBullet()
    {            
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
        player.GetComponent<PlayerMove>().BulletCount--;                
    }   
}
