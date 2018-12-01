using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Title, Intro, Main, Win, Lose
}

public enum MiniGame
{
    StompBug = 0, SmashBug, PaintBug, Piano
}


public class MainGameManager : SingletonBehaviour<MainGameManager> {
    
    public GameState gameState = GameState.Title;
    public MiniGameManagerBase currentGame;

    private int currentLevel;
    private const int miniGameNum = 4;
    private int[] miniLevels;

	private void Start()
	{
        miniLevels = new int[miniGameNum];
        System.Array.Clear(miniLevels, 0, miniLevels.Length);
        currentLevel = 0;

        SceneManager.LoadScene("TitleScene", LoadSceneMode.Additive);
	}

	private void Update()
	{
        if (gameState == GameState.Title)
        {
            HandleInput_Title();
        }

        if (gameState == GameState.Main)
        {
            HandleInput_MiniGame();
        }
	}



    private void GameStart()
    {
        if (gameState == GameState.Main)
            return;
        
        gameState = GameState.Main;
        PlayMiniGame();
    }

    private void GameLost()
    {
        SceneManager.LoadScene("GameLost", LoadSceneMode.Additive);
    }

    /// <summary>
    /// play random mini game
    /// </summary>
    private void PlayMiniGame()
    {
        int miniGameIndex = Random.Range(0, 3);
        if (currentLevel < 6)
            miniGameIndex = Random.Range(0, 2);
        miniLevels[miniGameIndex]++;
        currentLevel++;
        switch(miniGameIndex)
        {
            case 0:
                SceneManager.LoadScene("StompBug", LoadSceneMode.Additive);
                break;
            case 1:
                SceneManager.LoadScene("SmashBug", LoadSceneMode.Additive);
                break;
            case 2:
                SceneManager.LoadScene("PaintBug", LoadSceneMode.Additive);
                break;
        }
    }

    private void UnloadMiniGame(MiniGame game)
    {
        string sceneName = "";
        switch (game)
        {
            case MiniGame.StompBug:
                sceneName = "StompBug";
                break;
            case MiniGame.SmashBug:
                sceneName = "SmashBug";
                break;
            case MiniGame.PaintBug:
                sceneName = "PaintBug";
                break;
            case MiniGame.Piano:
                sceneName = "Pinao";
                break;
        }

        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void FinishMiniGame(MiniGame game, bool win)
    {
        UnloadMiniGame(game);

        if (win)
            StartCoroutine(PrepareForNextMiniGame());
        else StartCoroutine(LoadLost());
    }

    IEnumerator PrepareForNextMiniGame()
    {
        yield return new WaitForSeconds(1.0f);

        PlayMiniGame();
    }

    IEnumerator LoadLost()
    {
        yield return new WaitForSeconds(1.0f);

        GameLost();
    }




    private void HandleInput_Title()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameStart();
        }
    }

    private void HandleInput_MiniGame()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Q))
        {
            FinishMiniGame("StompBug");
        }
        */
    }


    public float GetMiniGameTime(int miniGameLevel)
    {
        if (miniGameLevel < 5)
            return 5;
        else if (miniGameLevel < 10)
            return 4.5f;
        else if (miniGameLevel < 15)
            return 4;
        else if (miniGameLevel < 20)
            return 3.5f;
        else if (miniGameLevel < 25)
            return 3;
        else if (miniGameLevel < 30)
            return 2.5f;
        else
            return 2;
    }



    public int GetCurrentLevel()
    {
        return currentLevel;
    }



    public int GetMiniGameLevel(MiniGame game)
    {
        int index = (int)game;
        return miniLevels[index];
    }
}
