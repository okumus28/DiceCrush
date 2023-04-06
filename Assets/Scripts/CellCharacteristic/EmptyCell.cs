using Enums;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EmptyCell : ICellCharacteristic
{
    public FullyCell fullyCell;


    public override void Init(Cell cell , int value = 0)
    {
        this.cell = cell;
        cell.value = 0;
    }
    public override void RemoveDice()
    {
        return;
    }
    public override void SetDice(Dice dice)
    {
        //Debug.Log(cell);

        cell.SetDice(dice);

        cell.handler = fullyCell;
        fullyCell.cell = cell;

    }
    public override void CreateItem(IItem item)
    {
        Dice dice = new Dice();
        dice.value = item.value;

        //Debug.Log(item.cellValue);

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
            cell.GetComponent<DiscoItem>().cellValue = discoItem.cellValue;
        }
        else if (item is BombItem bombItem)
        {
            cell.AddComponent<BombItem>();
        }

    }

}
