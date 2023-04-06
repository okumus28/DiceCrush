using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HorizontalItem : IItem
{
    public HorizontalItem(int value)
    {
        this.value = value;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        UseItem();
        Destroy(cell.GetComponent<HorizontalItem>());
        OnMoveControlCall();
    }

    public override void UseItem()
    {
        for (int i = 8; i > -1; i--)
        {
            if (Board.Instance.grid[i, cell.y] != null )
            {
                //Debug.Log("Horizontal : " + Board.Instance.grid[i, cell.y]);
                Board.Instance.grid[i, cell.y].handler.RemoveDice();
            }
        }
    }
}
