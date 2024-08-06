using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagController : SingletonMono<BagController>
{
    private BagView bagView;
    public Transform DragCanvas;
    protected override void Awake()
    {
        base.Awake();
        bagView = GetComponent<BagView>();
        ItemModel.Instance.action += bagView.UpdateAllSlot;
    }
    private void Start()
    {
        ItemModel.Instance.Reset(bagView.gridNum);
        bagView.UpdateAllSlot();
        ItemModel.Instance.AddItem(new TestItem());
    }
}
