using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    public Button startButton;
    public GameObject menuPanel;

    void Start()
    {
        startButton.onClick.AddListener(OnStartPressed);
    }

    public void OnStartPressed()
    {
            GameController.Instance.sFXManager.PlayButtonClickSFX();
        menuPanel.SetActive(false);
    }
}
