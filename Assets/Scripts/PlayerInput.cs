using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; 

public class PlayerInput : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidBody2D;

    [SerializeField] float moveSpeedModifier;

   
    void Start()
    {
         myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Run();
        FlipSprite();
    }


    void OnMove(InputValue value){

        moveInput = value.Get<Vector2>();

    }

    void Run(){
        Vector2  playerVelocity = new Vector2 (moveInput.x * moveSpeedModifier, myRigidBody2D.velocity.y);
        myRigidBody2D.velocity = playerVelocity;

    }

    void FlipSprite(){

        bool playerHorizontalSpeed = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;

        if(playerHorizontalSpeed){

         transform.localScale =  new Vector2(Mathf.Sign(myRigidBody2D.velocity.x), 1f);
        }
    }
}
