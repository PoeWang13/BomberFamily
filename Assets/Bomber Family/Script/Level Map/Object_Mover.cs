using DG.Tweening;
using UnityEngine;

public class Object_Mover : Secret_Object
{
    [Header("Hanging")]
    [SerializeField] private Camera myCamera;
    [SerializeField] private Vector3 hangingOffSet;

    [Header("Moving")]
    [SerializeField] private bool isLocal;
    [SerializeField] private float movingTime;
    [SerializeField] private Vector3 movingPoint;
    [SerializeField] private Transform movingTransform;

    private bool isHandled;
    private Vector3 myOrjPos;
    private int layerMaskIndex;

    private void Start()
    {
        myOrjPos = transform.position;
        layerMaskIndex = 1 << LayerMask.NameToLayer("Secret");
    }
    private void OnMouseUpAsButton()
    {
        if (MissionComplete)
        {
            return;
        }
        if (LevelCondition())
        {
            isHandled = !isHandled;
            if (isHandled)
            {
                transform.position = myOrjPos + hangingOffSet;
            }
            else
            {
                transform.position = myOrjPos;
            }
        }
    }
    private void Update()
    {
        if (MissionComplete)
        {
            return;
        }
        if (isHandled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 500, layerMaskIndex))
                {
                    if (hit.transform == movingTransform)
                    {
                        isHandled = false;
                        if (isLocal)
                        {
                            transform.SetParent(movingTransform);
                            transform.DOLocalMove(movingPoint, movingTime).OnComplete(() =>
                            {
                                SetMissionComplete();
                            });
                        }
                        else
                        {
                            transform.DOMove(movingPoint, movingTime).OnComplete(() =>
                            {
                                SetMissionComplete();
                            });
                        }
                    }
                    else
                    {
                        isHandled = false;
                        transform.position = myOrjPos;
                    }
                }
            }
        }
    }
}