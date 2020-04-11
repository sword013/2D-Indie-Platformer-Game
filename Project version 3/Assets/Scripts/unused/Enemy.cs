using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


    Rigidbody2D myRigidBody2D;
    BoxCollider2D myCollider2D;
    public float enemySpeed=30f;
    bool touchingGround; 

	// Use this for initialization
	void Start () {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myCollider2D = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
      
        /*touchingGround = myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"));
        Debug.Log(touchingGround);*/

        if (IsFacingRight())  
            myRigidBody2D.velocity = new Vector2(enemySpeed*Time.deltaTime,0f);
        else
            myRigidBody2D.velocity = new Vector2(-enemySpeed * Time.deltaTime, 0f);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        FlipEnemy();
       
    }

    void FlipEnemy()
    {
       transform.localScale = new Vector2(-Mathf.Sign(myRigidBody2D.velocity.x),1f);
        
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
        
    }

}
