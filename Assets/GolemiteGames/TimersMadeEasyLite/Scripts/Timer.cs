using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public UnityEvent onTimerEnd;

    [Range(0, 23)]
    public int hours;
    [Range(0, 59)]
    public int minutes;
    [Range(0, 59)]
    public int seconds;

    public enum SeperatorType
    {
        Colon,
        Bullet,
        Slash
    };

    public enum OutputType
    {
        None,
        StandardText,
        TMPro,
        HorizontalSlider,
        Dial
    };

    [Tooltip("If checked, runs the timer on play")]
    public bool startAtRuntime = true;

    [Tooltip("Select what to display")]
    public bool hoursDisplay = false;
    public bool minutesDisplay = true;
    public bool secondsDisplay = true;

    [Tooltip("Select the output type")]
    public OutputType outputType;
    public Text standardText;
    public TextMeshProUGUI textMeshProText;
    public Slider standardSlider;
    public Image dialSlider;

    bool timerRunning = false;
    bool timerPaused = false;
    public double timeRemaining;

    private void Awake()
    {
        if (!standardText && GetComponent<Text>())
        {
            standardText = GetComponent<Text>();
        }
        if (!textMeshProText && GetComponent<TextMeshProUGUI>())
        {
            textMeshProText = GetComponent<TextMeshProUGUI>();
        }
        if (!standardSlider && GetComponent<Slider>())
        {
            standardSlider = GetComponent<Slider>();
        }
        if (!dialSlider && GetComponent<Image>())
        {
            dialSlider = GetComponent<Image>();
        }

        if (standardSlider)
        {
            standardSlider.maxValue = ReturnTotalSeconds();
            standardSlider.value = standardSlider.maxValue;
        }
        if (dialSlider)
        {
            dialSlider.fillAmount = 1f;
        }
    }

    void Start()
    {
        if (startAtRuntime)
        {
            StartTimer();
        }
        else
        {
            timeRemaining = ReturnTotalSeconds();
            DisplayInTextObject();
        }
    }

    void Update()
    {
        if (timerRunning)
        {
            CountDown();

            if (standardSlider)
            {
                StandardSliderDown();
            }
            if (dialSlider)
            {
                DialSliderDown();
            }
        }
    }

    private void CountDown()
    {
        if (timeRemaining > 0.02)
        {
            timeRemaining -= Time.deltaTime;
            DisplayInTextObject();
        }
        else
        {
            // Waktu habis: restart otomatis
            timeRemaining = ReturnTotalSeconds();
            onTimerEnd.Invoke();
            DisplayInTextObject();

            // Reset slider/dial
            if (standardSlider)
            {
                standardSlider.value = standardSlider.maxValue;
            }
            if (dialSlider)
            {
                dialSlider.fillAmount = 1f;
            }
        }
    }

    private void StandardSliderDown()
    {
        if (standardSlider.value > standardSlider.minValue)
        {
            standardSlider.value -= Time.deltaTime;
        }
    }

    private void DialSliderDown()
    {
        float timeRangeClamped = Mathf.InverseLerp(ReturnTotalSeconds(), 0, (float)timeRemaining);
        dialSlider.fillAmount = Mathf.Lerp(1, 0, timeRangeClamped);
    }

    private void DisplayInTextObject()
    {
        if (standardText)
        {
            standardText.text = DisplayFormattedTime(timeRemaining);
        }
        if (textMeshProText)
        {
            textMeshProText.text = DisplayFormattedTime(timeRemaining);
        }
    }

    public double GetRemainingSeconds()
    {
        return timeRemaining;
    }

    public void StartTimer()
    {
        if (!timerRunning && !timerPaused)
        {
            ResetTimer();
            timerRunning = true;
        }
    }

    public void StopTimer()
    {
        timerRunning = false;
        ResetTimer();
    }

    private void ResetTimer()
    {
        timerPaused = false;
        timeRemaining = ReturnTotalSeconds();
        DisplayInTextObject();

        if (standardSlider)
        {
            standardSlider.maxValue = ReturnTotalSeconds();
            standardSlider.value = standardSlider.maxValue;
        }

        if (dialSlider)
        {
            dialSlider.fillAmount = 1f;
        }
    }

    public float ReturnTotalSeconds()
    {
        float totalTimeSet = hours * 3600 + minutes * 60 + seconds;
        return totalTimeSet;
    }

    public double ConvertToTotalSeconds(float hours, float minutes, float seconds)
    {
        timeRemaining = hours * 3600 + minutes * 60 + seconds;
        DisplayFormattedTime(timeRemaining);
        return timeRemaining;
    }

    public string DisplayFormattedTime(double remainingSeconds)
    {
        string convertedNumber;
        float h, m, s;
        RemainingSecondsToHHMMSSMMM(remainingSeconds, out h, out m, out s);

        string HoursFormat()
        {
            if (hoursDisplay)
            {
                string formatted = string.Format("{0:00}", h);
                if (minutesDisplay || secondsDisplay)
                    formatted += ":";
                return formatted;
            }
            return null;
        }

        string MinutesFormat()
        {
            if (minutesDisplay)
            {
                string formatted = string.Format("{0:00}", m);
                if (secondsDisplay)
                    formatted += ":";
                return formatted;
            }
            return null;
        }

        string SecondsFormat()
        {
            if (secondsDisplay)
            {
                return string.Format("{0:00}", s);
            }
            return null;
        }

        convertedNumber = HoursFormat() + MinutesFormat() + SecondsFormat();
        return convertedNumber;
    }

    private static void RemainingSecondsToHHMMSSMMM(double totalSeconds, out float hours, out float minutes, out float seconds)
    {
        hours = Mathf.FloorToInt((float)totalSeconds / 3600);
        minutes = Mathf.FloorToInt(((float)totalSeconds / 60) - (hours * 60));
        seconds = Mathf.FloorToInt((float)totalSeconds - (hours * 3600) - (minutes * 60));
    }

    private void OnValidate()
    {
        timeRemaining = ConvertToTotalSeconds(hours, minutes, seconds);
    }
}
