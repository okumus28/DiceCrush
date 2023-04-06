using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullyCell : ICellCharacteristic
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
        cell.isFull = true;
    }
    public override void RemoveDice()
    {
        cell.RemoveDice();

        cell.handler = emptyCell;
        emptyCell.cell = cell;

        cell.OnRemoveDiceEventCall(30);
    }
    public override void SetDice(Dice dice)
    {
        if (!cell.isFull)
        {
            DiscoItem discoItem = cell.GetComponent<DiscoItem>();

            discoItem.cellValue = dice.value;
            discoItem.UseItem();
        }
    }
}
