using UnityEngine;
using System.Collections;

public class Direction_Teleport : Moving_Base
{
    // Rastgele bir yere ışınlanır
    [SerializeField] private ParticleSystem teleportEnd;
    [SerializeField] private ParticleSystem teleportStart;

    private bool walkingTime;
    private Vector3Int teleportPos;
    private float randomDirectionTimeNext;

    public override void OnStart()
    {
        Transform teleportParent = Utils.MakeChieldForGameElement("Teleport_Parent");
        teleportStart.transform.SetParent(teleportParent);
        teleportEnd.transform.SetParent(teleportParent);
        MyBase.SetDirection(MyBase.LearnIntDirection(FindRandomDirection() - MyBase.LearnIntDirection(transform.position)));
        walkingTime = true;
    }
    private void DoTeleport()
    {
        StartCoroutine(TeleportStarted());
    }
    IEnumerator TeleportStarted()
    {
        // Teleport başlattransform.position
        teleportStart.Play();
        teleportStart.transform.position = transform.position;
        teleportEnd.transform.position = teleportPos;
        teleportEnd.Play();
        yield return new WaitForSeconds(1);
        // Transfer ol
        transform.position = teleportPos;
        yield return new WaitForSeconds(1);
        walkingTime = true;
        teleportStart.Stop();
        teleportEnd.Stop();
        yield return new WaitForSeconds(0.5f);
        MyBase.SetDirection(FindRandomDirection() - MyBase.LearnIntDirection(transform.position));
    }
    private void Update()
    {
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        if (Vector3.Distance(transform.position, Player.position) > 0.05f)
        {
            if (walkingTime)
            {
                randomDirectionTimeNext += Time.deltaTime;
                if (randomDirectionTimeNext > ChangeDirectionTime)
                {
                    walkingTime = false;
                    randomDirectionTimeNext = 0;
                    bool finded = false;
                    while (!finded)
                    {
                        int x = Random.Range(0, Map_Holder.Instance.GameBoard.GetLength(0));
                        int z = Random.Range(0, Map_Holder.Instance.GameBoard.GetLength(1));
                        if (x < 0 || z >= Map_Holder.Instance.GameBoard.GetLength(0))
                        {
                            continue;
                        }
                        if (x < 0 || z >= Map_Holder.Instance.GameBoard.GetLength(1))
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
                    DoTeleport();
                }
                if (Vector3.Distance(transform.position, MyDirections[RndDirec]) < 0.05f)
                {
                    MyBase.SetDirection(FindRandomDirection() - MyBase.LearnIntDirection(transform.position));
                }
                transform.Translate(MyBase.Direction * Time.deltaTime * MyBase.MySpeed);
            }
        }
    }
}