using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;

public class Object_Taker : Secret_Object
{
    [Header("Taker")]
    [SerializeField] private float movingTime;
    [SerializeField] private Vector3 sendingOffSet;
    [SerializeField] private List<Transform> movers = new List<Transform>();

    public void TakeMover(Transform object_Mover)
    {
        if (LevelCondition())
        {
            object_Mover.transform.DOMove(transform.position + sendingOffSet, movingTime).OnComplete(() =>
            {
                movers.Remove(object_Mover);
                if (movers.Count == 0)
                {
                    SetMissionComplete();
                }
            });
        }
    }
}