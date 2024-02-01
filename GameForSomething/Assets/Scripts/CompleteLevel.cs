
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{

    public GameManager gameManager;

    void OnTriggerEnter() {
        gameManager.CompleteLevel();
    }
}
