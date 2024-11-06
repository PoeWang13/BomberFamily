using UnityEngine;

public class Loot_Curse : PoolObje
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player_Base player_Base))
        {
            player_Base.GiveCurse();
            EnterHavuz();
        }
    }
}