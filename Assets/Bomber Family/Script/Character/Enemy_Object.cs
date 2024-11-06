using System.Collections.Generic;
using UnityEngine;

public class Enemy_Object : Board_Object
{
    [SerializeField] private int life;
    [SerializeField] private int lootChance;
    [SerializeField] private Vector2Int myExp;
    [SerializeField] private Loot_Controller_Multi loot_Controller;
    public void TakeDamage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            if (lootChance > Random.value * 100)
            {
                // Luck yüksek geldi. Loot alabilirsin.
                List<Pooler> allLoot = loot_Controller.GetLootItem();
                int oldStart = 0;
                int oldFinish = 0;
                int newStart = -1;
                int newFinish = Mathf.Abs(newStart) + 1;
                // Potansiyel item koyma koordinatları
                (List<Vector2Int>, int oldStart, int oldFinish, int newStart, int newFinish) potCoor =
                        PotansiyelKoordinatlar(oldStart, oldFinish, newStart, newFinish);
                while (allLoot.Count != 0)
                {
                    while (potCoor.Item1.Count != 0)
                    {
                        int rndOrder = Random.Range(0, potCoor.Item1.Count);
                        Vector2Int randomDirec = potCoor.Item1[rndOrder];
                        potCoor.Item1.RemoveAt(rndOrder);
                        if (Map_Holder.Instance.GameBoard[randomDirec.x, randomDirec.y] == null)
                        {
                            PoolObje poolObje = allLoot[0].HavuzdanObjeIste(new Vector3Int(randomDirec.x, 0, randomDirec.y));
                            Map_Holder.Instance.LootObjects.Add(poolObje);
                            Game_Manager.Instance.AddLootObhjectList(poolObje);
                            allLoot.RemoveAt(0);
                        }
                    }
                    if (allLoot.Count != 0)
                    {
                        // Potansiyel item koyma koordinatlarını tekrar doldur
                        oldStart = newStart;
                        oldFinish = newFinish;
                        newStart = newStart - 1;
                        newFinish = newFinish + 1;
                        potCoor = PotansiyelKoordinatlar(oldStart, oldFinish, newStart, newFinish);
                    }
                }
            }
            Game_Manager.Instance.AddEnemyAmount();
            Game_Manager.Instance.AddExpAmount(Random.Range(myExp.x, myExp.y));
            EnterHavuz();
        }
    }
    private (List<Vector2Int>, int oldStart, int oldFinish, int newStart, int newFinish) PotansiyelKoordinatlar
        (int oldStart, int oldFinish, int newStart, int newFinish)
    {
        List<Vector2Int> coor = new List<Vector2Int>();

        for (int x = newStart; x < newFinish; x++)
        {
            for (int y = newStart; y < newFinish; y++)
            {
                //-3,4 + -2,3
                if (x >= oldStart && x < oldFinish)
                {
                    if (y >= oldStart && y < oldFinish)
                    {
                        continue;
                    }
                    else
                    {
                        coor.Add(new Vector2Int(x, y));
                    }
                }
                else
                {
                    coor.Add(new Vector2Int(x, y));
                }
            }
        }
        return (coor, oldStart, oldFinish, newStart, newFinish);
    }
}