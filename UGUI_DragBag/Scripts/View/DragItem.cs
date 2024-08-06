using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    public Grid grid;
    public bool isDragging = false;
    public ItemUI dragedItem;
    public Transform DragCanvas;
    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void Start()
    {
        DragCanvas = BagController.Instance.DragCanvas;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            DragMannger.Instance.DragBegin(this);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(isDragging)
        {
            DragMannger.Instance.DragEnd(this);
        }
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if(isDragging)
        {
            DragMannger.Instance.OnDrag(this);
        }
    }

    public bool TryGetComponentInChildren<T>(out T item)
    {
        item = GetComponentInChildren<T>();
        if(item != null)
        {
            return true;
        }
        return false;
    }
}
