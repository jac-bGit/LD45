using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //instace
    public static GameManager Instance;

    //game
    public enum GameState { Play, Pause, RoundEnd, Menu }
    public static GameState gameState;

    public static int actualScore = 0;

    //time scale
    public static float GameTimeScale = 1;
    public static bool changeTime;

    //awake
    private void Awake()
    {
        // First we check if there are any other instances conflicting
        if (GameManager.Instance != null && GameManager.Instance != this)
        {
            // If that is the case, we destroy other instances
            Destroy(gameObject);
        }
        else
        {
            // Here we save our singleton instance
            Instance = this;
        }

        // Furthermore we make sure that we don't destroy between scenes (this is optional)
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        //set application
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 50;
        //for testing
        gameState = GameState.Play;
    }

    // Update is called once per frame
    void Update()
    {
        GameStatesBehaviour();
    }


    // Added methods \\\

    // Game restart - set every ingame data to starting position
    public void GameRestart()
    {
        actualScore = 0;
    }

    public void GameStatesBehaviour()
    {
        switch (gameState)
        {
            case GameState.Play:
                Time.timeScale = GameTimeScale;
                break;
            case GameState.Pause:
                Time.timeScale = 0;
                break;
            case GameState.RoundEnd:
                Time.timeScale = 1;
                break;
            case GameState.Menu:
                actualScore = 0;
                break;
        }
    }

    //change time gradualy
    public static IEnumerator ChangeTimeScale(float from, float to, float duration)
    {
        float timeDivision = 100;
        bool end = false;
        float division = (Mathf.Abs(from - to) / timeDivision) / duration;
        GameTimeScale = from;

        while (!end)
        {
            //substract
            if (from > to)
            {
                GameManager.GameTimeScale -= division;
                if (GameManager.GameTimeScale < to)
                {
                    GameManager.GameTimeScale = to;
                    end = true;
                }
            }
            //add
            if (from < to)
            {
                GameManager.GameTimeScale += division;
                if (GameManager.GameTimeScale > to)
                {
                    GameManager.GameTimeScale = to;
                    end = true;
                }
            }

            yield return new WaitForSeconds(1 / timeDivision);
        }
    }
}

