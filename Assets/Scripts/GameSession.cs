using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int playerLives = 3;
    [SerializeField] TextMeshProUGUI livestext;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int score = 0;
    //[SerializeField] int oneUp = 1;
   void Awake()
   {
       int numGameSession = FindObjectsOfType<GameSession>().Length;
       if(numGameSession > 1){
           Destroy(gameObject);
       } else {
           DontDestroyOnLoad(gameObject);
       }
   }
      void Start()
     {
         livestext.text = playerLives.ToString();
        scoreText.text = score.ToString();
     }
   public void ProcessPlayerDeath(){
       if(playerLives > 0) {
           TakeLife();
       }else {
           ResetGameSession();
       }
           
       
   }
   public void AddToScore(int pointsToAdd){

       score += pointsToAdd;
       scoreText.text = score.ToString();
   }
   public void AddToLives(){
      playerLives++;
      livestext.text = playerLives.ToString();
   }
  void TakeLife(){
      playerLives--;
      int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
      SceneManager.LoadScene(currentSceneIndex);
      livestext.text = playerLives.ToString();
  }
  void ResetGameSession(){
      FindObjectOfType<ScenePersist>().ResetScenePersist();
      SceneManager.LoadScene(0);
      Destroy(gameObject);
      
  }
}
