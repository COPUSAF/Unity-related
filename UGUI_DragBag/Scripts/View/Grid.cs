using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Grid : MonoBehaviour
{
    public int id;
    public ItemUI itemUI;
    public bool IsEmpty
    {
        get
        {
            return (ItemModel.Instance.GetItem(id) == null);
        }
    }

    public BaseItem item
    {
        get
        {
            return ItemModel.Instance.GetItem(id);
        }
    }

    private void Awake()
    {
        itemUI = GetComponentInChildren<ItemUI>();
    }

    public void UpdateImage()
    {
        BaseItem tempItem = ItemModel.Instance.GetItem(id);
        if(tempItem != null)
        {
            itemUI.image.sprite = Resources.Load<Sprite>(tempItem.SpritePath);
        }
        else
        {
            itemUI.image.sprite = Resources.Load<Sprite>("empty");
        }
    }
}
