using UnityEngine;
using System.Collections;

public class Direction_Teleport : Moving_Base
{
    // Rastgele bir yere ışınlanır
    [SerializeField] private ParticleSystem teleportEnd;
    [SerializeField] private ParticleSystem teleportStart;
    [SerializeField] private float teleportTime;

    private bool walkingTime;
    private Vector3Int teleportPos;
    private float randomDirectionTimeNext;
    private float teleportTimeNext;

    public override void OnStart()
    {
        Transform teleportParent = Utils.MakeChieldForGameElement("Teleport_Parent");
        teleportStart.transform.SetParent(teleportParent);
        teleportEnd.transform.SetParent(teleportParent);
        MyBase.SetDirection(FindRandomDirection());
        walkingTime = true;
    }
    public override void Move()
    {
        if (walkingTime)
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
                    ChangeDirection();
                }
            }
            else
            {
                randomDirectionTimeNext += Time.deltaTime;
                if (randomDirectionTimeNext > ChangeDirectionTime)
                {
                    ChangeDirection();
                    return;
                }
                teleportTimeNext += Time.deltaTime;
                if (teleportTimeNext > teleportTime)
                {
                    walkingTime = false;
                    teleportTimeNext = 0;
                    bool finded = false;
                    while (!finded)
                    {
                        int x = Random.Range(0, Map_Holder.Instance.GameBoard.GetLength(0));
                        int z = Random.Range(0, Map_Holder.Instance.GameBoard.GetLength(1));
                        if (x < 0 || x >= Map_Holder.Instance.GameBoard.GetLength(0))
                        {
                            continue;
                        }
                        if (z < 0 || z >= Map_Holder.Instance.GameBoard.GetLength(1))
                        {
                            continue;
                        }
                        if (Map_Holder.Instance.GameBoard[x, z].board_Game.boardType == BoardType.Wall ||
                            Map_Holder.Instance.GameBoard[x, z].board_Game.boardType == BoardType.Trap ||
                            Map_Holder.Instance.GameBoard[x, z].board_Game.boardType == BoardType.Box)
                        {
                            continue;
                        }
                        finded = true;
                        teleportPos = new Vector3Int(x, 0, z);
                    }
                    // Teleport yap
                    StartCoroutine(TeleportStarted());
                }
            }
        }
    }
    private void ChangeDirection()
    {
        MyBase.SetIntPos();
        randomDirectionTimeNext = 0;
        MyBase.SetDirection(FindRandomDirection());
    }
    IEnumerator TeleportStarted()
    {
        // Teleport başlattransform.position
        teleportEnd.Play();
        teleportStart.Play();
        teleportEnd.transform.position = teleportPos;
        teleportStart.transform.position = transform.position;
        yield return new WaitForSeconds(0.5f);
        // Transfer ol
        transform.position = teleportPos;
        MyBase.SetIntPos();
        teleportEnd.Stop();
        teleportStart.Stop();
        MyBase.SetDirection(FindRandomDirection());
        walkingTime = true;
    }
}