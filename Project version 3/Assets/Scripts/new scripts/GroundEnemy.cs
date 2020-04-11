using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    public float Speed;
    public LayerMask layerMask;

    Rigidbody2D rb2D;
    Transform trans;
    float width, height;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        trans = gameObject.transform;
        width = this.GetComponent<SpriteRenderer>().bounds.extents.x;
        height = this.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    void FixedUpdate()
    {
        // move foward along gameobjects local forward dir
        rb2D.velocity = trans.right * Speed;

        Vector3 linecastpos = trans.position + trans.right * width;
        Debug.DrawLine(linecastpos, linecastpos + trans.right * 0.1f);
        Debug.DrawLine(linecastpos, linecastpos - new Vector3(0, height));
        RaycastHit2D Down = Physics2D.Raycast(linecastpos, Vector3.down, height, layerMask);
        RaycastHit2D Front = Physics2D.Raycast(linecastpos, trans.right, 0.1f, layerMask);

        if (Front.collider != null || Down.collider == null) // obstructed || not grounded
        {
            Flip();
        }
    }

    void Flip()
    {
        // Debug.Log("flip");
        trans.Rotate(new Vector3(0, 180, 0));
    }


}
