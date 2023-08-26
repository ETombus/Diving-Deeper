using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointMaster : MonoBehaviour
{
    private int points = 0;
    private float finalTime;

    [SerializeField]private GameObject finalDestination;
    [SerializeField] private GameObject finalRadar;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private TMP_Text timeText;


    public float time = 0;

    public static PointMaster Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        time += Time.deltaTime;
    }


    public static string TimeToString(float time)
    {
        string timeString = "";

        timeString += Mathf.Floor(time / 60).ToString();
        timeString += ":";
        if (Mathf.Floor(time % 60) < 10)
            timeString += "0";
        timeString += Mathf.Floor(time % 60).ToString();

        return timeString;
    }

    public void PointUp()
    {
        points++;

        if (points >= 4)
        {
            finalDestination.SetActive(true);
            finalRadar.SetActive(true);
        }
    }

    public void Win()
    {
        finalTime = time;
        winScreen.SetActive(true);
        timeText.text = TimeToString(finalTime);
        Time.timeScale = 0;
    }
}
