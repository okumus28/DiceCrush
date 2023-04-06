using System;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private TextMeshProUGUI jellyCountText;
    private TextMeshProUGUI clearCountText;
    private TextMeshProUGUI obstacleCountText;

    public TextMeshProUGUI moveCountText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI targetScoreText;

    public GameObject gameOverPanel;
    public GameObject levelCompletedPanel;
    public GameObject questPanel;
    public GameObject questUIPrefab;

    public Image scoreFillBar;

    public static event Action OnNextLevelButton = delegate { };
    public static event Action OnRetryLevelButton = delegate { };

    private GameObject jellyQuest;
    private GameObject clearQuest;
    private GameObject obstacleQuest;
    

    private void OnEnable()
    {        
        gameOverPanel.SetActive(false);
        levelCompletedPanel.SetActive(false);

        GameManager.OnMoveCountUpdate += MoveCountUpdate;
        GameManager.OnJellyCountUpdate += JellyCountUpdate;
        GameManager.OnClearCountUpdate += ClearCountUpdate;
        GameManager.OnObstacleCountUpdate += ObstacleCountUpdate;
        GameManager.OnScoreUpdate += ScoreUpdate;
        LevelManager.OnTargetScore += GetTargetScore;
        GameManager.OnGameOver += GameOverPanel;
        GameManager.OnLevelWin += LevelCompletedPanel;
    }

    private void OnDisable()
    {
        GameManager.OnMoveCountUpdate -= MoveCountUpdate;
        GameManager.OnJellyCountUpdate -= JellyCountUpdate;
        GameManager.OnClearCountUpdate -= ClearCountUpdate;
        GameManager.OnObstacleCountUpdate -= ObstacleCountUpdate;
        GameManager.OnScoreUpdate -= ScoreUpdate;
        LevelManager.OnTargetScore -= GetTargetScore;
        GameManager.OnGameOver -= GameOverPanel;
        GameManager.OnLevelWin -= LevelCompletedPanel;
    }
    private void MoveCountUpdate(int count)
    {
        moveCountText.text = count.ToString();
    }

    private void LevelCompletedPanel()
    {
        levelCompletedPanel.SetActive(true);
    }
    private void GameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    private void JellyCountUpdate(int count)
    {
        if (jellyQuest == null && count > 0)
        {
            jellyQuest = Instantiate(questUIPrefab, questPanel.transform);
            jellyCountText = jellyQuest.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }
        
        if(jellyQuest != null)
            jellyCountText.text = "Jelly : " + count.ToString();

    }

    private void ClearCountUpdate(int count)
    {
        if (clearQuest == null && count >= 0)
        {
            clearQuest = Instantiate(questUIPrefab, questPanel.transform);
            clearCountText = clearQuest.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }
        
        if(clearQuest != null)
            clearCountText.text = "Clear : " + count.ToString();
    }
    
    private void ObstacleCountUpdate(int count)
    {
        if (obstacleQuest == null && count > 0)
        {
            obstacleQuest = Instantiate(questUIPrefab, questPanel.transform);
            obstacleCountText = obstacleQuest.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }
        
        if(obstacleQuest != null)
            obstacleCountText.text = "Obstacle : " + count.ToString();
    }

    private void ScoreUpdate(int score , int targetScore)
    {
        scoreText.text = score.ToString();
        //Debug.Log(score +"  " + targetScore);
        scoreFillBar.fillAmount = (float)score / (float)targetScore;
    }

    private void GetTargetScore(int targetScore)
    {
        //targetScoreText.text = "Target : " + targetScore;
    }

    public void NextLevelButton()
    {
        OnNextLevelButton?.Invoke();
    }

    public void QuitButton()
    {
        SceneManager.LoadScene(0);
    }

    public void RetryButton()
    {
        OnRetryLevelButton?.Invoke();
    }

    public void CreateQuest()
    {
        //GameObject quest = Instantiate(questUIPrefab, questPanel.transform);
    }
}
