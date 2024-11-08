﻿using UnityEngine;
using System.Collections.Generic;

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

    [SerializeField] private List<Sprite> playerIcon = new List<Sprite>();
    public List<Item> GenelList { get { return genelList; } }
    public List<Item> WallList { get { return wallList; } }
    public List<Item> BoxList { get { return boxList; } }
    public List<Item> TrapList { get { return trapList; } }
    public List<Item> EnemyList { get { return enemyList; } }
    public List<Item> BossEnemyList { get { return bossEnemyList; } }
    public List<Item> GateList { get { return gateList; } }
    public List<Sprite> PlayerIcon { get { return playerIcon; } }

    public int LearnOrder(Item trap)
    {
        if (trap.MyBoardType == BoardType.Wall)
        {
            for (int e = 0; e < wallList.Count; e++)
            {
                if (trap == wallList[e])
                {
                    return e;
                }
            }
        }
        else if (trap.MyBoardType == BoardType.Box)
        {
            for (int e = 0; e < boxList.Count; e++)
            {
                if (trap == boxList[e])
                {
                    return e;
                }
            }
        }
        else if (trap.MyBoardType == BoardType.Trap)
        {
            for (int e = 0; e < trapList.Count; e++)
            {
                if (trap == trapList[e])
                {
                    return e;
                }
            }
        }
        else if (trap.MyBoardType == BoardType.Enemy)
        {
            for (int e = 0; e < enemyList.Count; e++)
            {
                if (trap == enemyList[e])
                {
                    return e;
                }
            }
        }
        else if (trap.MyBoardType == BoardType.BossEnemy)
        {
            for (int e = 0; e < bossEnemyList.Count; e++)
            {
                if (trap == bossEnemyList[e])
                {
                    return e;
                }
            }
        }
        else if (trap.MyBoardType == BoardType.Gate)
        {
            for (int e = 0; e < gateList.Count; e++)
            {
                if (trap == gateList[e])
                {
                    return e;
                }
            }
        }
        return -1;
    }
}