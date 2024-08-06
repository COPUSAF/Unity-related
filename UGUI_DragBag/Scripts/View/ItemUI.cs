using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Image image;

    private void Awake()
    {
        image = GetComponentInChildren<Image>();
    }
}
