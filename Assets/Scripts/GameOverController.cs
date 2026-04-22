using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;

public class GameOverController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI titleText;
    //[SerializeField] private SFXManager sfxManager;
    [SerializeField] private Light spotlight;
    [SerializeField] private GameObject player;
    [SerializeField] private SFXManager sfxManager;
    
    void Start()
    {
        player.GetComponent<ContinuousMoveProvider>().enabled = false;
        int score = PlayerPrefs.GetInt("Score", 0);
        int quota = PlayerPrefs.GetInt("Quota", 0);
        if(score >= quota)
        {
            titleText.text = "Shift completed";
            gameOverText.text = "Congratulations. Your efficiency metrics were acceptable. You have earned the right to sleep for 4 hours before your next mandatory shift. \n\nDon't get used to the sunlight.";
            spotlight.intensity= 20f;
            sfxManager.PlayGoodEndingMusic();
        }
        else
        {
            titleText.text = "Contract terminated? No.";
            gameOverText.text = "You failed to meet the quota. The exit doors have been permanently welded shut. \n\nYou are no longer a cook; you are now part of the kitchen's infrastructure. \n\nHope you like the smell of grease... forever";
            spotlight.intensity= 0.0f;
            sfxManager.PlayBadEndingMusic();
        }
    }

    public void newMatch()
    {
        PlayerPrefs.SetInt("Score", 0);
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    
}
