using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInput : MonoBehaviour
{
    Vector2 moveInput;

   
    void Start()
    {
        
    }

    void Update()
    {
        
    }


    void OnMove(InputValue value){

        moveInput = value.Get<Vector2>();

    }
}
     
