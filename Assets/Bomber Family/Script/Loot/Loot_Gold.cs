using UnityEngine;

public class Loot_Gold : PoolObje
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int earnGold = Random.Range(2, 20);
            Canvas_Manager.Instance.SetGoldSmooth(earnGold);
            Game_Manager.Instance.AddGoldAmount(earnGold);
            EnterHavuz();
        }
    }
}