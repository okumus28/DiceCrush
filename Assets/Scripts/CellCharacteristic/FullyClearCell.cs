using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullyClearCell : ICellCharacteristic
{
    public EmptyClearCell emptyClearCell;

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
        //cell.spriteRenderer.color = Color.red;
        cell.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.red;
        cell.isFull = true;

        cell.OnClearCountEventCall(1);
    }

    public override void RemoveDice()
    {
        cell.RemoveDice();

        cell.handler = emptyClearCell;
        emptyClearCell.cell = cell;

        cell.OnClearCountEventCall(-1);
        cell.OnRemoveDiceEventCall(50);
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
