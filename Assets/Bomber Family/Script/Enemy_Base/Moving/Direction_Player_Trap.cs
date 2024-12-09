using UnityEngine;
using System.Collections.Generic;

public class Direction_Player_Trap : Moving_Base
{
    // Playerın 5*5 birimlik yakınındaki rastgele bir noktaya gitmeye çalışır. Bulamazsa rastgele dolaşır.
    private bool findPlayer;
    private Vector3 nextPoint;
    private float randomDirectionTimeNext;
    private List<Node> findPath = new List<Node>();

    public override void OnStart()
    {
        if (Game_Manager.Instance.GameType == GameType.Game)
        {
            TryToFindPlayer();
        }
    }
    [ContextMenu("Find Player")]
    private void TryToFindPlayer()
    {
        findPlayer = false;
        MyBase.SetIntPos();
        FindNodeForTrapping();
        if (findPath.Count == 0)
        {
            MyBase.SetDirection(FindRandomDirection());
        }
        else
        {
            MyBase.ResetSpeed();
            findPlayer = true;
            nextPoint = new Vector3(findPath[0].X, 0, findPath[0].Z);
            MyBase.SetDirection(LearnDirectionToNextPoint(nextPoint));
        }
    }
    public override void Move()
    {
        if (Vector3.SqrMagnitude(transform.position + MyBase.Direction - Player.position) < 0.01f)
        {
            MyBase.StopMovingForXTime();
            return;
        }
        RaycastHit raycast;
        Ray ray = new(transform.position + Vector3.up * 0.5f, MyBase.Direction);
        if (Physics.Raycast(ray, out raycast, 1, BoardMaskIndex))
        {
            if (Vector3.SqrMagnitude(transform.position + MyBase.Direction - raycast.transform.position) < 0.01f)
            {
                TryToFindPlayer();
            }
        }
        else
        {
            if (findPlayer)
            {
                if (Vector3.SqrMagnitude(transform.position - nextPoint) < 0.01f)
                {
                    TryToFindPlayer();
                }
            }
            else
            {
                randomDirectionTimeNext += Time.deltaTime;
                if (randomDirectionTimeNext > ChangeDirectionTime)
                {
                    if (Vector3.SqrMagnitude(transform.position - MyBase.LearnIntDirection(transform.position)) < 0.01f)
                    {
                        randomDirectionTimeNext = 0;
                        TryToFindPlayer();
                    }
                }
            }
        }
    }
    private Vector3Int LearnDirectionToNextPoint(Vector3 vector)
    {
        Vector3Int direction = Vector3Int.zero;
        if (vector.x > transform.position.x)
        {
            return Vector3Int.right;
        }
        if (vector.z > transform.position.z)
        {
            return Vector3Int.forward;
        }
        if (vector.x < transform.position.x)
        {
            return Vector3Int.left;
        }
        if (vector.z < transform.position.z)
        {
            return Vector3Int.back;
        }
        return direction;
    }
    private void FindNodeForTrapping()
    {
        findPath.Clear();
        bool finded = false;
        Node startNode = new Node(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        List<Node> trapNodes = new List<Node>();
        for (int x = -2; x < 3; x++)
        {
            for (int z = -2; z < 3; z++)
            {
                Vector3Int playerPos = MyBase.LearnIntDirection(Player.position + new Vector3(x, 0,z));
                if (playerPos.x < 0 || playerPos.x >= Map_Holder.Instance.GameBoard.GetLength(0))
                {
                    continue;
                }
                if (playerPos.z < 0 || playerPos.z >= Map_Holder.Instance.GameBoard.GetLength(1))
                {
                    continue;
                }
                if (Map_Holder.Instance.GameBoard[playerPos.x, playerPos.z].board_Game.boardType == BoardType.Wall ||
                    Map_Holder.Instance.GameBoard[playerPos.x, playerPos.z].board_Game.boardType == BoardType.Trap ||
                    Map_Holder.Instance.GameBoard[playerPos.x, playerPos.z].board_Game.boardType == BoardType.Box)
                {
                    continue;
                }
                finded = true;
                trapNodes.Add(new Node(playerPos.x, playerPos.z));
            }
        }
        if (finded)
        {
            bool findWay = false;
            while (!findWay)
            {
                int rndNode = Random.Range(0, trapNodes.Count);
                findPath = Map_Holder.Instance.FindPath(startNode, trapNodes[rndNode]);
                if (findPath.Count > 0)
                {
                    findWay = true;
                }
                else
                {
                    trapNodes.RemoveAt(rndNode);
                }
            }
        }
        else
        {
            findPath = Map_Holder.Instance.FindPath(startNode, 
                new Node(Mathf.RoundToInt(Player.position.x), Mathf.RoundToInt(Player.position.z)));
        }
    }
}