using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using TMPro;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public Transform ClientTarget;
    public Transform ClientDestination;

     public GameObject player; // Continuous Move Provider

    public Canvas startCanvas;
    public int prepTimer = 20;
    public int serveTimer = 300;

    public TextMeshProUGUI timer;
    public TextMeshProUGUI timerHeader;
    public TextMeshProUGUI client;
    public SFXManager sFXManager;
    public SpawnController spawnController;
    public int score;
     public int quota;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        player.GetComponent<ContinuousMoveProvider>().enabled = false;
        client.text = "Clients Served Correctly: " + score + "/" + quota;
    }

    public void EnableMovement()
    {
        player.GetComponent<ContinuousMoveProvider>().enabled = true;
        startCanvas.gameObject.SetActive(false);
        StartCoroutine(MatchTimer());
    }

    public void UpdateScore(int change)
    {
        score += change;
        client.text = "Clients Served Correctly: " + score + "/" + quota;
    }

    IEnumerator PrepTimer()
    {
        int timeRemaining = prepTimer;
        while (timeRemaining > 0)
        {
            timer.text = FormatTime(timeRemaining);
            yield return new WaitForSeconds(1);
            timeRemaining--;

            
            if (timeRemaining == spawnController.minSpawnDelay)
            {
                spawnController.StartSpawning();
            }
        }
    }

    IEnumerator ServeTimer()
    {
        int timeRemaining = serveTimer;
        while (timeRemaining > 0)
        {
            timer.text = FormatTime(timeRemaining);
            yield return new WaitForSeconds(1);
            timeRemaining--;
        }
    }

    IEnumerator MatchTimer()
    {
        yield return StartCoroutine(PrepTimer());
        timerHeader.text = "Time to Serve!";
       
        yield return StartCoroutine(ServeTimer());
        yield return StartCoroutine(EndMatch());
    }

    string FormatTime(int seconds)
    {
        int minutes = seconds / 60;
        int remainingSeconds = seconds % 60;
        return $"{minutes:00}:{remainingSeconds:00}";
    }

    IEnumerator EndMatch()
    {
        if (spawnController != null)
        {
            spawnController.StopSpawning();
        }
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Quota", quota);
        timer.text = "00:00";
        client.text = "";
        yield return new WaitForSeconds(2);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene");
    }
}