using UnityEngine;

public class Direction_Random : Moving_Base
{
    // Rastgele dolaşır
    private float randomDirectionTimeNext;

    public override void OnStart()
    {
        MyBase.SetDirection(MyBase.LearnIntDirection(FindRandomDirection() - MyBase.LearnIntDirection(transform.position)));
    }
    private void Update()
    {
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        if (Vector3.Distance(transform.position, Player.position) > 0.05f)
        {
            randomDirectionTimeNext += Time.deltaTime;
            if (randomDirectionTimeNext > ChangeDirectionTime)
            {
                randomDirectionTimeNext = 0;
                MyBase.SetDirection(FindRandomDirection() - MyBase.LearnIntDirection(transform.position));
            }
            if (Vector3.Distance(transform.position, MyDirections[RndDirec]) < 0.05f)
            {
                MyBase.SetDirection(FindRandomDirection() - MyBase.LearnIntDirection(transform.position));
            }
            transform.Translate(MyBase.Direction * Time.deltaTime * MyBase.MySpeed);
        }
    }
}