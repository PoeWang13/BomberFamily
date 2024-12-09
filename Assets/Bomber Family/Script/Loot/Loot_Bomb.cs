using DG.Tweening;
using UnityEngine;

public class Loot_Bomb : Loot_Base
{
    [SerializeField] private BombType myBombType;

    public void AddBomb()
    {
        int power = Random.Range(1, 3);
        int limit = Random.Range(1, 3); ;
        float fireTime = Random.Range(0.5f, 3.0f);
        bool finded = false;
        for (int e = 0; e < Save_Load_Manager.Instance.gameData.allSpecialBomb.Count && !finded; e++)
        {
            if (Save_Load_Manager.Instance.gameData.allSpecialBomb[e].IsSameBomb(myBombType, power, limit, fireTime))
            {
                Save_Load_Manager.Instance.gameData.allSpecialBomb[e].bombAmount++;
                Canvas_Manager.Instance.SetBomb(e);
            }
        }
        if (!finded)
        {
            Canvas_Manager.Instance.AddBombPanel(myBombType, power, limit, fireTime);
        }
    }
}