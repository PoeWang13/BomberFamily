using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class Board_Replicater : Board_Box
{
    [SerializeField] private float replicaTime = 5;

    private float replicaTimeNext;
    private bool created = false;
    private Transform boardBoxParent;

    public override void OnStart()
    {
        boardBoxParent = Utils.MakeChieldForGameElement("Board_Box");
        this.enabled = false;
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
            List<int> rndDirection = new List<int>() { 0, 1, 2, 3};
            created = false;
            while (!created && rndDirection.Count > 0)
            {
                int rndDir = Random.Range(0, rndDirection.Count);
                // X sınırlar içinde mi
                if (rndDirection[rndDir] == 0)
                {
                    if (MyCoor.x + 1 < Map_Holder.Instance.GameBoard.GetLength(0))
                    {
                        if (Map_Holder.Instance.GameBoard[MyCoor.x + 1, MyCoor.y].board_Object is null)
                        {
                            created = true;
                            CreateReplicator(MyCoor.x + 1, MyCoor.y);
                        }
                    }
                }
                else if (rndDirection[rndDir] == 1)
                {
                    // X sınırlar içinde mi
                    if (MyCoor.x - 1 >= 0)
                    {
                        if (Map_Holder.Instance.GameBoard[MyCoor.x - 1, MyCoor.y].board_Object is null)
                        {
                            created = true;
                            CreateReplicator(MyCoor.x - 1, MyCoor.y);
                        }
                    }
                }
                else if (rndDirection[rndDir] == 2)
                {
                    // Y sınırlar içinde mi
                    if (MyCoor.y + 1 >= 0)
                    {
                        if (Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y + 1].board_Object is null)
                        {
                            created = true;
                            CreateReplicator(MyCoor.x, MyCoor.y + 1);
                        }
                    }
                }
                else if (rndDirection[rndDir] == 3)
                {
                    // Y sınırlar içinde mi
                    if (MyCoor.y - 1 < Map_Holder.Instance.GameBoard.GetLength(1))
                    {
                        if (Map_Holder.Instance.GameBoard[MyCoor.x, MyCoor.y - 1].board_Object is null)
                        {
                            created = true;
                            CreateReplicator(MyCoor.x, MyCoor.y - 1);
                        }
                    }
                }
                rndDirection.Remove(rndDirection[rndDir]);
            }
        }
    }
    private void CreateReplicator(int x, int y)
    {
        Board_Object board_Object = havuzum.HavuzdanObjeIste(new Vector3(MyCoor.x, 0, MyCoor.y)).GetComponent<Board_Object>();
        board_Object.SetBoardOrder(BoardOrder);
        board_Object.SetBoardCoor(new Vector2Int(x, y));
        board_Object.transform.SetParent(boardBoxParent);
        Map_Holder.Instance.GameBoard[x, y] = new GameBoard(board_Object.LearnBoardType(), BoardOrder, board_Object.gameObject);

        // Bossu dışarı çıkar
        board_Object.transform.DOMoveY(0, 1.0f).OnComplete(() =>
        {
            board_Object.transform.DOMove(new Vector3(x, 0, y), 1.0f);
        });
    }
}