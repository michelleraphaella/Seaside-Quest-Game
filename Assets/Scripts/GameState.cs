using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameState : Singleton<GameState>
{
    public float Target = 1f;
    public Image TargetBar;
    public GameObject pausePanel;
    public GameObject winPanel;

    public enum States
    {
        Playing, Rushed, Paused, GameOver, Win
    }

    public States currentState;

    private float score;
    public TMP_Text ScoreText;
    public TMP_Text Timer;

    public float Score
    {
        get => score;
        set
        {
            score = value;
            if (ScoreText != null) ScoreText.text = $"{score} of 10 Treasure";
            if (TargetBar != null)
            {
                TargetBar.DOFillAmount(score / Target, 0.5f).OnComplete(() =>
                {
                    if (score == Target && currentState == States.Playing)
                    {
                        Win();
                    }
                });
            }
        }
    }

    public float PlayTime = 120f;
    public float TimeLeft;
    public float RushTime = 0.5f;

    public bool IsPlaying => currentState == States.Playing || currentState == States.Rushed;
    public bool IsRushed => currentState == States.Rushed;
    public bool IsPaused => currentState == States.Paused;
    public bool IsGameOver => currentState == States.GameOver;
    public bool IsWin => currentState == States.Win;
    // Start is called before the first frame update
    void Start()
    {
        TimeLeft = PlayTime;
        currentState = States.Playing;
        Time.timeScale = 1f;
        Score = 0f;

        Debug.Log("playing");
        //pausePanel.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            OnPause();
            pausePanel.SetActive(IsPaused);
        }

        if (!IsPlaying) return;

        DisplayTimer(TimeLeft);

        TimeLeft -= Time.deltaTime;
        if (TimeLeft / PlayTime < RushTime) currentState = States.Rushed;
        if (TimeLeft < 0f)
        {
            GameOver();
            SceneManager.LoadScene(3, LoadSceneMode.Single);
        }
    }

    void DisplayTimer(float timeLeft)
    {
        if(timeLeft < 0)
        {
            timeLeft = 0;
        }

        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);

        if (Timer != null) Timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Win()
    {
        currentState = States.Win;
        Time.timeScale = 0f;
        winPanel.SetActive(IsWin);
        print("You Win!");
    }

    public void GameOver()
    {
        currentState = States.GameOver;
        Time.timeScale = 0f;
        print("Game Over!");
    }

    void OnPause()
    {
        switch (currentState)
        {
            case (States.Playing):
            {
                currentState = States.Paused;
                Time.timeScale = 0f;
                break;
            }
                
            case (States.Paused):
            {
                currentState = States.Playing;
                Time.timeScale = 1f;
                break;
            }
        }
        print(currentState.ToString());
    }
}
