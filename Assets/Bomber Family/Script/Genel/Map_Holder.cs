using System;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[Serializable]
public class Board
{
    public int x;
    public int y;
    public int boardOrder = -1;
    public string boardSpecial = "";
    public BoardType boardType = BoardType.Empty;

    public Board()
    {
    }
    public Board(Board board)
    {
        this.boardOrder = board.boardOrder;
        this.boardType = board.boardType;
        this.boardOrder = board.boardOrder;
    }
    public Board(BoardType boardType, int boardOrder)
    {
        this.boardType = boardType;
        this.boardOrder = boardOrder;
    }
}
public class GameBoard
{
    public Board board_Game;
    public GameObject board_Object;
    public GameBoard()
    {
        board_Game = new Board();
        board_Game.boardType = BoardType.Empty;
        board_Game.boardOrder = -1;
        board_Object = null;
    }
    public GameBoard(Board board_Game, GameObject board_Object)
    {
        this.board_Game = new Board(board_Game);
        this.board_Object = board_Object;
    }
    public GameBoard(BoardType boardType, int boardOrder, GameObject board_Object)
    {
        board_Game = new Board();
        board_Game.boardType = boardType;
        board_Game.boardOrder = boardOrder;
        this.board_Object = board_Object;
    }
}
public class MapStrings
{
    public BoardData boardData;
    public GameObject board_Object;
    public MapStrings(BoardData boardData, GameObject board_Object)
    {
        this.boardData = boardData;
        this.board_Object = board_Object;
    }
}
public enum BoardType
{
    NonUseable = 999,
    Checked = 998,
    Empty = 0,
    Wall = 1,
    Box = 2,
    Trap = 3,
    Enemy = 4,
    BossEnemy = 5,
    Gate = 6,
    Bomb = 7,
}
public enum TrapType
{
    Trigged = 0,
    Diken = 1,
}
public class Map_Holder : Singletion<Map_Holder>
{
    public class Araba
    {
        public virtual void FiyatSorAraba()
        {
            Debug.Log("Araba : " + 5);
        }
    }
    public class Ford : Araba
    {
        public override void FiyatSorAraba()
        {
            Debug.Log("Ford Araba : " + 11);
        }
    }
    public class Honda : Araba
    {
        public override void FiyatSorAraba()
        {
            Debug.Log("Honda Araba : " + 33);
        }
    }
    [ContextMenu("Close 1")]
    private void CloseGameBoard1()
    {
        Araba araba = new Araba();
        araba.FiyatSorAraba();
        araba = new Ford();
        araba.FiyatSorAraba();
        araba = new Honda();
        araba.FiyatSorAraba();
        Ford ford = new Ford();
        ford.FiyatSorAraba();
    }
    [Header("Genel")]
    //[SerializeField] private bool openParents;
    [SerializeField] private All_Item_Holder all_Item_Holder;

    [Header("Board")]
    [SerializeField] private Pooler boardCloser;
    [SerializeField] private Transform boardGround;

    private Vector2Int boardSize;
    private GameBoard[,] gameBoard;
    private Transform boardBoxParent;
    private Transform boardWallParent;
    private Transform boardTrapParent;
    private Transform boardGateParent;
    private Transform boardEnemyParent;
    private Transform boardCloserParent;
    private Transform boardBossEnemyParent;
    private List<PoolObje> boxObjects = new List<PoolObje>();
    private List<PoolObje> lootObjects = new List<PoolObje>();
    private List<PoolObje> wallObjects = new List<PoolObje>();
    private List<PoolObje> trapObjects = new List<PoolObje>();
    private List<PoolObje> gateObjects = new List<PoolObje>();
    private List<PoolObje> enemyObjects = new List<PoolObje>();
    private List<PoolObje> closerObjects = new List<PoolObje>();
    private List<PoolObje> bossEnemyObjects = new List<PoolObje>();
    private List<PoolObje> magicStoneObjects = new List<PoolObje>();

