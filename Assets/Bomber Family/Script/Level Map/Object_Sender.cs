using DG.Tweening;
using UnityEngine;

public class Object_Sender : Secret_Object
{
    [Header("Sender")]
    [SerializeField] private Vector3 sendingPos;
    [SerializeField] private float sendingTime;

    public void SendingObject(Transform object_Send)
    {
        if (MissionComplete)
        {
            return;
        }
        object_Send.transform.DOMove(sendingPos, sendingTime).OnComplete(() =>
        {
            SetMissionComplete();
        });
    }
}