using TMPro;
using UnityEngine;

public class Grid
{
    private int width;
    private int height;
    private int cellSize;
    private int[,] gridArray;
    private TextMeshPro[,] worldTexts;

    public Grid(int width, int height, int cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridArray = new int[width, height];
    }
    public Grid(int width, int height, int cellSize, Transform textParent)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridArray = new int[width, height];
        worldTexts = new TextMeshPro[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                TextMeshPro worldText = new GameObject("X : " + x + " - " + "Z : " + z, typeof(TextMeshPro)).GetComponent<TextMeshPro>();
                worldTexts[x, z] = worldText;
                worldText.fontSize = 2;
                worldText.richText = false;
                worldText.transform.SetParent(textParent, false);
                worldText.text = "X : " + x + "\n" + "Z : " + z + "\n" + gridArray[x, z];
                worldText.alignment = TextAlignmentOptions.Center;
                worldText.rectTransform.sizeDelta = new Vector2(1, 1);
                worldText.rectTransform.eulerAngles = new Vector3(90, 0, 0);
                worldText.rectTransform.anchoredPosition3D = new Vector3(x, 0.01f, z);
            }
        }
    }
    public void GetXY(Vector3 worldPos, out int x, out int z)
    {
        x = Mathf.RoundToInt(worldPos.x / cellSize);
        z = Mathf.RoundToInt(worldPos.z / cellSize);
    }
    public void ResetValue()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                gridArray[x, z] = 0;
                worldTexts[x, z].text = "X : " + x + "\n" + "Z : " + z + "\n" + gridArray[x, z];
            }
        }
    }
    public int GetWidth()
    {
        return width;
    }
    public int GetHeight()
    {
        return height;
    }
    public int GetValue(int x, int z)
    {
        if (x >= 0 && x < gridArray.GetLength(0) && z >= 0 && z < gridArray.GetLength(1))
        {
            return gridArray[x, z];
        }
        return -9999999;
    }
    public void SetGridForObstacle(Vector3 worldPos, bool isObstacle)
    {
        int x;
        int z;
        GetXY(worldPos, out x, out z);
        gridArray[x, z] = isObstacle ? 1 : 0;
    }
    //public void SetGridForObstacle(Vector3 worldPos, bool isObstacle)
    //{
    //    int x;
    //    int z;
    //    GetXY(worldPos, out x, out z);
    //    gridArray[x, z] = isObstacle ? 1 : 0;
    //    worldTexts[x, z].text = "X : " + x + "\n" + "Z : " + z + "\n" + gridArray[x, z];
    //}
    //public void IncreaseValue(Vector3 worldPos, int value)
    //{
    //    int x;
    //    int z;
    //    GetXY(worldPos, out x, out z);
    //    IncreaseValue(x, z, value);
    //}
    //public void IncreaseValue(int x, int z, int value)
    //{
    //    if (x >= 0 && x < gridArray.GetLength(0) && z >= 0 && z < gridArray.GetLength(1))
    //    {
    //        gridArray[x, z] += value;
    //        worldTexts[x, z].text = "X : " + x + "\n" + "Z : " + z + "\n" + gridArray[x, z];
    //    }
    //}
    //public void DecreaseValue(Vector3 worldPos, int value)
    //{
    //    int x;
    //    int z;
    //    GetXY(worldPos, out x, out z);
    //    DecreaseValue(x, z, value);
    //}
    //public void DecreaseValue(int x, int z, int value)
    //{
    //    if (x >= 0 && x < gridArray.GetLength(0) && z >= 0 && z < gridArray.GetLength(1))
    //    {
    //        gridArray[x, z] -= value;
    //        worldTexts[x, z].text = "X : " + x + "\n" + "Z : " + z + "\n" + gridArray[x, z];
    //    }
    //}
    //public int GetValue(Vector3 worldPos)
    //{
    //    int x;
    //    int z;
    //    GetXY(worldPos, out x, out z);
    //    return GetValue(x, z);
    //}
    //public Vector3 GetWorldPos(int x, int z)
    //{
    //    return new Vector3(x, 0, z) * cellSize;
    //}
    //public int[,] GetGridArray()
    //{
    //    return gridArray;
    //}
    //public void SetActiveWorldText(bool isActive)
    //{
    //    foreach (var text in worldTexts)
    //    {
    //        text.gameObject.SetActive(isActive);
    //    }
    //}
}