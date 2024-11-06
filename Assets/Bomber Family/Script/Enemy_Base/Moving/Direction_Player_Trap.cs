using UnityEngine;
using System.Collections.Generic;

public class Direction_Player_Trap : Moving_Base
{
    // Playerın 5*5 birimlik yakınındaki rastgele bir noktaya gitmeye çalışır. Bulamazsa rastgele dolaşır.
    private bool findPlayer;
    private Vector3 nextPoint;
    private float tryingToFindPlayerTimeNext;
    private List<Node> findPath = new List<Node>();

    public override void OnStart()
    {
        TryToFindPlayer();
    }
    [ContextMenu("Find Player")]
    private void TryToFindPlayer()
    {
        if (Vector3.Distance(transform.position, Player.position) > 0.05f)
        {
            findPlayer = false;
            Node startNode = new Node(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
            Deneme(startNode);
            if (findPath.Count == 0)
            {
                UnityEditor.EditorApplication.isPaused = true;
                MyBase.SetDirection(FindRandomDirection() - MyBase.LearnIntDirection(transform.position));
            }
            else
            {
                MyBase.SetSpeed(1);
                findPlayer = true;
                nextPoint = new Vector3(findPath[0].X, 0, findPath[0].Z);
                MyBase.SetDirection(MyBase.LearnIntDirection(nextPoint) - MyBase.LearnIntDirection(transform.position));
            }
        }
    }
    private void Deneme(Node startNode)
    {
        bool finded = false;
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
        Node trapNode = null;
        if (finded)
        {
            trapNode = trapNodes[Random.Range(0, trapNodes.Count)];
        }
        else
        {
            trapNode = new Node(Mathf.RoundToInt(Player.position.x), Mathf.RoundToInt(Player.position.z));
        }
        findPath = Map_Holder.Instance.FindPath(startNode, trapNode);
    }
    private void Update()
    {
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        tryingToFindPlayerTimeNext += Time.deltaTime;
        if (tryingToFindPlayerTimeNext > ChangeDirectionTime)
        {
            tryingToFindPlayerTimeNext = 0;
            TryToFindPlayer();
        }
        if (findPlayer)
        {
            if (Vector3.Distance(transform.position, Player.position) < 0.05f)
            {
                MyBase.SetSpeed(0);
            }
            else if (Vector3.Distance(transform.position, nextPoint) < 0.05f)
            {
                TryToFindPlayer();
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, MyDirections[RndDirec]) < 0.05f)
            {
                TryToFindPlayer();
            }
        }
        transform.Translate(MyBase.Direction * Time.deltaTime * MyBase.MySpeed);
    }
}