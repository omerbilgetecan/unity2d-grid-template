using System.Collections.Generic;
using UnityEngine;

public class GridBuilder : MonoBehaviour
{
    public static GridBuilder Instance { get; private set; }

    [Header("Grid Ayarları")]
    [Tooltip("Her bir hücrenin boyutu")]
    [Min(0.01f)]
    [SerializeField] private float cellSize = 1f;

    [Tooltip("Grid'in satır (x) ve sütun (y) sayısı")]
    [SerializeField] private Vector2Int gridSize = new Vector2Int(5, 5);

    [Tooltip("Hücreler arasındaki boşluk")]
    [Min(0f)]
    [SerializeField] private float gridSpacing = 0.1f;

    [Space(10)]
    [Header("Konumlandırma")]
    [Tooltip("Grid'in başlayacağı dünya konumu")]
    [SerializeField] private Vector2 startPos;

    [Space(10)]
    [Header("Referanslar")]
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Transform cellParent;

    [Space(10)]
    [Header("Debug")]
    [Tooltip("Inspector'da tetiklenebilir - Play modunda çalışır")]
    [SerializeField] private bool generate;

    private List<GameObject> cellList = new List<GameObject>();

    public float CellSize => cellSize;

    private void Awake()
    {
        Instance = this;
    }
    private void OnValidate()
    {
        if (generate)
        {
            generate = false;
            BuildGrid();
        }
    }

    private void BuildGrid()
    {
        ClearList();


        cellPrefab.transform.localScale = new Vector2(cellSize, cellSize);

        Vector2 pos = startPos;
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                GameObject cellTemp = Instantiate(cellPrefab, pos, transform.rotation, cellParent);
                cellList.Add(cellTemp);
                pos.y += cellSize + gridSpacing;
            }
            pos.x += cellSize + gridSpacing;
            pos.y = startPos.y;
        }
    }

    private void ClearList()
    {
        if (cellList == null) return;
        foreach (var cell in cellList)
        {
            if (cell == null) continue;

            if (Application.isPlaying)
                Destroy(cell);
            else
                DestroyImmediate(cell);
        }
        cellList.Clear();
    }
}
