using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TwinDice : MonoBehaviour
{
    public Dice firstDice;
    public Dice secondDice;

    public static event Action OnMoveControl = delegate { };

    void Start()
    {
        NewTwinDices();
    }

    public void NewTwinDices()
    {
        firstDice.SetValue(Random.Range(1,7));
        secondDice.SetValue(Random.Range(1,7));
    }

    public void DicesMoveControl()
    {
        if (firstDice.contactCell != null && secondDice.contactCell != null)
        {
            if (!firstDice.contactCell.isFull && !secondDice.contactCell.isFull)
            {
                ChangeCellDice();
                OnMoveControl?.Invoke();
                NewTwinDices();
            }
        }
    }

    public void ChangeCellDice()
    {
        firstDice.contactCell.handler.SetDice(firstDice);
        secondDice.contactCell.handler.SetDice(secondDice);


        firstDice.contactCell.MatchControl(firstDice.value);

        secondDice.contactCell.MatchControl(secondDice.value);
    }
}
