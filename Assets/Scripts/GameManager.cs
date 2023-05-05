using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{

    public List<Sprite> diceSprites;
    public int diceCount;
    public int jellyCount;
    public int clearCount;
    public int obstacleCount;
    public int moveCount;
    public int MoveCount
    {
        get { return moveCount; }
        set 
        {
            moveCount = value;
            OnMoveCountUpdate?.Invoke(moveCount);
        }
    }
    public int score;
    public int targetScore;
    public int rewardCount;
    //public int RewardCount
    //{
    //    get { return rewardCount; }
    //    set 
    //    { 
    //        OnCanReward?.Invoke(rewardCount <= 2);
    //        rewardCount = value;
    //    }
    //}

    public static event Action<int> OnMoveCountUpdate = delegate { };
    public static event Action<int> OnJellyCountUpdate = delegate { };
    public static event Action<int> OnClearCountUpdate = delegate { };
    public static event Action<int> OnObstacleCountUpdate = delegate { };
    public static event Action<int , int> OnScoreUpdate = delegate { };
    public static event Action OnLevelWin = delegate { };
    public static event Action OnGameOver = delegate { };
    public static event Action<bool> OnCanReward;

    private void OnEnable()
    {
        TwinDice.OnMoveControl += MoveCountUpdate;
        Cell.OnJellyCount += JellyCountUpdate;
        Cell.OnRemoveDice += ScoreUpdate;
        Cell.OnClearCount += ClearCountUpdate;
        Cell.OnObstacleCount += ObstacleCountUpdate;
        LevelManager.OnMoveCount += GetMoveCount;
        LevelManager.OnTargetScore += (int targetScore) => this.targetScore = targetScore;
        UIManager.OnNextLevelButton += ResetLevel;
        UIManager.OnRetryLevelButton += ResetLevel;
        IItem.OnMoveControl += MoveCountUpdate;
    }

    private void OnDisable()
    {
        TwinDice.OnMoveControl -= MoveCountUpdate;
        IItem.OnMoveControl -= MoveCountUpdate;
        Cell.OnJellyCount -= JellyCountUpdate;
        Cell.OnRemoveDice -= ScoreUpdate;
        Cell.OnClearCount -= ClearCountUpdate;
        Cell.OnObstacleCount -= ObstacleCountUpdate;
        LevelManager.OnMoveCount -= GetMoveCount;
        LevelManager.OnTargetScore -= (int targetScore) => this.targetScore = targetScore;
        UIManager.OnNextLevelButton -= ResetLevel;
        UIManager.OnRetryLevelButton -= ResetLevel;
    }


    private void MoveCountUpdate()
    {
        moveCount--;
        GameOverControl();
        OnMoveCountUpdate?.Invoke(moveCount);
    }

    private void JellyCountUpdate(int count)
    {
        jellyCount += count;
        OnJellyCountUpdate?.Invoke(jellyCount);
    }
    private void ClearCountUpdate(int count)
    {
        clearCount += count;
        OnClearCountUpdate?.Invoke(clearCount);
    }
    
    private void ObstacleCountUpdate(int count)
    {
        obstacleCount += count;
        OnObstacleCountUpdate(obstacleCount);
    }

    private void ScoreUpdate(int score)
    {
        this.score += score;
        OnScoreUpdate?.Invoke(this.score , this.targetScore);
        LevelWinControl();
    }

    private void GetMoveCount(int count)
    {
        moveCount = count;
        OnMoveCountUpdate?.Invoke(moveCount);
    }

    void LevelWinControl()
    {
        if (jellyCount <= 0 && score >= targetScore && obstacleCount <= 0 && clearCount <= 0)
        {
            OnLevelWin?.Invoke();
            Debug.Log("Game Winnn !!!");
        }
    }

    private void GameOverControl()
    {
        Cell[,] grid = Board.Instance.grid;
        int count = 0;

        for (int i = 0; i < Board.Instance.transform.childCount; i++)
        {
            Cell cell = Board.Instance.transform.GetChild(i).GetComponent<Cell>();

            if (cell.isFull)
            {
                continue;
            }

            if (cell.x > 0 && grid[cell.x + 1, cell.y] != null && !grid[cell.x + 1, cell.y].isFull)
            {
                count++;
                break;
            }

            if (cell.x < 9 && grid[cell.x - 1, cell.y] != null && !grid[cell.x - 1, cell.y].isFull)
            {
                count++;
                break;
            }

            if (cell.y > 0 && grid[cell.x, cell.y + 1] != null && !grid[cell.x, cell.y + 1].isFull)
            {
                count++;
                break;
            }

            if (cell.y < 9 && grid[cell.x, cell.y - 1] != null && !grid[cell.x, cell.y - 1].isFull)
            {
                count++;
                break;
            }
        }


        if (count <= 0)
        {
            OnGameOver?.Invoke();
            Debug.Log("Boş Yer Kalmadı");
            OnCanReward?.Invoke(false);
            return;
        }

        if (moveCount <= 0)
        {
            rewardCount++;
            OnCanReward?.Invoke(rewardCount <= 2);
            OnGameOver?.Invoke();
            Debug.Log("Hamle Kalmadı !!!");
        }

    }

    private void ResetLevel()
    {
        jellyCount = 0;
        obstacleCount = 0;
        clearCount = 0;
        score = 0;
        clearCount = 0;
        OnScoreUpdate?.Invoke(0 , 0);
        OnJellyCountUpdate?.Invoke(0);
        OnObstacleCountUpdate?.Invoke(0);
    }

}
