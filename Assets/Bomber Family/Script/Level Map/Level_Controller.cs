using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Level_Controller : MonoBehaviour
{
    public UnityEvent unityEvent;
    [SerializeField] private List<Secret_Object> secret_Objects = new List<Secret_Object>();

    public void ControlObjects()
    {
        bool everyMissionComplete = true;
        for (int i = 0; i < secret_Objects.Count && everyMissionComplete; i++)
        {
            everyMissionComplete = secret_Objects[i].MissionComplete;
        }
        if (everyMissionComplete)
        {
            unityEvent?.Invoke();
        }
    }
}