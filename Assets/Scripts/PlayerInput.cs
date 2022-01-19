using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; 

public class PlayerInput : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidBody2D;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    BoxCollider2D footCollider;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float runSpeed = 5f; 
    [SerializeField] float climbSpeed;
    [SerializeField] Vector2 deathKick = new Vector2(200f,50f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    float gravityScaleStart;
    bool isAlive = true;
    

   
    void Start()
    {
         myRigidBody2D = GetComponent<Rigidbody2D>();
         myAnimator = GetComponent<Animator>();
         myCapsuleCollider = GetComponent<CapsuleCollider2D>();
         gravityScaleStart = myRigidBody2D.gravityScale; 
         footCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
       if (!isAlive)
       {
         return;
       }
          Run();
        ClimbLadder();
        FlipSprite();
        Die(); 
    }

    void Die(){

        if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemy","Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("isDying");
            myRigidBody2D.velocity = deathKick;
        }
        
    }
    void OnMove(InputValue value){
        if (!isAlive)
       {
         return;
       }
        moveInput = value.Get<Vector2>();

    }
    void OnJump(InputValue value){
        if (!isAlive)
       {
         return;
       }

       if(!footCollider.IsTouchingLayers(LayerMask.GetMask("foreground"))){
           
           return;
       }
        if(value.isPressed) {

            myRigidBody2D.velocity += new Vector2 (0f , jumpSpeed);
            
        }


    }
    void ClimbLadder(){

      
         if(!footCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))){
             myRigidBody2D.gravityScale = gravityScaleStart;
             myAnimator.SetBool("isClimbing", false);
           return;
       }
        Vector2 climbVelocity = new Vector2 (myRigidBody2D.velocity.x, moveInput.y  * climbSpeed);
        myRigidBody2D.velocity = climbVelocity;
        myRigidBody2D.gravityScale = 0f;

         bool playerhasVerticalSpeed = Mathf.Abs(myRigidBody2D.velocity.y) > Mathf.Epsilon;
         myAnimator.SetBool("isClimbing", playerhasVerticalSpeed);

    }
    void Run(){

      

        Vector2  playerVelocity = new Vector2 (moveInput.x * runSpeed, myRigidBody2D.velocity.y);
        myRigidBody2D.velocity = playerVelocity;


        bool playerHorizontalSpeed = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;

        myAnimator.SetBool("isRunning", playerHorizontalSpeed);
    }

    void FlipSprite(){

        bool playerHorizontalSpeed = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;

        if(playerHorizontalSpeed){

         transform.localScale =  new Vector2(Mathf.Sign(myRigidBody2D.velocity.x), 1f);
        }
    }

    void OnFire(InputValue value){

        if(!isAlive){return;}
        Instantiate(bullet, gun.position, transform.rotation);
    }
   
}
