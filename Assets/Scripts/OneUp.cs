using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneUp : MonoBehaviour
{
   [SerializeField] AudioClip oneUpSFX;
   
   bool wasCollected = false;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !wasCollected) {
            wasCollected = true;
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(oneUpSFX, Camera.main.transform.position );
            FindObjectOfType<GameSession>().AddToLives();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
