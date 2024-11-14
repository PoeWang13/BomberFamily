using UnityEngine;
using System.Collections.Generic;

public class Enemy_Base : Character_Base
{
    [SerializeField] private Item_Enemy myItem;

    private List<Vector2Int> possibleLootPos = new List<Vector2Int>();

    public Item_Enemy MyItem { get { return myItem; } }

    public override void OnStart()
    {
        base.OnStart();
        SetMyLife(CharacterStat.myLife);
        SetMySpeed(CharacterStat.mySpeed);
        SetCharacterStat(myItem.MyStartingStat);
    }
    public override void IncreaseBombAmount(BombType bombType)
    {
        if (myItem.MyBombType == bombType)
        {
            IncreaseBomb();
        }
    }

    #region Taked Damage
    public override void Dead()
    {
        if (myItem.MyLootChance > Random.value * 100)
        {
            // Luck yüksek geldi. Loot alabilirsin.
            List<Pooler> allLoot = myItem.MyLootController.GetLootItem();
            int oldStart = 0;
            int oldFinish = 0;
            int newStart = -1;
            int newFinish = Mathf.Abs(newStart) + 1;
            // Potansiyel item koyma koordinatlarý
            PotansiyelKoordinatlar(oldStart, oldFinish, newStart, newFinish);

            if (allLoot.Count > 0)
            {
                Audio_Manager.Instance.PlayGameDrop();
            }
            while (allLoot.Count != 0)
            {
                while (possibleLootPos.Count != 0)
                {
                    int rndOrder = Random.Range(0, possibleLootPos.Count);
                    Vector2Int randomDirec = possibleLootPos[rndOrder];
                    possibleLootPos.RemoveAt(rndOrder);
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
                    // Potansiyel item koyma koordinatlarýný tekrar doldur
                    oldStart = newStart;
                    oldFinish = newFinish;
                    newStart = newStart - 1;
                    newFinish = newFinish + 1;
                    PotansiyelKoordinatlar(oldStart, oldFinish, newStart, newFinish);
                }
            }
        }
        Game_Manager.Instance.AddEnemyAmount();
        Game_Manager.Instance.AddExpAmount(myItem.LearnExp());
        
        EnterHavuz();
    }
    private void PotansiyelKoordinatlar(int oldStart, int oldFinish, int newStart, int newFinish)
    {
        possibleLootPos.Clear();

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
                        possibleLootPos.Add(new Vector2Int(x, y));
                    }
                }
                else
                {
                    possibleLootPos.Add(new Vector2Int(x, y));
                }
            }
        }
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopMovingForXTime();
            Game_Manager.Instance.AddLoseLifeAmount(CharacterStat.myBombPower);
            Character_Base character_Base = other.GetComponent<Character_Base>();
            character_Base.TakeDamage(CharacterStat.myBombPower);
        }
        else if (other.CompareTag("Enemy"))
        {
            Physics.IgnoreCollision(MyCollider, other.GetComponent<Board_Object>().MyCollider);
        }
    }
}