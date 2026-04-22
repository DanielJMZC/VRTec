using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;

public class ClienteTimer : MonoBehaviour
{
    float time;
    [SerializeField] private TextMeshPro timerText;
    
    void Start()
    {
        if (CleintController.Instance != null)
        {
            time = CleintController.Instance.waitTimeBeforeOffView;
        }
        else
        {
            Debug.Log("CleintController.Instance no está inicializado");
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
    void CheckTime(){
        if (time <= 0f)
        {
            timerText.text = " ";
            StopAllCoroutines();
            
        }else{
            StartCoroutine(MatchTime());
        }
    }
    IEnumerator MatchTime()
    {
        yield return new WaitForSeconds(1);
        time-=1f;
        ActiveText();
        CheckTime();
    }
}
