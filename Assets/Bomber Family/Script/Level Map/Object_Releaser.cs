using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;

public class Object_Releaser : Secret_Object
{
    [SerializeField] private Transform object_ReleaserViewer;
    [SerializeField] private Vector3 newPosForCloser;
    [SerializeField] private List<Object_Mover> movers = new List<Object_Mover>();

    private void OnMouseUpAsButton()
    {
        if (MissionComplete)
        {
            return;
        }
        if (LevelCondition())
        {
            if (movers.Count == 0)
            {
                transform.DOMove(Vector3.down * 10, 2.5f);
                object_ReleaserViewer.transform.DOShakePosition(2, 50, 25, 180).OnComplete(() =>
                {
                    object_ReleaserViewer.DOMove(Vector3.zero, 1.5f);
                    SetMissionComplete();
                });
            }
        }
    }
}