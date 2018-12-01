using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTimerUI : SingletonBehaviour<MiniGameTimerUI> {

    public TMPro.TextMeshPro textMeshPro;

    private float totalTime;
    private float timeLeft;
    private System.DateTime startTime;
    private bool timerGoing;
    private float accelerateTime = 0.1f;


    public delegate void TimeUp();
    public static event TimeUp OnTimeUp;

	private void Start()
	{
        timerGoing = false;
	}

	private void Update()
    {
        if (!timerGoing)
            return;

        System.DateTime now = System.DateTime.Now;
        timeLeft = totalTime - (float)(now - startTime).TotalSeconds;
        UpdateUI();
        if (timeLeft < 0)
            FinishTimer();
    }

    private void UpdateUI()
    {
        textMeshPro.text = timeLeft.ToString("F2");
    }

    private void FinishTimer()
    {
        //post event
        OnTimeUp();

        timerGoing = false;
        timeLeft = 0;
        UpdateUI();
        textMeshPro.gameObject.SetActive(false);
    }

    public void ResetTimer(float seconds)
    {
        totalTime = seconds;
        timeLeft = totalTime;
        textMeshPro.text = totalTime.ToString("F2");
    }

    public void StartTimer()
    {
        timerGoing = true;
        startTime = System.DateTime.Now;
        textMeshPro.gameObject.SetActive(true);
    }
}
