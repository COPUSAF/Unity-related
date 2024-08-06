using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BagView : MonoBehaviour
{
    public int gridNum;
    public GameObject gridPrefab;
    public Grid[] grids;
    private void Awake()
    {
        grids = new Grid[gridNum];
        GerneradGrid();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void UpdateAllSlot()
    {
        foreach (var grid in grids)
        {
            grid.UpdateImage();
        }
    }

    public void UpdateSlot(int index)
    {
        grids[index].UpdateImage();
    }

    public void GerneradGrid()
    {
        for(int i = 0; i < gridNum; i++)
        {
            GameObject obj = Instantiate(gridPrefab);
            obj.transform.SetParent(gameObject.transform, false);
            grids[i] = obj.GetComponent<Grid>();
            grids[i].id = i;
        }
    }
}
