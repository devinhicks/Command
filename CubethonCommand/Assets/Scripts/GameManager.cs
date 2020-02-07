using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    bool gameHasEnded = false;

    public float restartDelay = 1f;

    public GameObject completeLevelUI;
    public GameObject gameOverUI;

    public int collected;

    public bool playerActivatedReplay = false;
    bool instantReplay = false;
    GameObject player;

    public Canvas ReplayUI;

    float replayStartTime;

    void Start()
    {
        PlayerMovement pm = FindObjectOfType<PlayerMovement>();
        player = pm.gameObject;

        if (CommandLog.commands.Count > 0 && CommandLog.doQueue)
        {
            Debug.Log("\n\nINSTANT REPLAY!!\n\n");
            instantReplay = true;
            replayStartTime = Time.timeSinceLevelLoad;
        }
    }

    void Update()
    {
        if (instantReplay)
        {
            RunInstantReplay();
        }
    }

    public void ComepleteLevel()
    {
        completeLevelUI.SetActive(true);
    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            gameOverUI.SetActive(true);
            Invoke("Restart", restartDelay); // Restart after specfied delay
        }
    }

    void Restart()
    {
        // if player does not end game, restart the current scene/level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RunInstantReplay()
    {
        if (CommandLog.commands.Count == 0)
        {
            return;
        }

        Command command = CommandLog.commands.Peek();
        if (Time.timeSinceLevelLoad >= command.timestamp)
        {
            command = CommandLog.commands.Dequeue();
            command._player = player.GetComponent<Rigidbody>();
            Invoker invoker = new Invoker();
            invoker.disableLog = true;
            invoker.SetCommand(command);
            invoker.ExecuteCommand();
        }
    }

    public void activateReplay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        CommandLog.doQueue = true;
        ReplayUI.gameObject.SetActive(false);
        //playerActivatedReplay = true;
    }
}
