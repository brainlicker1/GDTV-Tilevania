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
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float runSpeed = 5f; 
    [SerializeField] float climbSpeed;
    float gravityScaleStart;

   
    void Start()
    {
         myRigidBody2D = GetComponent<Rigidbody2D>();
         myAnimator = GetComponent<Animator>();
         myCapsuleCollider = GetComponent<CapsuleCollider2D>();
         gravityScaleStart = myRigidBody2D.gravityScale; 
    }

    void Update()
    {
        Run();
        ClimbLadder();
        FlipSprite();
    }


    void OnMove(InputValue value){

        moveInput = value.Get<Vector2>();

    }
    void OnJump(InputValue value){

       if(!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("foreground"))){
           
           return;
       }
        if(value.isPressed) {

            myRigidBody2D.velocity += new Vector2 (0f , jumpSpeed);
            
        }


    }
    void ClimbLadder(){

         if(!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))){
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
}
