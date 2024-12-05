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
        Canvas_Manager.Instance.SetActiveMapProcessHolder(true);
        Player_Base.Instance.SetEffectivePlayer(false);
        Map_Holder.Instance.SetBoardSize(levelBoard.levelSize);
        Map_Holder.Instance.SetBoardGround();

        Canvas_Manager.Instance.MapProcessBase("Map");
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
        Vector3 cameraDirec = new Vector3(Map_Holder.Instance.GameBoard.GetLength(0), 0, 
            Map_Holder.Instance.GameBoard.GetLength(1)) / Map_Holder.Instance.GameBoard.GetLength(0);
        Canvas_Manager.Instance.SetActiveMapProcessHolder(true);

        // Dış çerçeveye duvarlar eklenecek
        Map_Holder.Instance.CreateOutsideWall();

        // Construct board Gameobjects
        BoardType boardType = BoardType.Empty;
        List<PoolObje> trapList = new List<PoolObje>();
        for (int x = 0; x < Map_Holder.Instance.GameBoard.GetLength(0); x++)
        {
            Camera_Manager.Instance.ShowConstructLevelBoard(cameraDirec);
            //Camera_Manager.Instance.ShowConstructLevelBoard(cameraDirec * x);
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
                PoolObje poolObject = null;
                string boardSpecial = "";
                if (Map_Holder.Instance.GameBoard[x, y].board_Game.boardType == BoardType.Wall)
                {
                    boardType = BoardType.Wall;
                    poolObject = all_Item_Holder.WallList[Map_Holder.Instance.GameBoard[x, y].board_Game.boardOrder].MyPool.HavuzdanObjeIste(new Vector3(x, 0, y));
                    poolObject.transform.SetParent(Map_Holder.Instance.BoardWallParent);
                    Map_Holder.Instance.WallObjects.Add(poolObject);
                }
                else if (Map_Holder.Instance.GameBoard[x, y].board_Game.boardType == BoardType.Box)
                {
                    boardType = BoardType.Box;
                    poolObject = all_Item_Holder.BoxList[Map_Holder.Instance.GameBoard[x, y].board_Game.boardOrder].MyPool.HavuzdanObjeIste(new Vector3(x, 0, y));
                    poolObject.transform.SetParent(Map_Holder.Instance.BoardBoxParent);
                    Map_Holder.Instance.BoxObjects.Add(poolObject);
                }
                else if (Map_Holder.Instance.GameBoard[x, y].board_Game.boardType == BoardType.Gate)
                {
                    boardType = BoardType.Gate;
                    poolObject = all_Item_Holder.GateList[Map_Holder.Instance.GameBoard[x, y].board_Game.boardOrder].MyPool.HavuzdanObjeIste(new Vector3(x, 0, y));
                    poolObject.transform.SetParent(Map_Holder.Instance.BoardGateParent);
                    Map_Holder.Instance.SetBoardGate(poolObject);
                }
                else if (Map_Holder.Instance.GameBoard[x, y].board_Game.boardType == BoardType.Enemy)
                {
                    boardType = BoardType.Enemy;
                    poolObject = all_Item_Holder.EnemyList[Map_Holder.Instance.GameBoard[x, y].board_Game.boardOrder].MyPool.HavuzdanObjeIste(new Vector3(x, 0, y));
                    poolObject.transform.SetParent(Map_Holder.Instance.BoardEnemyParent);
                    Map_Holder.Instance.EnemyObjects.Add(poolObject);
                    Map_Holder.Instance.AllEnemyObjects.Add(poolObject);
                }
                else if (Map_Holder.Instance.GameBoard[x, y].board_Game.boardType == BoardType.BossEnemy)
                {
                    boardType = BoardType.BossEnemy;
                    poolObject = all_Item_Holder.BossEnemyList[Map_Holder.Instance.GameBoard[x, y].board_Game.boardOrder].MyPool.HavuzdanObjeIste(new Vector3(x, 0, y));
                    poolObject.transform.SetParent(Map_Holder.Instance.BoardBossEnemyParent);
                    Map_Holder.Instance.BossEnemyObjects.Add(poolObject);
                    Map_Holder.Instance.AllEnemyObjects.Add(poolObject);
                }
                else if (Map_Holder.Instance.GameBoard[x, y].board_Game.boardType == BoardType.Trap)
                {
                    boardType = BoardType.Trap;
                    poolObject = all_Item_Holder.TrapList[Map_Holder.Instance.GameBoard[x, y].board_Game.boardOrder].MyPool.HavuzdanObjeIste(new Vector3(x, 0, y));
                    poolObject.transform.SetParent(Map_Holder.Instance.BoardTrapParent);
                    Map_Holder.Instance.TrapObjects.Add(poolObject);
                    if (!string.IsNullOrEmpty(Map_Holder.Instance.GameBoard[x, y].board_Game.boardSpecial))
                    {
                        // Trapin özel durumu vardır.
                        trapList.Add(poolObject);
                        boardSpecial = Map_Holder.Instance.GameBoard[x, y].board_Game.boardSpecial;
                    }
                }
                Board_Object board_Object = poolObject.GetComponent<Board_Object>();
                board_Object.SetBoardOrder(Map_Holder.Instance.GameBoard[x, y].board_Game.boardOrder);
                board_Object.SetBoardCoor(new Vector2Int(x, y));
                if (boardType == BoardType.Wall || boardType == BoardType.Box || boardType == BoardType.Gate || boardType == BoardType.Trap)
                {
                    Map_Holder.Instance.GameBoard[x, y] = new GameBoard(new Board(boardType, board_Object.MyBoardOrder, boardSpecial), poolObject.gameObject);
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
        SetTrapList(trapList);
        Canvas_Manager.Instance.SetActiveMapProcessHolder(false);
        Camera_Manager.Instance.SetCameraPos(new Vector3Int(Map_Holder.Instance.BoardSize.x, 0, Map_Holder.Instance.BoardSize.y));
        Map_Holder.Instance.SetMagicStone(magicStoneAmount);
    }
    private void SetTrapList(List<PoolObje> trapList)
    {
        for (int e = 0; e < trapList.Count; e++)
        {
            trapList[e].GetComponent<Trap_Base>().SetTrap();
        }
    }
}