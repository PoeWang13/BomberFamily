using UnityEngine;

public class Direction_Random : Moving_Base
{
    // Rastgele dolaşır
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
        MyBase.SetDirection(FindRandomDirection());
    }
}