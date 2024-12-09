using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss : Enemy_Base
{
    [SerializeField] private List<EnemyBomb> enemyBombList = new List<EnemyBomb>();

    #region Use Special Bomb
    public void AddBomb(int bombAmount, BombType bombType)
    {
        enemyBombList.Add(new EnemyBomb(bombAmount, bombType));
    }
    public override void UseSpecialBomb(int order)
    {
        for (int e = 0; e < enemyBombList.Count; e++)
        {
            if (enemyBombList[e].bombType != (BombType)order)
            {
                continue;
            }
            if (enemyBombList[e].bombAmount > 0)
            {
                enemyBombList[e].bombAmount--;
                base.UseSpecialBomb(order);
                return;
            }
        }
    }
    #endregion
}