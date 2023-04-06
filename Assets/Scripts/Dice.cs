using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public int value;
    public Cell contactCell;

    void Start()
    {
        Roll();
    }

    public void Roll()
    {
        value = Random.Range(1, 7);
        GetComponent<SpriteRenderer>().sprite = GameManager.Instance.diceSprites[value];
    }

    public int GetValue()
    {
        return value;
    }

    public void SetValue(int newValue)
    {
        value = newValue;
        GetComponent<SpriteRenderer>().sprite = GameManager.Instance.diceSprites[value];
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Cell"))
        {
            contactCell = collision.GetComponent<Cell>();
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cell"))
        {
            contactCell = null;
        }
    }
}
