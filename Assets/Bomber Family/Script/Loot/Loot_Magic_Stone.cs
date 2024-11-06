using UnityEngine;

public class Loot_Magic_Stone : PoolObje
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Board_Gate.Instance.FoundMagicStone(this);
        }
    }
}