using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;

public class Object_2_Clicker : Secret_Object
{
    [SerializeField] private Camera myCamera;
    [SerializeField] private List<Transform> clicker = new List<Transform>();

    private bool isHandled;
    private bool canClick;
    private int layerMaskIndex;
    private Vector3 myOrjPos;
    private List<Transform> clicked = new List<Transform>();

    private void Start()
    {
        myOrjPos = transform.position;
        layerMaskIndex = 1 << LayerMask.NameToLayer("Secret");
    }
    private void OnMouseUpAsButton()
    {
        if (LevelCondition())
        {
            isHandled = !isHandled;
            if (isHandled)
            {
                canClick = true;
                transform.position = myOrjPos + Vector3.up * 5;
            }
            else
            {
                transform.position = myOrjPos;
            }
        }
    }
    private void Update()
    {
        if (isHandled && canClick)
        {
            Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 500, layerMaskIndex))
            {
                if (hit.transform == clicker[clicked.Count])
                {
                    canClick = false;
                    clicked.Add(hit.transform);
                    transform.DOMove(hit.transform.position, 1.0f).OnComplete(() =>
                    {
                        if (clicker.Count == clicked.Count)
                        {
                            SetMissionComplete();
                        }
                        else
                        {
                            transform.DOMove(myOrjPos + Vector3.up * 5, 1.0f).OnComplete(() =>
                            {
                                canClick = true;
                            });
                        }
                    });
                }
                else
                {
                    clicked.Clear();
                    canClick = true;
                    isHandled = false;
                    transform.position = myOrjPos;
                }
            }
        }
    }
}