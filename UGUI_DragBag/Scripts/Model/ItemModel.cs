using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemModel : BaseManager<ItemModel>
{
    private BaseItem[] Items;
    public UnityAction action;

    private ItemModel()
    {

    }

    public BaseItem GetItem(int index)
    {
        if (Items[index] != null)
        {
            return Items[index];
        }
        else
        {
            return null;
        }
    }

    public void AddItem(BaseItem item)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if(Items[i] == null)
            {
                Items[i] = item;
                action?.Invoke();
                return;
            }
        }
        Debug.Log("无空余格子");
    }

    public void RemoveItem(int index)
    {
        if (Items[index] != null)
        {
            Items[index] = null;
            action?.Invoke();
            return;
        }
        return;
    }

    public void SwapItem(int index, int index2)
    {
        BaseItem temp = Items[index]; 
        Items[index] = Items[index2];
        Items[index2] = temp;
        action?.Invoke();
    }

    public void Reset(int nums)
    {
        Items = new BaseItem[nums]; 
    }
}

public abstract class BaseItem
{
    public string Name;
    public string Description;
    public string SpritePath
    {
        get { return "ItemSprite/" + Name; }
    }
}

public class TestItem:BaseItem
{
    public TestItem()
    {
        this.Name = "Test用道具";
        this.Description = "这是一个测试用道具的描述";
    }
}
