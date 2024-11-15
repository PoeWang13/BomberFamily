using UnityEngine;
using System.Collections.Generic;

public class Trap_Poison : Board_Object
{
    [SerializeField] private int damage;
    [SerializeField] private float posionTime = 5;

    private float posionTimeNext;
    private List<Character_Base> allCharacterBase = new List<Character_Base>();

    public override void OnStart()
    {
        Physics.IgnoreCollision(MyCollider, Player_Base.Instance.MyCollider);
    }
    private void Update()
    {
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        if (allCharacterBase.Count > 0)
        {
            posionTimeNext += Time.deltaTime;
            if (posionTimeNext > posionTime)
            {
                posionTimeNext = 0;
                allCharacterBase.ForEach(x => x.TakeDamage(damage));
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Game_Manager.Instance.AddCaughtTrapAmount();
            Game_Manager.Instance.AddLoseLifeAmount(damage);
            Character_Base character_Base = other.GetComponent<Character_Base>();
            character_Base.TakeDamage(damage);
            allCharacterBase.Add(character_Base);
        }
        else if (other.CompareTag("Enemy"))
        {
            Character_Base character_Base = other.GetComponent<Character_Base>();
            character_Base.TakeDamage(damage);
            allCharacterBase.Add(character_Base);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Character_Base character_Base = other.GetComponent<Character_Base>();
            if (allCharacterBase.Contains(character_Base))
            {
                allCharacterBase.Remove(character_Base);
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            Character_Base character_Base = other.GetComponent<Character_Base>();
            if (allCharacterBase.Contains(character_Base))
            {
                allCharacterBase.Remove(character_Base);
            }
        }
        if (allCharacterBase.Count == 0)
        {
            posionTimeNext = 0;
        }
    }
}