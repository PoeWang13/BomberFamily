using UnityEngine;

public class Loot_Bomb_Area : PoolObje
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player_Base player_Base))
        {
            player_Base.IncreaseAreaBombAmount();
            EnterHavuz();
        }
    }
}