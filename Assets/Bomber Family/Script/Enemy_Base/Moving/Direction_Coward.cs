using UnityEngine;

public class Direction_Coward : Moving_Base
{
    private float randomDirectionTimeNext;
    public override void OnStart()
    {
        MyBase.SetDirection(FindRandomDirection());
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
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, MyBase.Direction, LearnDistance(), PlayerMaskIndex))
        {
            ChangeDirection();
        }
        else if(Physics.Raycast(ray, out raycast, 1, BoardMaskIndex))
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
                if (Vector3.SqrMagnitude(transform.position - MyBase.LearnIntDirection(transform.position)) < 0.01f)
                {
                    ChangeDirection();
                }
            }
        }
    }
    private void ChangeDirection()
    {
        MyBase.SetIntPos();
        randomDirectionTimeNext = 0;
        MyBase.SetDirection(FindRandomDirection(true));
    }
    private int LearnDistance()
    {
        if (Mathf.Abs(Mathf.RoundToInt(MyBase.Direction.x)) > 0)
        {
            return BoardWeight;
        }
        if (Mathf.Abs(Mathf.RoundToInt(MyBase.Direction.z)) > 0)
        {
            return BoardHeight;
        }
        return 0;
    }
}