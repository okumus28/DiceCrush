using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoSingleton<LevelManager>
{
    public List<Level> levels;
    public int currentLevelIndex;

    public static event Action<int> OnMoveCount = delegate { };
    public static event Action<int> OnTargetScore = delegate { };

    private void OnEnable()
    {
        currentLevelIndex = PlayerPrefs.GetInt("loadedLevel");

        UIManager.OnNextLevelButton += NextLevel;
        UIManager.OnRetryLevelButton += () => SceneManager.LoadScene(1);
    }

    private void OnDisable()
    {
        UIManager.OnNextLevelButton -= NextLevel;
        UIManager.OnRetryLevelButton -= () => SceneManager.LoadScene(1);
    }

    private void RetryLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void NextLevel()
    {
        currentLevelIndex++;
        SceneManager.LoadScene(1);
        if (currentLevelIndex >= levels.Count)
            currentLevelIndex = 0;

        PlayerPrefs.SetInt("currentLevelIndex" , currentLevelIndex);
    }

    public Level CurrentLevel()
    {
        OnMoveCount?.Invoke(levels[currentLevelIndex].moveCount);
        OnTargetScore?.Invoke(levels[currentLevelIndex].targetScore);
        return levels[currentLevelIndex];
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(1);
        }
    }
}
