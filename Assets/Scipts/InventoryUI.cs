using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    [Header("Grid Settings")]
    public int GridSize = 8;

    [Header("References")]
    public GridLayoutGroup grid;
    public GameObject itemPrefab;

    private int currentGridSize;
    private int maxCells;
    private List<GameObject> items= new List<GameObject>();

    void Start()
    {
        currentGridSize = GridSize;
        ResizeGrid();
    }

    public void AddItem()
    {
        if (items.Count >= 8)
        {
            currentGridSize += 8;
            ResizeGrid();
        }

        GameObject item = Instantiate(itemPrefab, grid.transform);
        items.Add(item);
    }

    public void RemoveItem()
    {
        if (items.Count == 0) return;
        GameObject item = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);
        Destroy(item);
    }

    void ResizeGrid()
    {
        grid.constraintCount = 8;
        maxCells = currentGridSize;
    }
}