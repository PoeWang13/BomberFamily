using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Genel/Level Exp Holder")]
public class Level_Exp_Holder : ScriptableObject
{
    [SerializeField] private int playerLevelLimit = 140;
    [SerializeField] private List<int> playerLevelExpList = new List<int>();

    [ContextMenu("Set Player Level")]
    private void SetPlayerLevelExp()
    {
        playerLevelExpList.Clear();
        float level = 0;
        for (int i = 2; i < playerLevelLimit + 2; i++)
        {
            level = (i * i * i - 6 * i * i + 17 * i - 12) * 1.0f * 50 / 3;
            playerLevelExpList.Add((int)level);
        }
    }
    public int LearnMyLevelMaxExp(int myLevel)
    {
        return playerLevelExpList[myLevel];
    }
    public bool CanIncreaseMyLevel(int myLevel)
    {
        return myLevel < playerLevelExpList.Count - 1;
    }
}