using UnityEngine;

public class Loot_Searcher : PoolObje
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player_Base player_Base))
        {
            player_Base.IncreaseSearcherBombAmount();
            EnterHavuz();
        }
    }
}