using UnityEngine;
using System.Collections.Generic;

public class Direction_Player : Moving_Base
{
    // Player olduğu noktaya gitmeye çalışır. Bulamazsa rastgele dolaşır.
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
            Node playerNode = new Node(Mathf.RoundToInt(Player.position.x), Mathf.RoundToInt(Player.position.z));
            findPath = Map_Holder.Instance.FindPath(startNode, playerNode);
            if (findPath.Count == 0)
            {
                MyBase.SetDirection(FindRandomDirection() - MyBase.LearnDirection(transform.position));
            }
            else
            {
                findPlayer = true;
                nextPoint = new Vector3(findPath[0].X, 0, findPath[0].Z);
                MyBase.SetDirection(MyBase.LearnDirection(nextPoint) - MyBase.LearnDirection(transform.position));
            }
        }
    }
    private void Update()
    {
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        if (!MyBase.CanMove)
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
                MyBase.SetMySpeed(0);
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