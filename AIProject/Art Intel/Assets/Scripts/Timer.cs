using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private GameManager gm;

    public float timeRemaining = 10;
    public bool timerIsRunning = false;

    [SerializeField]
    private int timeOnIncrease = 10;

    public Text timeText;

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
        gm = gameObject.GetComponent <GameManager>();
    }
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                gm.stopGame();
            }
        }

        displayTime(timeRemaining);
    }

    void displayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void addTime()
    {
        timeRemaining += timeOnIncrease;
    }
}