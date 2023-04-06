using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICellCharacteristic : MonoBehaviour
{
    public Cell cell;
    public abstract void Init(Cell cell , int value = 0);
    public abstract void SetDice(Dice dice);
    public abstract void RemoveDice();

    public abstract void CreateItem(IItem item);
}
