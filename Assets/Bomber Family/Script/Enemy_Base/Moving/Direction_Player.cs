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
        if (Vector3.SqrMagnitude(transform.position + MyBase.Direction - Player.position) > 0.01f)
        {
            MyBase.SetIntPos();
            findPlayer = false;
            Node playerNode = LearnNode(Player.position);
            Node startNode = LearnNode(transform.position);
            findPath = Map_Holder.Instance.FindPath(startNode, playerNode);
            if (findPath.Count == 0)
            {
                MyBase.SetDirection(FindRandomDirection());
            }
            else
            {
                findPlayer = true;
                nextPoint = new Vector3(findPath[0].X, 0, findPath[0].Z);
                MyBase.SetDirection(MyBase.LearnDirection(nextPoint) - MyBase.LearnDirection(transform.position));
            }
        }
    }
    private Node LearnNode(Vector3 pos)
    {
        return new Node(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.z));
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
                tryingToFindPlayerTimeNext = 0;
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
                tryingToFindPlayerTimeNext += Time.deltaTime;
                if (tryingToFindPlayerTimeNext > ChangeDirectionTime)
                {
                    if (Vector3.SqrMagnitude(transform.position - MyBase.LearnIntDirection(transform.position)) < 0.01f)
                    {
                        TryToFindPlayer();
                        tryingToFindPlayerTimeNext = 0;
                    }
                }
            }
        }
    }
}