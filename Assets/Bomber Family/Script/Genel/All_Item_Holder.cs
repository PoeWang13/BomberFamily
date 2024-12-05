using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Genel/All Item Holder")]
public class All_Item_Holder : ScriptableObject
{
    [Header("Items")]
    [SerializeField] private List<Item> genelList = new List<Item>();

    [Header("Recipe Items")]
    [SerializeField] private List<Item_Source> malzemeList = new List<Item_Source>();
    [SerializeField] private List<Item_Tool> toolList = new List<Item_Tool>();
    [SerializeField] private List<Item> bombList = new List<Item>();

    [Header("Creator Items")]
    [SerializeField] private List<Item_Board> wallList = new List<Item_Board>();
    [SerializeField] private List<Item_Board> boxList = new List<Item_Board>();
    [SerializeField] private List<Item_Board> trapList = new List<Item_Board>();
    [SerializeField] private List<Item_Board> enemyList = new List<Item_Board>();
    [SerializeField] private List<Item_Board> bossEnemyList = new List<Item_Board>();
    [SerializeField] private List<Item_Board> gateList = new List<Item_Board>();

    // Items
    public List<Item> GenelList { get { return genelList; } }

    // Recipe Items
    public List<Item_Source> MalzemeList { get { return malzemeList; } }
    public List<Item_Tool> ToolList { get { return toolList; } }
    public List<Item> BombList { get { return bombList; } }

    // Creator Items
    public List<Item_Board> WallList { get { return wallList; } }
    public List<Item_Board> BoxList { get { return boxList; } }
    public List<Item_Board> TrapList { get { return trapList; } }
    public List<Item_Board> EnemyList { get { return enemyList; } }
    public List<Item_Board> BossEnemyList { get { return bossEnemyList; } }
    public List<Item_Board> GateList { get { return gateList; } }

    [Header("Sprite")]
    [SerializeField] private List<Sprite> genelIcons = new List<Sprite>();
    [SerializeField] private List<Sprite> biletIcons = new List<Sprite>();
    public List<Sprite> GenelIcons { get { return genelIcons; } }
    public List<Sprite> BiletIcons { get { return biletIcons; } }

    [Header("Player")]
    [SerializeField] private List<Player_Source> playerSourceList = new List<Player_Source>();
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
    public int LearnMalzemeOrder(Item_Source source)
    {
        for (int e = 0; e < malzemeList.Count; e++)
        {
            if (source == malzemeList[e])
            {
                return e;
            }
        }
        return -1;
    }
    public int LearnToolOrder(Item_Source source)
    {
        for (int e = 0; e < toolList.Count; e++)
        {
            if (source == toolList[e])
            {
                return e;
            }
        }
        return -1;
    }
    public int LearnOrder(Item_Board item)
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