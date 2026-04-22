using UnityEngine;
using UnityEngine.UI;

public class ShowCanvasOnClick : MonoBehaviour
{
    public Button button;
    public GameObject targetCanvas;

    void Start()
    {
        button.onClick.AddListener(OnButtonPressed);
    }

    public void OnButtonPressed()
    {
        targetCanvas.SetActive(true);
    }
}
