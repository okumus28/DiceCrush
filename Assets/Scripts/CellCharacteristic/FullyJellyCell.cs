using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullyJellyCell : ICellCharacteristic
{
    public EmptyCell emptyCell;

    public override void CreateItem(IItem item)
    {
        throw new System.NotImplementedException();
    }

    public override void Init(Cell cell, int value = 0)
    {
        this.cell = cell;
        cell.SetValue(value == 0 ? Random.Range(1, 6) : value);

        //cell.SetValue(Random.Range(1, 6));
        cell.spriteRenderer.sprite = GameManager.Instance.diceSprites[cell.value];
        cell.transform.GetChild(0).gameObject.SetActive(true);
        cell.isFull = true;

        cell.OnJellyCountEventCall(1);
    }

    public override void RemoveDice()
    {
        cell.RemoveDice();

        cell.transform.GetChild(0).gameObject.SetActive(false);
        cell.handler = emptyCell;
        emptyCell.cell = cell;

        cell.OnJellyCountEventCall(-1);
        cell.OnRemoveDiceEventCall(50);
    }

    public override void SetDice(Dice dice)
    {
        throw new System.NotImplementedException();
    }
}
