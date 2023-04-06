using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class BombItem : IItem
{
    public BombItem(int value)
    {
        this.value = value;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        UseItem();
        Destroy(cell.GetComponent<BombItem>());
        OnMoveControlCall();
    }

    public override void UseItem()
    {
        for (int i = cell.x - 1; i <= cell.x + 1; i++)
        {
            for (int j = cell.y - 1; j <= cell.y + 1; j++)
            {
                if (i < 0 || j < 0 || j > 8 || i > 8)
                {
                    continue;
                }

                if (Board.Instance.grid[i, j] != null || Board.Instance.grid[i, j].value != 0)
                {
                    Board.Instance.grid[i, j].handler.RemoveDice();
                }
            }
        }
    }
}
