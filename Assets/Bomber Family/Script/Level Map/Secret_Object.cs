using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Secret_Object : MonoBehaviour
{
    [SerializeField] private int levelLimit;
    [SerializeField] private bool missionComplete;
    [SerializeField] private UnityEvent unityEvent;
    [SerializeField] private List<Secret_Object> conditionObjects = new List<Secret_Object>();

    public bool MissionComplete { get { return missionComplete; } }

    public virtual void ObjectFixed()
    {
        missionComplete = true;
    }
    public void SetMissionComplete()
    {
        missionComplete = true;
        unityEvent?.Invoke();
    }
    public bool ObjectCondition()
    {
        for (int i = 0; i < conditionObjects.Count; i++)
        {
            if (!conditionObjects[i].MissionComplete)
            {
                return false;
            }
        }
        return true;
    }
    public bool LevelCondition()
    {
        if (levelLimit >= Save_Load_Manager.Instance.gameData.lastLevel)
        {
            return true;
        }
        return false;
    }
}