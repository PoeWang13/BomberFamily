using DG.Tweening;
using UnityEngine;

public enum Direction
{
    X, Y, Z
}
public class Object_Turner : Secret_Object
{
    [SerializeField] private int angleMinDistance;
    [SerializeField] private int angleMaxDistance;
    [SerializeField] private Direction direction;
    [SerializeField] private Vector3 object_Angle;

    private bool canTurn = true;

    private void OnMouseUpAsButton()
    {
        if (MissionComplete)
        {
            return;
        }
        if (canTurn)
        {
            if (LevelCondition())
            {
                canTurn = false;
                transform.DORotate(transform.eulerAngles + object_Angle, 3.0f).OnComplete(() =>
                {
                    canTurn = true;
                    if (direction == Direction.X)
                    {
                        if (transform.eulerAngles.x >= angleMinDistance && transform.eulerAngles.x <= angleMaxDistance)
                        {
                            SetMissionComplete();
                        }
                    }
                    else if (direction == Direction.Y)
                    {
                        if (transform.eulerAngles.y >= angleMinDistance && transform.eulerAngles.y <= angleMaxDistance)
                        {
                            SetMissionComplete();
                        }
                    }
                    else if (direction == Direction.Z)
                    {
                        if (transform.eulerAngles.z >= angleMinDistance && transform.eulerAngles.z <= angleMaxDistance)
                        {
                            SetMissionComplete();
                        }
                    }
                });
            }
        }
    }
}