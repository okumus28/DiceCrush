using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EmptyClearCell : ICellCharacteristic
{
    public FullyClearCell fullyClearCell;

    public override void CreateItem(IItem item)
    {
        Dice dice = new Dice();
        dice.value = item.value;

        SetDice(dice);

        if (item is HorizontalItem horizontalItem)
        {
            cell.AddComponent<HorizontalItem>();
        }
        else if (item is VerticalItem verticalItem)
        {
            cell.AddComponent<VerticalItem>();
        }
        else if (item is DiscoItem discoItem)
        {
            cell.AddComponent<DiscoItem>();
            cell.GetComponent<DiscoItem>().value = cell.value;
        }
        else if (item is BombItem bombItem)
        {
            cell.AddComponent<BombItem>();
        }
    }

    public override void Init(Cell cell , int value = 0)
    {
        this.cell = cell;
        cell.value = 0;

        //cell.spriteRenderer.color = Color.red;
        cell.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.red;
        cell.OnClearCountEventCall(0);
    }

    public override void RemoveDice()
    {
        return;
    }

    public override void SetDice(Dice dice)
    {
        cell.SetDice(dice);

        cell.handler = fullyClearCell;
        fullyClearCell.cell = cell;

        cell.OnClearCountEventCall(1);
    }
}
