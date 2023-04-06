using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VerticalItem : IItem
{

    public VerticalItem(int value)
    {
        this.value = value;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        UseItem();
        Destroy(cell.GetComponent<VerticalItem>());
        OnMoveControlCall();
    }

    public override void UseItem()
    {
        for (int i = 8; i > -1; i--)
        {
            if (Board.Instance.grid[cell.x , i] != null || Board.Instance.grid[cell.x, i].value != 0)
            {
                Board.Instance.grid[cell.x, i].handler.RemoveDice();
            }
        }
    }
}
