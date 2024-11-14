using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class Trap_Home : Board_Object
{
    // Boarddaki herhangi bir enemyden üretir.
    [SerializeField] private float spawnTime = 5;
    [SerializeField] private List<Pooler> enemies = new List<Pooler>();

    private float spawnTimeNext;
    private PoolObje poolObje;
    private Transform boardEnemyParent;
    private List<BoardCoor> boardCoors = new List<BoardCoor>();

    private void Start()
    {
        boardEnemyParent = Utils.MakeChieldForGameElement("Board_Enemy");
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
        spawnTimeNext += Time.deltaTime;
        if (spawnTimeNext > spawnTime)
        {
            spawnTimeNext = 0;
            poolObje = enemies[Random.Range(0, enemies.Count)].HavuzdanObjeIste(transform.position + Vector3.down * 5);
            Enemy_Base enemy_Base = poolObje.GetComponent<Enemy_Base>();
            enemy_Base.GetComponent<Character_Base>().DebuffMySpeed(0.0f);
            enemy_Base.SetBoardCoor(new Vector2Int(MyCoor.x, MyCoor.y));
            SendBossOutSide(enemy_Base);
            enemy_Base.transform.SetParent(boardEnemyParent);
        }
    }
    private void SendBossOutSide(Enemy_Base board_Object)
    {
        int rndDir = Random.Range(0, boardCoors.Count);
        // Bossu dışarı çıkar
        board_Object.transform.DOMoveY(0, 1.0f).OnComplete(() =>
        {
            // Bossu aşağı yürüt
            int rndDirec = Random.Range(0, boardCoors.Count);
            board_Object.transform.DOMove(new Vector3(boardCoors[rndDirec].x, 0, boardCoors[rndDirec].y), 1.0f).OnComplete(() =>
            {
                // Bossu serbest bırak
                board_Object.SetMove(true);
                board_Object.GetComponent<Character_Base>().ResetSpeed();
            });
        });
    }
}