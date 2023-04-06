using Enums;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour 
{
    public int x; // hücrenin x pozisyonu
    public int y; // hücrenin y pozisyonu
    public bool isFull; // hücrede zar olup olmadığını belirtir
    public int value;
    public Dice dice; // hücredeki zarın referansı
    public CellCharacteristic characteristic;
    public int jelly;
    public bool clear;

    public static event Action<int> OnJellyCount = delegate { };
    public static event Action<int> OnClearCount = delegate { };
    public static event Action<int> OnObstacleCount = delegate { };
    public static event Action<int> OnRemoveDice = delegate { };

    public SpriteRenderer spriteRenderer;

    public ICellCharacteristic handler;

    Cell[,] grid;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        grid = Board.Instance.grid;
    }    

    public Cell Init(int x, int y, CellCharacteristic cellCharacteristic , int value = 0)
    {
        gameObject.name = x + "," + y;
        this.value = value;
        this.x = x;
        this.y = y;
        this.characteristic = cellCharacteristic;

        ProcessCharacteristic(cellCharacteristic);

        if (handler != null)
        {
            handler.Init(this , value);
        }
        return this;

    }

    private void ProcessCharacteristic(CellCharacteristic cellCharacteristic)
    {
        switch (cellCharacteristic)
        {
            case CellCharacteristic.Space:
                Destroy(gameObject);
                break;
            case CellCharacteristic.Empty:
                handler = GetComponentInChildren<EmptyCell>();
                break;
            case CellCharacteristic.Fully:
                handler = GetComponentInChildren<FullyCell>();
                break;
            case CellCharacteristic.E_Jelly:
                handler = GetComponentInChildren<EmptyJellyCell>();
                break;
            case CellCharacteristic.F_Jelly:
                handler = GetComponentInChildren<FullyJellyCell>();
                break;
            case CellCharacteristic.E_Clear:
                handler = GetComponentInChildren<EmptyClearCell>();
                break;
            case CellCharacteristic.F_Clear:
                handler = GetComponentInChildren<FullyClearCell>();
                break;
            case CellCharacteristic.EJ_Clear:
                handler = GetComponentInChildren<EmptyJellyClearCell>();
                break;
            case CellCharacteristic.FJ_Clear:
                handler = GetComponentInChildren<FullyJellyClearCell>();
                break;
            case CellCharacteristic.Obstacle:
                handler = GetComponentInChildren<ObstacleCell>();
                break;
            case CellCharacteristic.J_Obstacle:
                handler = GetComponentInChildren<JellyObstacleCell>();
                break;
            case CellCharacteristic.C_Obstacle:
                handler = GetComponentInChildren<ClearObstacleCell>();
                break;
            case CellCharacteristic.JC_Obstacle:
                handler = GetComponentInChildren<JellyClearObstacleCell>();
                break;
            default:
                break;
        }

    }

    public void SetDice(Dice dice)
    {
        this.dice = dice;
        SetValue(dice.GetValue());
        spriteRenderer.sprite = GameManager.Instance.diceSprites[value];

        isFull = true;
    }

    public void RemoveDice()
    {
        dice = null;
        isFull = false;
        SetValue(0);
        GetComponent<SpriteRenderer>().sprite = GameManager.Instance.diceSprites[value];

        ObstacleControl(this);
    }

    public void SetValue(int value)
    {
        this.value = value;
    }

    #region public event call
    public void OnRemoveDiceEventCall(int point)
    {
        OnRemoveDice?.Invoke(point);
    }
    public void OnJellyCountEventCall(int count)
    {
        OnJellyCount?.Invoke(count);
    }
    public void OnClearCountEventCall(int count)
    {
        OnClearCount?.Invoke(count);
    }
    public void OnObstacleCountEventCall(int count)
    {
        OnObstacleCount?.Invoke(count);
    }
    #endregion


    #region matching processes 
    public void MatchControl(int controlValue)
    {
        List<Cell> horizontalList = new()
        {
            this
        };
        
        List<Cell> verticalList = new()
        {
            this
        };

        HorizontalMatchControl(horizontalList , controlValue);
        VerticalMatchControl(verticalList, controlValue);

        //bomb control
        if (horizontalList.Count >= 3 && horizontalList.Count < 5 && verticalList.Count >= 3 && verticalList.Count < 5)
        {
            CreateBombItem(verticalList , horizontalList);
            return;
        }

        //horizontal control
        if (horizontalList.Count == 5)
        {
            CreateDiscoItem(horizontalList , horizontalList[1].value);
        }

        else if (horizontalList.Count == 4)
        {
            CreateVerticalItem(horizontalList);
        }
        else if(horizontalList.Count == 3)
        {
            RemoveList(horizontalList);
        }
        
        //vertical control
        if (verticalList.Count == 5)
        {
            CreateDiscoItem(verticalList , verticalList[1].value);
        }

        else if (verticalList.Count == 4)
        {
            CreateHorizontalItem(verticalList);
        }
        else if (verticalList.Count == 3)
        {
            RemoveList(verticalList);
        }
    }

    private void HorizontalMatchControl(List<Cell> _matchList , int controlValue)
    {
        //Cell[,] grid = Board.Instance.grid;

        //right Horizontal
        for (int i = x + 1; i < x + 4 && i < 9; i++)
        {
            if (grid[i, y] == null || grid[i, y].value > 7 && grid[i, y].value == 0)
            {
                break;
            }
            if (controlValue == grid[i, y].value)
            {
                _matchList.Add(grid[i, y]);
            }
            else break;
        }

        //Left horizontal
        for (int i = x - 1; i > x - 4 && i >= 0; i--)
        {
            if (grid[i, y] == null || grid[i, y].value > 7 && grid[i, y].value == 0)
            {
                break;
            }
            if (controlValue == grid[i , y].value)
            {
                _matchList.Add(grid[i, y]);
            }
            else break;
        }
    }    
    
    private void VerticalMatchControl(List<Cell> _matchList , int controlValue)
    {
        //Cell[,] grid = Board.Instance.grid;

        //top vertical
        for (int i = y + 1; i < y + 4 && i < 9; i++)
        {
            if (grid[x, i] == null || grid[x, i].value > 7 && grid[x, i].value == 0)
            {
                break;
            }
            if (controlValue == grid[x, i].value)
            {
                _matchList.Add(grid[x, i]);
            }
            else break;
        }
        //bottom
        for (int i = y - 1; i > y - 4 && i >= 0; i--)
        {
            if (grid[x, i] == null || grid[x, i].value > 7 && grid[x, i].value == 0)
            {
                break;
            }
            if (controlValue == Board.Instance.grid[x , i].value)
            {
                _matchList.Add(Board.Instance.grid[x, i]);
            }
            else break;
        }
    }
    #endregion

    public void ObstacleControl(Cell cell)
    {        

        if (x + 1 < 9 && grid[x + 1, y].value == 21)
        {
            grid[x + 1, y].handler.RemoveDice();
        }
        
        if (x - 1 > 0 && grid[x - 1, y].value == 21)
        {
            grid[x - 1, y].handler.RemoveDice();
        }

        if (y+1 < 9 && grid[x, y + 1].value == 21)
        {
            grid[x, y + 1].handler.RemoveDice();
        }

        if (y - 1 > 0 && grid[x, y - 1].value == 21)
        {
            grid[x , y - 1].handler.RemoveDice();
        }
    }

    #region item created func
    private void CreateHorizontalItem(List<Cell> list)
    {
        RemoveList(list);

        IItem item = new HorizontalItem(9);
        handler.CreateItem(item);

        for (int i = 1; i < list.Count; i++)
        {
            list[i].RemoveDice();
            Destroy(list[i].GetComponent<IItem>() ?? null);
        }
        
        

        list.Clear();
    }
    
    private void CreateVerticalItem(List<Cell> list)
    {
        RemoveList(list);

        IItem item = new VerticalItem(8);
        handler.CreateItem(item);

        for (int i = 1; i < list.Count; i++)
        {
            list[i].RemoveDice();
            Destroy(list[i].GetComponent<IItem>() ?? null);
        }
        list.Clear();

    }
    
    private void CreateDiscoItem(List<Cell> list , int cellValue)
    {
        RemoveList(list);

        IItem item = new DiscoItem(11, cellValue);
        handler.CreateItem(item);

        this.isFull = false;

        list.Clear();
    }

    private void CreateBombItem(List<Cell> verList , List<Cell> horList)
    {
        RemoveList(verList);
        RemoveList(horList);

        IItem item = new BombItem(10);
        handler.CreateItem(item);

        verList.Clear();
        horList.Clear();
    }

    private void RemoveList(List<Cell> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].handler.RemoveDice();
        }
    }
    #endregion
}
