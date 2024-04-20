using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Threading;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    [SerializeField]
    private float count;
    [SerializeField]
    private int minute;
    [SerializeField]
    private int hour;
    [SerializeField]
    private int day;
    [SerializeField]
    private int year;
    private int daysInYear = 90;
    [SerializeField]
    private int timeScaleMuliplier;

    [SerializeField]
    private float startTimeHour;

    [SerializeField]
    private Light sunLight;

    [SerializeField]
    private float sunriseHour;

    [SerializeField]
    private float sunsetHour;

    public TMP_Text dayText;
    public TMP_Text yearText;
    public TMP_Text hourText;


    // Start is called before the first frame update
    void Start()
    {
        minute = 1;
        hour = 8;
        day = 1;
        year = 1;
        count = 0.0f;
        timeScaleMuliplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // Every second increase the minute by 1 times the time scale
        count += (Time.deltaTime * timeScaleMuliplier);

        if (count > 1.0)
        {
            // Debug.Log(minute);
            minute += 1;
            count = 0.0f;
            // Update date values
            if (minute >= 60)
            {
                minute = 1;
                hour += 1;

                if (hour >= 24)
                {
                    hour = 1;
                    day += 1;

                    if (day >= daysInYear)
                    {
                        year += 1;
                    }
                }
            }
            updateUI();
            rotateSun(hour, minute);
        }
    }

    public void advanceDay()
    {
        Debug.Log("In function: advanceDay");
        day = day + 1;
        minute = 1;
        hour = 6;

        updateUI();
    }

    private void updateUI()
    {
        yearText.text = "Year " + year.ToString();
        dayText.text = "Day " + day.ToString();

        if(minute < 10)
        {
            if(hour <= 11)
            {
                // AM
                hourText.text = hour.ToString() + ":0" + minute.ToString() + " AM";
            }
            else if (hour == 12)
            {
                hourText.text = hour.ToString() + ":0" + minute.ToString() + " PM";
            }
            else
            {
                // PM
                hourText.text = (hour - 12).ToString() + ":0" + minute.ToString() + " PM";
            }
        }
        else
        {
            if (hour <= 11)
            {
                // AM
                hourText.text = hour.ToString() + ":" + minute.ToString() + " AM";
            }
            else if(hour == 12)
            {
                hourText.text = hour.ToString() + ":" + minute.ToString() + " PM";
            }
            else
            {
                // PM
                hourText.text = (hour - 12).ToString() + ":" + minute.ToString() + " PM";
            }
        }
        
    }

    private void rotateSun(int sunHour, int sunMinute)
    {
        // Move sun once every minute
        float minutesPast = (sunHour * 60) + sunMinute;
        // Calculate time of day based on how far through the day we are
        float percentDoneDay = minutesPast / 1440;

        float sunRotation = 360 * percentDoneDay;

        // The start of the day is at rotarion value 270
        sunRotation = sunRotation + 270.0f;

        //Rotate the sun
        sunLight.transform.rotation = Quaternion.AngleAxis(sunRotation, Vector3.right);

    }
}