    public GameBoard[,] GameBoard { get { return gameBoard; } }
    public Vector2Int BoardSize { get { return boardSize; } }
    public Transform BoardBoxParent { get { return boardBoxParent; } }
    public Transform BoardWallParent { get { return boardWallParent; } }
    public Transform BoardTrapParent { get { return boardTrapParent; } }
    public Transform BoardGateParent { get { return boardGateParent; } }
    public Transform BoardEnemyParent { get { return boardEnemyParent; } }
    public Transform BoardBossEnemyParent { get { return boardBossEnemyParent; } }
    public List<PoolObje> BoxObjects { get { return boxObjects; } }
    public List<PoolObje> LootObjects { get { return lootObjects; } }
    public List<PoolObje> WallObjects { get { return wallObjects; } }
    public List<PoolObje> TrapObjects { get { return trapObjects; } }
    public List<PoolObje> GateObjects { get { return gateObjects; } }
    public List<PoolObje> EnemyObjects { get { return enemyObjects; } }
    public List<PoolObje> BossEnemyObjects { get { return bossEnemyObjects; } }
    public List<PoolObje> MagicStoneObjects { get { return magicStoneObjects; } }

    private void Start()
    {
        boardBoxParent = Utils.MakeChieldForGameElement("Board_Box");
        boardWallParent = Utils.MakeChieldForGameElement("Board_Wall");
        boardTrapParent = Utils.MakeChieldForGameElement("Board_Trap");
        boardGateParent = Utils.MakeChieldForGameElement("Board_Gate");
        boardEnemyParent = Utils.MakeChieldForGameElement("Board_Enemy");
        boardCloserParent = Utils.MakeChieldForGameElement("Board_Closer");
        boardBossEnemyParent = Utils.MakeChieldForGameElement("Board_Boss_Enemy");

        //boardBoxParent.gameObject.SetActive(openParents);
        //boardWallParent.gameObject.SetActive(openParents);
        //boardTrapParent.gameObject.SetActive(openParents);
        //boardGateParent.gameObject.SetActive(openParents);
        //boardEnemyParent.gameObject.SetActive(openParents);
        //boardCloserParent.gameObject.SetActive(openParents);
        //boardBossEnemyParent.gameObject.SetActive(openParents);
    }
    public void SetBoardSize(Vector2Int boardSize)
    {
        this.boardSize = boardSize;
    }
    public void SetBoardGround()
    {
        boxObjects.Clear();
        wallObjects.Clear();
        trapObjects.Clear();
        gateObjects.Clear();
        enemyObjects.Clear();
        bossEnemyObjects.Clear();
        SendToPoolCloserObject();
        boardGround.gameObject.SetActive(true);
        boardGround.localScale = new Vector3(boardSize.x + 1, 0.2f, boardSize.y + 1);
        boardGround.position = new Vector3((boardSize.x + 1) * 0.5f - 1, -0.1f, (boardSize.y + 1) * 0.5f - 1);

        gameBoard = new GameBoard[boardSize.x, boardSize.y];

        for (int x = 0; x < boardSize.x; x++)
        {
            for (int y = 0; y < boardSize.y; y++)
            {
                gameBoard[x, y] = new GameBoard();
            }
        }
    }
    public void SendToPoolAllObjects()
    {
        for (int e = 0; e < magicStoneObjects.Count; e++)
        {
            if (magicStoneObjects[e].gameObject.activeSelf)
            {
                magicStoneObjects[e].EnterHavuz();
            }
        }
        for (int e = 0; e < lootObjects.Count; e++)
        {
            if (lootObjects[e].gameObject.activeSelf)
            {
                lootObjects[e].EnterHavuz();
            }
        }
        for (int e = 0; e < wallObjects.Count; e++)
        {
            if (wallObjects[e].gameObject.activeSelf)
            {
                wallObjects[e].EnterHavuz();
            }
        }
        for (int e = 0; e < boxObjects.Count; e++)
        {
            if (boxObjects[e].gameObject.activeSelf)
            {
                boxObjects[e].EnterHavuz();
            }
        }
        for (int e = 0; e < trapObjects.Count; e++)
        {
            if (trapObjects[e].gameObject.activeSelf)
            {
                trapObjects[e].EnterHavuz();
            }
        }
        for (int e = 0; e < gateObjects.Count; e++)
        {
            if (gateObjects[e].gameObject.activeSelf)
            {
                gateObjects[e].EnterHavuz();
            }
        }
        for (int e = 0; e < enemyObjects.Count; e++)
        {
            if (enemyObjects[e].gameObject.activeSelf)
            {
                enemyObjects[e].EnterHavuz();
            }
        }
        for (int e = 0; e < bossEnemyObjects.Count; e++)
        {
            if (bossEnemyObjects[e].gameObject.activeSelf)
            {
                bossEnemyObjects[e].EnterHavuz();
            }
        }
    }
    public void CreateOutsideWall()
    {
        for (int x = -1; x <= gameBoard.GetLength(0); x++)
        {
            for (int y = -1; y <= gameBoard.GetLength(1); y++)
            {
                if (y == -1 || y == gameBoard.GetLength(1))
                {
                    CreateWall(x, y, Map_Creater_Manager.Instance.WallOrder);
                }
                else if (x == -1 || x == gameBoard.GetLength(0))
                {
                    CreateWall(x, y, Map_Creater_Manager.Instance.WallOrder);
                }
            }
        }
    }
    public void CreateWall(int x, int y, int wallOrder)
    {
        GameObject wallOutSide = all_Item_Holder.WallList[wallOrder].MyObject.HavuzdanObjeIste(new Vector3(x, 0, y)).gameObject;
        wallOutSide.transform.SetParent(boardWallParent);
        wallOutSide.name = "WallOutSide -> X: " + x + ", Y: " + y;
    }
    public void SetBoardForNonUseable()
    {
        // Kullanılmayan alanlar belirlenecek
        //  Sol Alt
        gameBoard[0, 1].board_Game.boardType = BoardType.NonUseable;
        gameBoard[0, 0].board_Game.boardType = BoardType.NonUseable;
        gameBoard[1, 0].board_Game.boardType = BoardType.NonUseable;
        //  Sol Üst
        gameBoard[0, boardSize.y - 2].board_Game.boardType = BoardType.NonUseable;
        gameBoard[0, boardSize.y - 1].board_Game.boardType = BoardType.NonUseable;
        gameBoard[1, boardSize.y - 1].board_Game.boardType = BoardType.NonUseable;
        //  Sağ Üst
        gameBoard[boardSize.x - 2, boardSize.y - 1].board_Game.boardType = BoardType.NonUseable;
        gameBoard[boardSize.x - 1, boardSize.y - 1].board_Game.boardType = BoardType.NonUseable;
        gameBoard[boardSize.x - 1, boardSize.y - 2].board_Game.boardType = BoardType.NonUseable;
        //  Sağ Alt
        gameBoard[boardSize.x - 1, 1].board_Game.boardType = BoardType.NonUseable;
        gameBoard[boardSize.x - 1, 0].board_Game.boardType = BoardType.NonUseable;
        gameBoard[boardSize.x - 2, 0].board_Game.boardType = BoardType.NonUseable;
    }
    public void ReleaseMap()
    {
        SendToPoolAllObjects();
        SetBoardGround();
    }

