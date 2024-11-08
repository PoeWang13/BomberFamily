﻿using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;

public class Board_Door : Board_Box
{
    [SerializeField] private float replicaTime = 5;
    [SerializeField] private All_Item_Holder all_Item_Holder;

    private float replicaTimeNext;
    private Transform boardBossEnemyParent;
    private List<BoardCoor> boardCoors = new List<BoardCoor>();

    private void Start()
    {
        boardBossEnemyParent = Utils.MakeChieldForGameElement("Board_Boss_Enemy");
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                // X sınırlar içinde mi
                if (MyCoor.x + x < 0 || MyCoor.x + x >= Map_Holder.Instance.GameBoard.GetLength(0))
                {
                    continue;
                }
                // Y sınırlar içinde mi
                if (MyCoor.y + y < 0 || MyCoor.y + y >= Map_Holder.Instance.GameBoard.GetLength(1))
                {
                    continue;
                }
                if (Map_Holder.Instance.GameBoard[MyCoor.x + x, MyCoor.y + y].board_Object is null)
                {
                    boardCoors.Add(new BoardCoor(MyCoor.x + x, MyCoor.y + y));
                }
            }
        }
    }
    private void Update()
    {
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        replicaTimeNext += Time.deltaTime;
        if (replicaTimeNext > replicaTime)
        {
            replicaTimeNext = 0;
            int rndDirec = Random.Range(0, boardCoors.Count);
            if (Map_Holder.Instance.GameBoard[boardCoors[rndDirec].x, boardCoors[rndDirec].y].board_Object is null)
            {
                int rndOrder = Random.Range(0, all_Item_Holder.BossEnemyList.Count);
                Enemy_Base enemy_Base = all_Item_Holder.BossEnemyList[rndOrder].MyObject.HavuzdanObjeIste(new Vector3(MyCoor.x, -5, MyCoor.y)).GetComponent<Enemy_Base>();
                enemy_Base.SetMyCoor(new Vector2Int(boardCoors[rndDirec].x, boardCoors[rndDirec].y));
                enemy_Base.GetComponent<Character_Base>().SetSpeed(0.0f);
                SendBossOutSide(enemy_Base.transform, rndDirec);
                enemy_Base.transform.SetParent(boardBossEnemyParent);
            }
        }
    }
    private void SendBossOutSide(Transform enemy_Base, int rndDirec)
    {
        // Bossu dışarı çıkar
        enemy_Base.DOMoveY(0, 1.0f).OnComplete(() =>
        {
            // Bossu aşağı yürüt
            enemy_Base.DOMove(new Vector3(boardCoors[rndDirec].x, 0, boardCoors[rndDirec].y), 1.0f).OnComplete(() =>
            {
                // Bossu serbest bırak
                enemy_Base.GetComponent<Character_Base>().ResetSpeed();
            });
        });
    }
}