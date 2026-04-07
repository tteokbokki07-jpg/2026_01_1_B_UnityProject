using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CubeGameUI : MonoBehaviour
{
    public TextMeshProUGUI TimerText;
    public float Timer;


    void Start()
    {
        
    }

    void Update()
    {
        Timer += Time.deltaTime;
        TimerText.text = "생존시간 : " + Timer.ToString("0.00");
    }
}