    #region Close Game Board
    [ContextMenu("Close Game Board")]
    private void CloseGameBoard()
    {
        StartCoroutine(CloseBoard());
    }
    IEnumerator CloseBoard()
    {
        bool ySide = true;
        int direction = 1;
        int xPoint = -1;
        int yPoint = -1;
        int xRight = boardSize.x;
        int xLeft = -1;
        int yTop = boardSize.y;
        int yBottom = -1;
        int usedBoard = 0;
        int boardAmount = (boardSize.x + 2) * (boardSize.y + 2);
        while (usedBoard < boardAmount)
        {
            yield return new WaitForSeconds(0.25f);
            usedBoard++;
            PoolObje poolObje = boardCloser.HavuzdanObjeIste(new Vector3(xPoint, 5, yPoint));
            Transform closer = poolObje.transform;
            closerObjects.Add(poolObje);
            closer.SetParent(boardCloserParent);
            closer.name = "Closer -> X: " + xPoint + " * Y: " + yPoint;
            closer.DOLocalMoveY(0, 1);
            if (ySide)
            {
                if (direction == 1)
                {
                    if (yPoint + direction > yTop)
                    {
                        ySide = false;
                        xPoint += direction;
                        xLeft += direction;
                    }
                    else
                    {
                        yPoint += direction;
                    }
                }
                else
                {
                    if (yPoint + direction < yBottom)
                    {
                        ySide = false;
                        xPoint += direction;
                        xRight += direction;
                    }
                    else
                    {
                        yPoint += direction;
                    }
                }
            }
            else
            {
                if (direction == 1)
                {
                    if (xPoint + direction > xRight)
                    {
                        ySide = true;
                        direction = -1;
                        yPoint += direction;
                        yTop += direction;
                    }
                    else
                    {
                        xPoint += direction;
                    }
                }
                else
                {
                    if (xPoint + direction < xLeft)
                    {
                        ySide = true;
                        direction = 1;
                        yPoint += direction;
                        yBottom += direction;
                    }
                    else
                    {
                        xPoint += direction;
                    }
                }
            }
        }
    }
    private void SendToPoolCloserObject()
    {
        for (int e = 0; e < closerObjects.Count; e++)
        {
            if (closerObjects[e].gameObject.activeSelf)
            {
                closerObjects[e].EnterHavuz();
            }
        }
    }
    public void SetMagicStone(int magicStoneAmount)
    {
        List<PoolObje> magicStoneObject = new List<PoolObje>();
        while (magicStoneObject.Count < magicStoneAmount)
        {
            int rndBox = Random.Range(0, boxObjects.Count);
            PoolObje poolObje = boxObjects[rndBox];
            if (!magicStoneObject.Contains(poolObje))
            {
                magicStoneObject.Add(poolObje);
                poolObje.GetComponent<Board_Box>().SetHasMagicStone(true);
            }
        }
        Board_Gate.Instance.SetNeededMagicStone(magicStoneAmount);
    }
    #endregion

