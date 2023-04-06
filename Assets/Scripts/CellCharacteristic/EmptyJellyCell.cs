using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Level;

public class EmptyJellyCell : ICellCharacteristic
{
    public FullyJellyCell fullyJellyCell;
    public EmptyCell emptyCell;

    public override void CreateItem(IItem item)
    {
        throw new System.NotImplementedException();
    }

    public override void Init(Cell cell, int value = 0)
    {
        this.cell = cell;
        cell.value = 0;

        cell.transform.GetChild(0).gameObject.SetActive(true);
        cell.OnJellyCountEventCall(1);
    }

    public override void RemoveDice()
    {
        cell.transform.GetChild(0).gameObject.SetActive(false);
        cell.handler = emptyCell;
        emptyCell.cell = cell;

        cell.OnJellyCountEventCall(-1);
        cell.OnRemoveDiceEventCall(50);

        Debug.Log(cell + " : " + cell.handler);
    }

    public override void SetDice(Dice dice)
    {
        cell.SetDice(dice);

        cell.handler = fullyJellyCell;
        fullyJellyCell.cell = cell;
    }
}
