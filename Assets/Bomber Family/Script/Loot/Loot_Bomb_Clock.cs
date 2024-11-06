using UnityEngine;

public class Loot_Bomb_Clock : PoolObje
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player_Base player_Base))
        {
            player_Base.IncreaseClockBombAmount();
            EnterHavuz();
        }
    }
}