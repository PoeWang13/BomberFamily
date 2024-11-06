using UnityEngine;

public class Loot_Speed : PoolObje
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player_Base player_Base))
        {
            player_Base.IncreaseSpeed();
            EnterHavuz();
        }
    }
}