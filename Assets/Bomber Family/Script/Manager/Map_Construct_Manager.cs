using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map_Construct_Manager : Singletion<Map_Construct_Manager>
{
    [SerializeField] private All_Item_Holder all_Item_Holder;

    private int mapProcess;
    private float mapMaxProcess;

    public void ConstructMap(LevelBoard levelBoard)
    {
        Canvas_Manager.Instance.SetActiveMapProcess(true);
        Player_Base.Instance.SetEffectivePlayer(false);
        Map_Holder.Instance.SetBoardSize(levelBoard.levelSize);
        Map_Holder.Instance.SetBoardGround();
        mapProcess = 0;
        mapMaxProcess = levelBoard.levelSize.x * levelBoard.levelSize.y;

        for (int x = 0; x < levelBoard.levelSize.x; x++)
        {
            for (int y = 0; y < levelBoard.levelSize.y; y++)
            {
                Map_Holder.Instance.GameBoard[x, y].board_Game = levelBoard.levelBoard[x * levelBoard.levelSize.y + y];
            }
        }
        Canvas_Manager.Instance.SetCreatorPanel(false);
        Map_Holder.Instance.SendToPoolAllObjects();
        StartCoroutine(CreateBoard(levelBoard.magicStone));
    }
    private void MapProcess()
    {
        mapProcess++;
        Canvas_Manager.Instance.MapProcess(1.0f * mapProcess / mapMaxProcess);
    }
    IEnumerator CreateBoard(int magicStoneAmount)
    {
        // Dış çerçeveye duvarlar eklenecek
        Map_Holder.Instance.CreateOutsideWall();

        // Construct board Gameobjects
        BoardType boardType = BoardType.Empty;
        List<MapStrings> trapList = new List<MapStrings>();
        for (int x = 0; x < Map_Holder.Instance.GameBoard.GetLength(0); x++)
        {
            for (int y = 0; y < Map_Holder.Instance.GameBoard.GetLength(1); y++)
            {
                MapProcess();
                if (Map_Holder.Instance.GameBoard[x, y].board_Game.boardType == BoardType.Empty ||
                    Map_Holder.Instance.GameBoard[x, y].board_Game.boardType == BoardType.NonUseable ||
                    Map_Holder.Instance.GameBoard[x, y].board_Game.boardType == BoardType.Bomb ||
                    Map_Holder.Instance.GameBoard[x, y].board_Game.boardType == BoardType.Checked)
                {
                    continue;
                }
                boardType = BoardType.Empty;
                int boardOrder = -1;
                PoolObje poolObject = null;
                if (Map_Holder.Instance.GameBoard[x, y].board_Game.boardType == BoardType.Wall)
                {
                    boardType = BoardType.Wall;
                    poolObject = all_Item_Holder.WallList[Map_Holder.Instance.GameBoard[x, y].board_Game.boardOrder].MyObject.HavuzdanObjeIste(new Vector3(x, 0, y));
                    poolObject.transform.SetParent(Map_Holder.Instance.BoardWallParent);
                    Map_Holder.Instance.WallObjects.Add(poolObject);
                }
                else if (Map_Holder.Instance.GameBoard[x, y].board_Game.boardType == BoardType.Box)
                {
                    boardType = BoardType.Box;
                    poolObject = all_Item_Holder.BoxList[Map_Holder.Instance.GameBoard[x, y].board_Game.boardOrder].MyObject.HavuzdanObjeIste(new Vector3(x, 0, y));
                    poolObject.transform.SetParent(Map_Holder.Instance.BoardBoxParent);
                    Map_Holder.Instance.BoxObjects.Add(poolObject);
                }
                else if (Map_Holder.Instance.GameBoard[x, y].board_Game.boardType == BoardType.Gate)
                {
                    boardType = BoardType.Gate;
                    poolObject = all_Item_Holder.GateList[Map_Holder.Instance.GameBoard[x, y].board_Game.boardOrder].MyObject.HavuzdanObjeIste(new Vector3(x, 0, y));
                    poolObject.transform.SetParent(Map_Holder.Instance.BoardGateParent);
                    Map_Holder.Instance.GateObjects.Add(poolObject);
                }
                else if (Map_Holder.Instance.GameBoard[x, y].board_Game.boardType == BoardType.Enemy)
                {
                    boardType = BoardType.Enemy;
                    poolObject = all_Item_Holder.EnemyList[Map_Holder.Instance.GameBoard[x, y].board_Game.boardOrder].MyObject.HavuzdanObjeIste(new Vector3(x, 0, y));
                    poolObject.transform.SetParent(Map_Holder.Instance.BoardEnemyParent);
                    Map_Holder.Instance.EnemyObjects.Add(poolObject);
                }
                else if (Map_Holder.Instance.GameBoard[x, y].board_Game.boardType == BoardType.BossEnemy)
                {
                    boardType = BoardType.BossEnemy;
                    poolObject = all_Item_Holder.BossEnemyList[Map_Holder.Instance.GameBoard[x, y].board_Game.boardOrder].MyObject.HavuzdanObjeIste(new Vector3(x, 0, y));
                    poolObject.transform.SetParent(Map_Holder.Instance.BoardBossEnemyParent);
                    Map_Holder.Instance.BossEnemyObjects.Add(poolObject);
                }
                else if (Map_Holder.Instance.GameBoard[x, y].board_Game.boardType == BoardType.Trap)
                {
                    boardType = BoardType.Trap;
                    poolObject = all_Item_Holder.TrapList[Map_Holder.Instance.GameBoard[x, y].board_Game.boardOrder].MyObject.HavuzdanObjeIste(new Vector3(x, 0, y));
                    poolObject.transform.SetParent(Map_Holder.Instance.BoardTrapParent);
                    Map_Holder.Instance.TrapObjects.Add(poolObject);
                    if (!string.IsNullOrEmpty(Map_Holder.Instance.GameBoard[x, y].board_Game.boardSpecial))
                    {
                        // Trapin özel durumu vardır.
                        BoardContainer boardTriggerContainer = JsonUtility.FromJson<BoardContainer>(Map_Holder.Instance.GameBoard[x, y].board_Game.boardSpecial);
                        trapList.Add(new MapStrings(boardTriggerContainer.boardData, poolObject.gameObject));
                    }
                }
                Board_Object board_Object = poolObject.gameObject.GetComponent<Board_Object>();
                board_Object.SetBoardCoor(new Vector2Int(x, y));
                boardOrder = board_Object.BoardOrder;
                Map_Holder.Instance.GameBoard[x, y] = new GameBoard(boardType, boardOrder, poolObject.gameObject);
                yield return new WaitForSeconds(0.1f);
            }
        }
        SetTrapList(trapList);
        Debug.LogWarning("Player hazır.");
        Player_Base.Instance.SetMove(true);
        Game_Manager.Instance.SetLevelStats();
        Canvas_Manager.Instance.SetPlayerStats();
        Canvas_Manager.Instance.SetGamePanel(true);
        Player_Base.Instance.SetEffectivePlayer(true);
        Player_Base.Instance.SetPosition(Vector3.zero);
        Canvas_Manager.Instance.SetActiveMapProcess(false);
        Map_Holder.Instance.SetMagicStone(magicStoneAmount);
    }
    private void SetTrapList(List<MapStrings> trapList)
    {
        for (int e = 0; e < trapList.Count; e++)
        {
            if (trapList[e].boardData.trapType == TrapType.Trigged)
            {
                BoardTrigger triggerData = JsonUtility.FromJson<BoardTrigger>(trapList[e].boardData.boardString);
                for (int h = 0; h < triggerData.triggerBoardObject.myAllCoor.Count; h++)
                {
                    trapList[e].board_Object.GetComponent<Board_Trigger>()
                        .AddDiken(Map_Holder.Instance.GameBoard[triggerData.triggerBoardObject.myAllCoor[h].x,
                        triggerData.triggerBoardObject.myAllCoor[h].y].board_Object.GetComponent<Board_Diken>());
                }
            }
            else if (trapList[e].boardData.trapType == TrapType.Diken)
            {
                BoardDiken dikenData = JsonUtility.FromJson<BoardDiken>(trapList[e].boardData.boardString);
                trapList[e].board_Object.GetComponent<Board_Diken>().
                        SetDiken(dikenData.dikenBoardObject.isActivited,
                        Map_Holder.Instance.GameBoard[dikenData.dikenBoardObject.myTriggedObjectCoor.x,
                        dikenData.dikenBoardObject.myTriggedObjectCoor.y].board_Object.GetComponent<Board_Trigger>());
            }
        }
    }
}