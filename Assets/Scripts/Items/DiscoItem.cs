using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiscoItem : IItem
{
    public int cellValue;

    public DiscoItem(int value , int cellValue)
    {
        this.value = value;
        this.cellValue = cellValue;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        return;
        //UseItem();
    }

    public override void UseItem()
    {
        cell.handler.RemoveDice();

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (Board.Instance.grid[i,j] != null && cellValue == Board.Instance.grid[i,j].value)
                {
                    Board.Instance.grid[i, j].handler.RemoveDice();
                }
            }
        }

        Destroy(cell.GetComponent<DiscoItem>());
    }
}
