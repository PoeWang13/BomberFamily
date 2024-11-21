using UnityEngine;
using System.Collections.Generic;
using static UnityEditor.Progress;

[CreateAssetMenu(menuName = "Genel/All Item Holder")]
public class All_Item_Holder : ScriptableObject
{
    [SerializeField] private List<Item> genelList = new List<Item>();
    [SerializeField] private List<Item> wallList = new List<Item>();
    [SerializeField] private List<Item> boxList = new List<Item>();
    [SerializeField] private List<Item> trapList = new List<Item>();
    [SerializeField] private List<Item> enemyList = new List<Item>();
    [SerializeField] private List<Item> bossEnemyList = new List<Item>();
    [SerializeField] private List<Item> gateList = new List<Item>();

    [SerializeField] private List<Player_Source> playerSourceList = new List<Player_Source>();
    public List<Item> GenelList { get { return genelList; } }
    public List<Item> WallList { get { return wallList; } }
    public List<Item> BoxList { get { return boxList; } }
    public List<Item> TrapList { get { return trapList; } }
    public List<Item> EnemyList { get { return enemyList; } }
    public List<Item> BossEnemyList { get { return bossEnemyList; } }
    public List<Item> GateList { get { return gateList; } }
    public List<Player_Source> PlayerSourceList { get { return playerSourceList; } }

    public int LearnPlayerOrder(Player_Source source)
    {
        for (int e = 0; e < PlayerSourceList.Count; e++)
        {
            if (source == PlayerSourceList[e])
            {
                return e;
            }
        }
        return -1;
    }
    public int LearnOrder(Item item)
    {
        if (item.MyBoardType == BoardType.Wall)
        {
            for (int e = 0; e < wallList.Count; e++)
            {
                if (item == wallList[e])
                {
                    return e;
                }
            }
        }
        else if (item.MyBoardType == BoardType.Box)
        {
            for (int e = 0; e < boxList.Count; e++)
            {
                if (item == boxList[e])
                {
                    return e;
                }
            }
        }
        else if (item.MyBoardType == BoardType.Trap)
        {
            for (int e = 0; e < trapList.Count; e++)
            {
                if (item == trapList[e])
                {
                    return e;
                }
            }
        }
        else if (item.MyBoardType == BoardType.Gate)
        {
            for (int e = 0; e < gateList.Count; e++)
            {
                if (item == gateList[e])
                {
                    return e;
                }
            }
        }
        else if (item.MyBoardType == BoardType.Enemy)
        {
            for (int e = 0; e < enemyList.Count; e++)
            {
                if (item == enemyList[e])
                {
                    return e;
                }
            }
        }
        else if (item.MyBoardType == BoardType.BossEnemy)
        {
            for (int e = 0; e < bossEnemyList.Count; e++)
            {
                if (item == bossEnemyList[e])
                {
                    return e;
                }
            }
        }
        return -1;
    }
}