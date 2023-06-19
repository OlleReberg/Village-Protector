using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] private float timeMultiplier;
    [SerializeField] private float startHour;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Light sunLight;
    [SerializeField] private float sunRiseHour;
    [SerializeField] private float sunSetHour;
    
    private DateTime currentTime;
    private TimeSpan sunRiseTime;
    private TimeSpan sunSetTime;
    
    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
        
        sunRiseTime = TimeSpan.FromHours(sunRiseHour);
        sunSetTime = TimeSpan.FromHours(sunSetHour);
    }

    private void Update()
    {
        UpdateTOD();
    }

    private void UpdateTOD()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);
        
        timeText.text = currentTime.ToString("HH:mm");
        
    }
}
