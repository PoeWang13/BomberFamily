using UnityEngine;
using System.Collections.Generic;

public class Enemy_Base : Character_Base
{
    [SerializeField] private Item_Enemy myItem;

    private int clockBombAmount;
    private int areaBombAmount;
    private int antiBombAmount;
    private int nucleerBombAmount;
    private int searcherBombAmount;
    private int elektroBombAmount;
    private int lavBombAmount;
    private int buzBombAmount;
    private int sisBombAmount;
    private int zehirBombAmount;
    private List<Vector2Int> possibleLootPos = new List<Vector2Int>();

    public Item_Enemy MyItem { get { return myItem; } }

    public override void OnStart()
    {
        base.OnStart();
        SetMyLife(CharacterStat.myLife);
        SetMySpeed(CharacterStat.mySpeed);
        SetCharacterStat(myItem.MyStartingStat);
    }
    public override void SetMouseButton()
    {
        Map_Creater_Manager.Instance.ChooseStuckObject(this);
        Canvas_Manager.Instance.OpenBaseSetting(true);
        Canvas_Manager.Instance.CloseSettingPanels();
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

    #region Use Special Bomb
    public override void UseAntiBomb()
    {
        if (antiBombAmount > 0)
        {
            antiBombAmount--;
            base.UseAntiBomb();
        }
    }
    public override void UseAreaBomb()
    {
        if (areaBombAmount > 0)
        {
            areaBombAmount--;
            base.UseAntiBomb();
        }
    }
    public override void UseClockBomb()
    {
        if (clockBombAmount > 0)
        {
            clockBombAmount--;
            base.UseClockBomb();
        }
    }
    public override void UseNucleerBomb()
    {
        if (nucleerBombAmount > 0)
        {
            nucleerBombAmount--;
            base.UseNucleerBomb();
        }
    }
    public override void UseSearcherBomb()
    {
        if (searcherBombAmount > 0)
        {
            searcherBombAmount--;
            base.UseSearcherBomb();
        }
    }
    public override void UseElektroBomb()
    {
        if (elektroBombAmount > 0)
        {
            elektroBombAmount--;
            base.UseElektroBomb();
        }
    }
    public override void UseLavBomb()
    {
        if (lavBombAmount > 0)
        {
            lavBombAmount--;
            base.UseLavBomb();
        }
    }
    public override void UseBuzBomb()
    {
        if (buzBombAmount > 0)
        {
            buzBombAmount--;
            base.UseBuzBomb();
        }
    }
    public override void UseSisBomb()
    {
        if (sisBombAmount > 0)
        {
            sisBombAmount--;
            base.UseSisBomb();
        }
    }
    public override void UseZehirBomb()
    {
        if (zehirBombAmount > 0)
        {
            zehirBombAmount--;
            base.UseZehirBomb();
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