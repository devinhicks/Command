using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public bool askRestart = false;
    public Canvas ReplayUI;
    public Rigidbody player;

    public GameManager gameManager;

    public void Start()
    {
        player = player.GetComponent<Rigidbody>();
    }
    public void LoadNextLevel()
    {
        if (!askRestart)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            gameManager.activateReplay();

        }
        else
        {
            player.gameObject.SetActive(false);
            ReplayUI.gameObject.SetActive(true);
        }
    }
}