    #region Find Path
    public List<Node> FindPath(Node start, Node goal, bool controlTrap = false)
    {
        var openSet = new List<Node> { start };
        var closedSet = new HashSet<Node>();

        while (openSet.Count > 0)
        {
            openSet.Sort((nodeA, nodeB) => nodeA.FCost.CompareTo(nodeB.FCost));
            var current = openSet[0];

            if (current.Equals(goal))
            {
                var path = new List<Node>();
                while (current != start)
                {
                    path.Add(current);
                    current = current.Parent;
                }
                path.Reverse();
                return path;
            }
            openSet.Remove(current);
            closedSet.Add(current);

            foreach (var neighbor in GetNeighbors(current))
            {
                if (closedSet.Contains(neighbor) || gameBoard[neighbor.X, neighbor.Z].board_Game.boardType == BoardType.Box
                     || gameBoard[neighbor.X, neighbor.Z].board_Game.boardType == BoardType.Wall
                     || gameBoard[neighbor.X, neighbor.Z].board_Game.boardType == BoardType.Gate)
                {
                    continue;
                }
                if (controlTrap && gameBoard[neighbor.X, neighbor.Z].board_Game.boardType == BoardType.Trap)
                {
                    continue;
                }

                var tentativeGCost = current.GCost + 1;
                if (tentativeGCost < neighbor.GCost || !openSet.Contains(neighbor))
                {
                    neighbor.GCost = tentativeGCost;
                    neighbor.HCost = Heuristic(neighbor, goal);
                    neighbor.Parent = current;

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }
        Debug.LogError("Yol bulunamadı.");
        return openSet;
    }
    public int Heuristic(Node a, Node b)
    {
        return Math.Abs(a.X - b.X) + Math.Abs(a.Z - b.Z);
    }
    public List<Node> GetNeighbors(Node node)
    {
        var neighbors = new List<Node>();
        var directions = new (int, int)[] { (0, 1), (0, -1), (1, 0), (-1, 0) };

        foreach (var (dx, dy) in directions)
        {
            var x = node.X + dx;
            var z = node.Z + dy;

            if (x >= 0 && x < gameBoard.GetLength(0) && z >= 0 && z < gameBoard.GetLength(1))
            {
                neighbors.Add(new Node(x, z));
            }
        }
        return neighbors;
    }
    #endregion
}
public class Node
{
    public int X { get; }
    public int Z { get; }
    public Node Parent { get; set; }
    public int GCost { get; set; }
    public int HCost { get; set; }
    public int FCost => GCost + HCost;

    public Node(int x, int z)
    {
        X = x;
        Z = z;
    }
    public override bool Equals(object obj)
    {
        if (obj is Node node)
        {
            return X == node.X && Z == node.Z;
        }
        return false;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Z);
    }
}