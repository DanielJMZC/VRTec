using UnityEngine;
using System.Collections;
using TMPro;

public class ClienteTimer : MonoBehaviour
{
    float time;
    [SerializeField] private TextMeshPro timerText;
    private CleintController client;

    void Awake()
    {
        client = GetComponent<CleintController>();

        
        if (client != null)
        {
            time = client.waitTimeBeforeOffView;
        }
    }


    void ActiveText()
    {
        timerText.text = time.ToString("F0"); 
    }

    public void StartTimer()
    {
        StartCoroutine(MatchTime());
    }

    IEnumerator MatchTime()
    {
        while (time > 0f)
        {
            yield return new WaitForSeconds(1f);
            time -= 1f;
            ActiveText();
        }

        timerText.text = "";
    }
}