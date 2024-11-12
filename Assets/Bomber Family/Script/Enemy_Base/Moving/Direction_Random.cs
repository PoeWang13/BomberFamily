using UnityEngine;

public class Direction_Random : Moving_Base
{
    // Rastgele dolaşır
    private float randomDirectionTimeNext;

    public override void OnStart()
    {
        MyBase.SetDirection(MyBase.LearnDirection(FindRandomDirection() - MyBase.LearnDirection(transform.position)));
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
        if (Vector3.Distance(transform.position, Player.position) > 0.05f)
        {
            randomDirectionTimeNext += Time.deltaTime;
            if (randomDirectionTimeNext > ChangeDirectionTime)
            {
                randomDirectionTimeNext = 0;
                MyBase.SetDirection(FindRandomDirection() - MyBase.LearnDirection(transform.position));
            }
            if (Vector3.Distance(transform.position, MyDirections[RndDirec]) < 0.05f)
            {
                MyBase.SetDirection(FindRandomDirection() - MyBase.LearnDirection(transform.position));
            }
            transform.Translate(MyBase.Direction * Time.deltaTime * MyBase.MySpeed);
        }
    }
}