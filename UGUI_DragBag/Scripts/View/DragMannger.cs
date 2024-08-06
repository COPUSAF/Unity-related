using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragMannger : SingletonMono<DragMannger>
{
    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;
    PointerEventData pointerEventData;

    public DragItem origialDragItem;
    public Transform DragCanvas;
    private void Start()
    {
        pointerEventData = new PointerEventData(eventSystem);
    }
    public void DragBegin(DragItem origialDragItem)
    {
        if (origialDragItem.grid.IsEmpty)
        {
            Debug.Log(origialDragItem.grid.id + "号格子开始拖动,但是该物品栏为空");
            origialDragItem.isDragging = false;
        }
        else
        {
            Debug.Log(origialDragItem.grid.id + "号格子开始拖动,该物品栏物品为" + origialDragItem.grid.item.Name + "\n其物品描述为：" + origialDragItem.grid.item.Description);
            origialDragItem.isDragging = true;
            origialDragItem.dragedItem = origialDragItem.grid.GetComponentInChildren<ItemUI>();
            origialDragItem.dragedItem.transform.SetParent(DragCanvas, true);
        }
    }
    public void DragEnd(DragItem origialDragItem)
    {
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);

        foreach (RaycastResult result in results)
        {
            GameObject uiObject = result.gameObject;

            if (uiObject.CompareTag("slot"))
            {
                Debug.Log("找到对应格子" + uiObject.name);
                Grid temp = uiObject.GetComponent<Grid>();
                origialDragItem.dragedItem.transform.SetParent(origialDragItem.transform, true);
                origialDragItem.dragedItem.transform.localPosition = Vector3.zero;
                ItemModel.Instance.SwapItem(origialDragItem.grid.id, temp.id);
                return;
            }
        }
        origialDragItem.dragedItem.transform.SetParent(origialDragItem.transform, true);
        origialDragItem.dragedItem.transform.localPosition = Vector3.zero;
    }

    public void OnDrag(DragItem origialDragItem)
    {
        Debug.Log(origialDragItem.grid.id + "号格子正在拖动");
        origialDragItem.dragedItem.transform.position = Input.mousePosition;
    }
}
