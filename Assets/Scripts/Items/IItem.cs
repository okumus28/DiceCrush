using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class IItem  : MonoBehaviour , IPointerClickHandler
{
    public int value;

    public Cell cell;

    public static event Action OnMoveControl = delegate { };
    private void OnEnable()
    {
        cell = transform.GetComponent<Cell>();
    }
    public abstract void OnPointerClick(PointerEventData eventData);

    public abstract void UseItem();

    public void OnMoveControlCall()
    {
        OnMoveControl?.Invoke();
    }

}
