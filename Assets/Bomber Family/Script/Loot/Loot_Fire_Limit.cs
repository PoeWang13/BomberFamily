using UnityEngine;

public class Loot_Fire_Limit : PoolObje
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player_Base player_Base))
        {
            player_Base.IncreaseBombFireLimit();
            EnterHavuz();
        }
    }
}