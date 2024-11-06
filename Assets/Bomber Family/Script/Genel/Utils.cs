using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public enum DebugType
{
    Log,
    Warning,
    Error
}
public static class Utils
{
    private static Transform gameElements;
    private static Camera myCamera;
    private static int groundLayerMask;

    public static void SetAllParents(Transform allParents)
    {
        gameElements = allParents;
    }
    public static void SetCamera()
    {
        myCamera = Camera.main;
    }
    public static int LearnLayerIndex()
    {
        return groundLayerMask;
    }
    public static void SetGroundLayerMaskIndex()
    {
        groundLayerMask = LayerMask.NameToLayer("Ground");
    }
    public static void WaitAndDo(float waitinTime, System.Action action)
    {
        // Bekle
        DOVirtual.DelayedCall(waitinTime,
            () =>
            {
                // Fonksiyonu aktif et.
                action?.Invoke();
            });
    }
    public static void SetParent(GameObject newObject, string objectName)
    {
        if (gameElements == null)
        {
            gameElements = new GameObject("Game Elements").transform;
        }
        var objectsParent = gameElements.Find(objectName);
        if (objectsParent == null)
        {
            objectsParent = new GameObject(objectName).transform;
            objectsParent.SetParent(gameElements);
        }
        newObject.transform.SetParent(objectsParent);
    }
    public static Transform MakeChieldForGameElement(string objectName)
    {
        if (gameElements == null)
        {
            gameElements = new GameObject("Game Elements").transform;
        }
        var newObject = gameElements.Find(objectName);
        if (newObject == null)
        {
            newObject = new GameObject(objectName).transform;
            newObject.SetParent(gameElements);
        }

        return newObject;
    }
    public static List<Button> FindAllButtons(Transform buttonParent, bool incLudeParent = false)
    {
        List<Button> allButtons = new List<Button>();

        List<Transform> list = new List<Transform>();
        list.Add(buttonParent);
        for (int e = 0; e < list.Count; e++)
        {
            if (list[e].TryGetComponent(out Button buton))
            {
                if (e == 0)
                {
                    if (incLudeParent)
                    {
                        allButtons.Add(buton);
                    }
                }
                else
                {
                    allButtons.Add(buton);
                }
            }
            for (int i = 0; i < list[e].childCount; i++)
            {
                list.Add(list[e].GetChild(i));
            }
        }
        return allButtons;
    }
    public static void Debug(string mesaj, DebugType debugType = DebugType.Log)
    {
        if (debugType == DebugType.Log)
        {
            UnityEngine.Debug.Log(mesaj);
        }
        if (debugType == DebugType.Warning)
        {
            UnityEngine.Debug.Log(mesaj);
        }
        if (debugType == DebugType.Error)
        {
            UnityEngine.Debug.Log(mesaj);
        }
    }
}