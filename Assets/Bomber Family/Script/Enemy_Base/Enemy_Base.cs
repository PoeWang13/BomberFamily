using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EnemyBomb
{
    public int bombAmount;
    public BombType bombType;

    public EnemyBomb(int bombAmount, BombType bombType)
    {
        this.bombAmount = bombAmount;
        this.bombType = bombType;
    }
}
public class Enemy_Base : Character_Base
{
    [SerializeField] private Item_Enemy myItem;
    [SerializeField] private List<EnemyBomb> enemyBombList = new List<EnemyBomb>();

    private int lootDeneme = 3;
    private List<Vector2Int> possibleLootPos = new List<Vector2Int>();

    public Item_Enemy MyItem { get { return myItem; } }

    public override void OnStart()
    {
        base.OnStart();
        SetMyLife(CharacterStat.myLife);
        SetMySpeed(CharacterStat.mySpeed);
        SetCharacterStat(myItem.MyStartingStat);
        lootDeneme = 3;
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
            while (allLoot.Count > 0)
            {
                while (possibleLootPos.Count > 0)
                {
                    int rndOrder = Random.Range(0, possibleLootPos.Count);
                    Vector2Int randomDirec = possibleLootPos[rndOrder] + MyCoor;
                    possibleLootPos.RemoveAt(rndOrder);

                    if (randomDirec.x < 0 || randomDirec.x >= Map_Holder.Instance.BoardSize.x)
                    {
                        continue;
                    }
                    if (randomDirec.y < 0 || randomDirec.y >= Map_Holder.Instance.BoardSize.y)
                    {
                        continue;
                    }
                    if (Map_Holder.Instance.GameBoard[randomDirec.x, MyCoor.y].board_Game.boardType == BoardType.Wall && Map_Holder.Instance.GameBoard[randomDirec.x, randomDirec.y].board_Object.activeSelf)
                    {
                        continue;
                    }
                    if (Map_Holder.Instance.GameBoard[randomDirec.x, randomDirec.y].board_Game.boardType == BoardType.Box && Map_Holder.Instance.GameBoard[randomDirec.x, randomDirec.y].board_Object.activeSelf)
                    {
                        continue;
                    }
                    if (Map_Holder.Instance.GameBoard[randomDirec.x, randomDirec.y].board_Game.boardType == BoardType.Trap)
                    {
                        continue;
                    }
                    if (Map_Holder.Instance.GameBoard[randomDirec.x, randomDirec.y].board_Game.boardType == BoardType.Gate)
                    {
                        continue;
                    }
                    if (Map_Holder.Instance.GameBoard[randomDirec.x, randomDirec.y].board_Game.boardType == BoardType.Bomb && Map_Holder.Instance.GameBoard[randomDirec.x, randomDirec.y].board_Object.activeSelf)
                    {
                        continue;
                    }
                    PoolObje poolObje = allLoot[0].HavuzdanObjeIste(new Vector3Int(randomDirec.x, 0, randomDirec.y));
                    Map_Holder.Instance.LootObjects.Add(poolObje);
                    Game_Manager.Instance.AddLootObhjectList(poolObje);
                    allLoot.RemoveAt(0);
                }
                if (lootDeneme > 0)
                {
                    if (allLoot.Count > 0)
                    {
                        // Potansiyel item koyma koordinatlarýný tekrar doldur
                        oldStart = newStart;
                        oldFinish = newFinish;
                        newStart = newStart - 1;
                        newFinish = newFinish + 1;
                        PotansiyelKoordinatlar(oldStart, oldFinish, newStart, newFinish);
                    }
                }
                else
                {
                    allLoot.Clear();
                }
            }
        }
        Game_Manager.Instance.AddEnemyAmount();
        Game_Manager.Instance.AddExpAmount(myItem.LearnExp());
        lootDeneme = 3;
        EnterHavuz();
    }
    private void PotansiyelKoordinatlar(int oldStart, int oldFinish, int newStart, int newFinish)
    {
        lootDeneme--;
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
    public void AddBomb(int bombAmount, BombType bombType)
    {
        enemyBombList.Add(new EnemyBomb(bombAmount, bombType));
    }
    public override void UseSpecialBomb(BombType bombType)
    {
        for (int e = 0; e < enemyBombList.Count; e++)
        {
            if (enemyBombList[e].bombType != bombType)
            {
                continue;
            }
            if (enemyBombList[e].bombAmount > 0)
            {
                enemyBombList[e].bombAmount--;
                base.UseSpecialBomb(bombType);
                return;
            }
        }
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopMovingForXTime();
            Character_Base character_Base = other.GetComponent<Character_Base>();
            character_Base.TakeDamage(CharacterStat.myBombPower);
        }
        else if (other.CompareTag("Enemy"))
        {
            Physics.IgnoreCollision(MyNotTriggeredCollider, other.GetComponent<Board_Object>().MyNotTriggeredCollider);
        }
    }
}