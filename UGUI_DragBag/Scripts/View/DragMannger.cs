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
            Debug.Log(origialDragItem.grid.id + "�Ÿ��ӿ�ʼ�϶�,���Ǹ���Ʒ��Ϊ��");
            origialDragItem.isDragging = false;
        }
        else
        {
            Debug.Log(origialDragItem.grid.id + "�Ÿ��ӿ�ʼ�϶�,����Ʒ����ƷΪ" + origialDragItem.grid.item.Name + "\n����Ʒ����Ϊ��" + origialDragItem.grid.item.Description);
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
                Debug.Log("�ҵ���Ӧ����" + uiObject.name);
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
        Debug.Log(origialDragItem.grid.id + "�Ÿ��������϶�");
        origialDragItem.dragedItem.transform.position = Input.mousePosition;
    }
}
