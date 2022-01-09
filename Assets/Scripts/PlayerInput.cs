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

   
    void Start()
    {
         myRigidBody2D = GetComponent<Rigidbody2D>();
         myAnimator = GetComponent<Animator>();
         myCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        Run();
       
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
