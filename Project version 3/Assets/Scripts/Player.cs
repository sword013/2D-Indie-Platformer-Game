using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Configs
    [SerializeField] float runSpeed = 4f;
    [SerializeField] float jumpSpeed = 16f;
    [SerializeField] float climbSpeed = 10f;
    [SerializeField] Vector2 deathKick = new Vector2(25f,25f);
    [SerializeField] Projectile projectile;

    //State 
    public bool isAlive = true; 

    //Cached component references
    Rigidbody2D myRigidBody2D;
    Animator myAnimator;
    // CapsuleCollider2D myCollider2D;
    BoxCollider2D myCollider2D;
    CapsuleCollider2D capsuleCollider2D;
    float gravityScaleAtStart;
    



    private void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        projectile = GetComponent<Projectile>();
        // TIP - Changed to box collider for better ground detection and takeoff... layer mask was only checking capsule when the box is a more accurate check
        // myCollider2D = GetComponent<CapsuleCollider2D>();
        myCollider2D = GetComponent<BoxCollider2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();

        gravityScaleAtStart = myRigidBody2D.gravityScale;
       
    }

    // TIP - input should be obtained in update and physics should be calculated in fixed update.
    void Update () {

        if (!isAlive)
            return;

        Run();          //move our character
        Climb();
        Jump();         //check for jump
        FlipPlayer();   //adjust the scale , >> or << 
        Die();
      

	}


    private void Jump()
    {
        /*
          we're only concerned about the y velocity ;
          we want it continuous so use Input Manager ; also we dont want it from vertical axis as it will go from 0 to 1; 
          we want a constant velocity to be given
        */
        

        //if anyone presses jump button from input manager 
        if (Input.GetButtonDown("Jump"))
        {
            //check if the player is on ground
            bool playerOnGround = myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"));
            if (playerOnGround)
            {
                PlayJumpAudio();
                myRigidBody2D.velocity = Vector2.zero; // added to fix a werid bug where sometimes velocity would triple 
                myRigidBody2D.velocity += new Vector2(0f, jumpSpeed);
            }
        }


        /*
          Another solution :  
           bool playerOnGround = myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"));
           if(!playerOnGround){ return; } 
           if(Imput.GetButtonDown("Jump"){
                //player is touching 
                myRigidBody2D.velocity += new Vector2(0f,jumpSpeed);
            }
           
         */
    }

    public void BtnJump() // to hook up to ui button
    {
        bool playerOnGround = myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (playerOnGround)
        {
            PlayJumpAudio();
            myRigidBody2D.velocity = Vector2.zero;
            myRigidBody2D.velocity += new Vector2(0f, jumpSpeed);
        }
    }
    void PlayJumpAudio()
    {
        string JumpSound = Random.Range(0.0f, 1.0f) > 0.5f ? "Jump1":"Jump2";
        AudioManager.instance.PlaySfx(JumpSound);
    }

    private void Run()
    {

        float displacementX = SimpleInput.GetAxis("Horizontal");
        //pratek frame madhe honara displacement ; note : it increases from 0 to 1 for right and to -1 for left

        myRigidBody2D.velocity = new Vector2(displacementX * runSpeed, myRigidBody2D.velocity.y);
        //karan transform.position use kartanna player can cross the collider(sometimes cuz its faster than update) ;

       

        //switch to running animation if player got speed (that means he's running)
        bool hasHorizontalSpeed = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("IsRunning",hasHorizontalSpeed);

    }

    private void FlipPlayer()
    {
        bool hasHorizontalSpeed = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;

        if (hasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody2D.velocity.x),1f);
            
            //dont change y's scale so 1f

            /* 
                Math.Sign(+,0) = returns 1;
                Math.Sign(-) = returns -1;


                Math.Abs(+,-) = + ; just like mod of a number

                Math.Epsilon = smallest possible float greater than 0 ; basically  its 0 to say
             */
        }
    }


    private void Climb()
    {
        bool touchingLadder = myCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing"));
        if (!touchingLadder)
        {
            //not climbing
            myAnimator.SetBool("IsClimbing", false);
            myRigidBody2D.gravityScale = gravityScaleAtStart; 
            return;
        }

        //bug = player gets stuck at the center of the ladder
        //CLIMBING 
        float displacementY = SimpleInput.GetAxis("Vertical");
        myRigidBody2D.velocity = new Vector2(myRigidBody2D.velocity.x, displacementY*climbSpeed);
        myRigidBody2D.gravityScale = 0f;

        //go to climb animation
        bool hasVerticalSpeed = Mathf.Abs(myRigidBody2D.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("IsClimbing",hasVerticalSpeed);
        
    }


    private void Die()
    {
        if (myCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy","Hazards","Traps")) || capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy","Hazards","Traps")))
        {
            myAnimator.SetTrigger("Die");
            GetComponent<Rigidbody2D>().velocity = deathKick;
            isAlive = false;
            // FindObjectOfType<GameSession>().ProcessPlayerDeath();  // performance heavy, avoid using Find as much as possible
            GameManager.instance.PlayerDeath();
        }
    }

}
