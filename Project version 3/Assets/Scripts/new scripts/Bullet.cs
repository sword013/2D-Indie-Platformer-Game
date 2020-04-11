using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public GameObject XplosionFX;
    Rigidbody2D rigidBody2D;
    BoxCollider2D coll2D;

    [SerializeField]
    bool TrapExitedGround;

    private void Start() {

        rigidBody2D = GetComponent<Rigidbody2D>();
        coll2D = GetComponent<BoxCollider2D>();

        rigidBody2D.velocity = transform.right * speed;

        TrapExitedGround = false;
    }

    private void OnTriggerExit2D(Collider2D other) {
        // Debug.Log(other.gameObject.layer);
        if (gameObject.layer == 16 && !TrapExitedGround)
        {
            TrapExitedGround = true;
        }
    }

    // TODO! - change all Destroy(gameObject) to a poolable logic for performance optimization, Instantiate and destorying is usually bad
    void OnTriggerEnter2D(Collider2D other) {
    
        if (gameObject.layer == 16) //traps
        {
            if (TrapExitedGround && !other.tag.Equals("Player")) // if previously exited ground, destory on colliding with wall/ceiling..
            {
                Destroy(gameObject);
            }
            return; // return if traps and not bullets
        }
        

        if (other.gameObject.layer == 12) //enemy 
        {
            Debug.Log("hit enemy");
            GameManager.instance.AddScore(1);
            SpawnFX();
            Destroy(other.gameObject);  //TODO: create funct to deal damage and handel player/enemy health later
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == 8) // ground layer
        {
            Destroy(gameObject);
        }
    }

    void SpawnFX()
    {
        Instantiate(XplosionFX, transform.position, transform.rotation);
    }
}
