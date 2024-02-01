using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public float DelayR = 1f;
    bool gameHasEnded = false;
    public NextLevel nextlevel;

    public GameObject completelevel;

    public GameObject gameover;
    
    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over!!");
            Restart();
        }
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void CompleteLevel()
    {
        completelevel.SetActive(true);
    }
}
