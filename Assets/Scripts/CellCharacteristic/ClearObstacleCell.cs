using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearObstacleCell : ICellCharacteristic
{
    public EmptyClearCell emptyClearCell;

    public override void CreateItem(IItem item)
    {
        throw new System.NotImplementedException();
    }

    public override void Init(Cell cell , int value = 0)
    {
        this.cell = cell;
        cell.SetValue(21);
        cell.spriteRenderer.sprite = GameManager.Instance.diceSprites[7];
        cell.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.red;
        cell.isFull = true;

        cell.OnObstacleCountEventCall(1);
        cell.OnClearCountEventCall(1);
    }

    public override void RemoveDice()
    {
        cell.SetValue(0);
        cell.spriteRenderer.sprite = GameManager.Instance.diceSprites[0];
        cell.isFull = false;

        cell.handler = emptyClearCell;
        emptyClearCell.cell = cell;

        cell.OnRemoveDiceEventCall(30);
        cell.OnObstacleCountEventCall(-1);
        cell.OnClearCountEventCall(-1);
    }

    public override void SetDice(Dice dice)
    {
        throw new System.NotImplementedException();
    }
}
