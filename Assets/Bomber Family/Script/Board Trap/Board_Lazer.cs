using UnityEngine;
using System.Collections.Generic;

public class Board_Lazer : Board_Object
{
    [SerializeField] private int damage;
    [SerializeField] private float activeTime = 5;
    [SerializeField] private List<GameObject> allLazerGameObject = new List<GameObject>();
    [SerializeField] private List<Collider> allLazerCollider = new List<Collider>();

    private bool isActive;
    private float activeTimeNext;

    private void Update()
    {
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        activeTimeNext += Time.deltaTime;
        if (isActive)
        {
            if (activeTimeNext > activeTime)
            {
                activeTimeNext = 0;
                isActive = false;
                allLazerCollider.ForEach(l => l.enabled = false);
                allLazerGameObject.ForEach(l => l.SetActive(false));
            }
        }
        else
        {
            if (activeTimeNext > activeTime)
            {
                activeTimeNext = 0;
                isActive = true;
                allLazerCollider.ForEach(l => l.enabled = true);
                allLazerGameObject.ForEach(l => l.SetActive(true));
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isActive)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            Game_Manager.Instance.AddCaughtTrapAmount();
            Game_Manager.Instance.AddLoseLifeAmount(damage);
            other.GetComponent<Character_Base>().TakeDamage(damage);
        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Character_Base>().TakeDamage(damage);
        }
    }
}