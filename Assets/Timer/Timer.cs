using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float countDownTime = 40f;
    public Text countDownDisplay;
   
    
    void Update()
    {
        countDownTime -= Time.deltaTime;
        if (countDownTime < 0)
        {
            countDownTime = 0;
        }
        countDownDisplay.text = "Time Left : " + Mathf.Round(countDownTime);
    }
}
