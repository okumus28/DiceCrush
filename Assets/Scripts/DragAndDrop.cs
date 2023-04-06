using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler , IPointerClickHandler
{
    private Vector3 offset;
    private Vector3 startPosition;
    private Vector3 startRotate;
    private bool isDragging = false;
    TwinDice td;

    private void Awake()
    {
        td = GetComponent<TwinDice>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(eventData.position);
        startPosition = transform.position;
        startRotate = transform.rotation.eulerAngles;
        transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
        transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = true;
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Camera.main.ScreenToWorldPoint(eventData.position) + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        td.DicesMoveControl();
        ResetPosition();
        transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
        transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Rotate();
    }

    public bool IsDragging()
    {
        return isDragging;
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        transform.eulerAngles = startRotate;
    }


    public void Rotate()
    {
        transform.eulerAngles += new Vector3(0, 0, -90);
        transform.GetChild(0).eulerAngles += new Vector3(0, 0, 90);
        transform.GetChild(1).eulerAngles += new Vector3(0, 0, 90);
    }

}
