using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyJellyClearCell : ICellCharacteristic
{
    public FullyJellyClearCell fullyJellyClearCell;
    public EmptyClearCell emptyClearCell;

    public override void CreateItem(IItem item)
    {
        throw new System.NotImplementedException();
    }

    public override void Init(Cell cell, int value = 0)
    {
        this.cell = cell;
        cell.value = 0;

        //cell.spriteRenderer.color = Color.red;
        cell.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.red;
        cell.transform.GetChild(0).gameObject.SetActive(true);

        cell.OnJellyCountEventCall(1);
        cell.OnClearCountEventCall(0);
    }

    public override void RemoveDice()
    {
        cell.handler = emptyClearCell;
        emptyClearCell.cell = cell;

        cell.transform.GetChild(0).gameObject.SetActive(false);

        //cell.OnClearCountEventCall(-1);
        cell.OnJellyCountEventCall(-1);
        cell.OnRemoveDiceEventCall(50);
    }

    public override void SetDice(Dice dice)
    {
        cell.SetDice(dice);

        cell.handler = fullyJellyClearCell;
        fullyJellyClearCell.cell = cell;
        cell.OnClearCountEventCall(1);
    }
}
