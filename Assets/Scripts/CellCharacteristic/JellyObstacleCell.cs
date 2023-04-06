using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyObstacleCell : ICellCharacteristic
{
    public EmptyJellyCell emptyJellyCell;

    public override void CreateItem(IItem item)
    {
        throw new System.NotImplementedException();
    }

    public override void Init(Cell cell, int value = 0)
    {
        this.cell = cell;
        cell.SetValue(21);
        cell.spriteRenderer.sprite = GameManager.Instance.diceSprites[7];
        cell.transform.GetChild(0).gameObject.SetActive(true);
        cell.isFull = true;

        cell.OnJellyCountEventCall(1);
        cell.OnObstacleCountEventCall(1);
    }

    public override void RemoveDice()
    {
        cell.SetValue(0);
        cell.spriteRenderer.sprite = GameManager.Instance.diceSprites[0];
        cell.isFull = false;

        cell.handler = emptyJellyCell;
        emptyJellyCell.cell = cell;

        cell.OnRemoveDiceEventCall(30);
        cell.OnObstacleCountEventCall(-1);

        Debug.Log(cell + " : " + cell.handler);
    }

    public override void SetDice(Dice dice)
    {
        throw new System.NotImplementedException();
    }
}
