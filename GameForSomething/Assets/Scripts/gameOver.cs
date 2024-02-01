using UnityEngine;

public class gameOver : MonoBehaviour
{

    public GameObject gmo;

    public void GameOver()

    {
        FindObjectOfType<GameManager>().EndGame();
    }
}
