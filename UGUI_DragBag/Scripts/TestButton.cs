using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    public void Add()
    {
        ItemModel.Instance.AddItem(new TestItem());
    }
}
