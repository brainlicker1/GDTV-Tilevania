using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D enemyRigidbody;
    [SerializeField] float movementSpeed = 1f;
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();

    }
    void Update()
    {
        enemyRigidbody.velocity = new Vector2(movementSpeed, 0f);
    }


     void OnTriggerExit2D(Collider2D other)
     {
       movementSpeed = -movementSpeed;   
       FlipEnemyFacing();
     }
     void FlipEnemyFacing(){
          

       

         transform.localScale =  new Vector2 (-(Mathf.Sign(enemyRigidbody.velocity.x)), 1f);
        

     }
}
