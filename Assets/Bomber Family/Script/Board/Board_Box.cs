using UnityEngine;

public class Board_Box : Board_Object, IDamegable
{
    [SerializeField] private int life;
    [SerializeField] private int lootChance;
    [SerializeField] private Loot_Controller_Single loot_Controller;

    private int myLife;
    private bool hasMagicStone;

    public void SetHasMagicStone(bool magicStone)
    {
        hasMagicStone = magicStone;
    }
    public void TakeDamage(int damage)
    {
        myLife -= damage;
        if (myLife <= 0)
        {
            if (hasMagicStone)
            {
                // Anahtar düşülecek.
                Game_Manager.Instance.CreateMagicStone(transform.position);
            }
            else
            {
                if (lootChance > Random.value * 100)
                {
                    // Luck yüksek geldi. Loot alabilirsin.
                    Pooler loot = loot_Controller.GetLootItem();
                    if (loot != null)
                    {
                        PoolObje poolObje = loot.HavuzdanObjeIste(transform.position);
                        Map_Holder.Instance.LootObjects.Add(poolObje);
                        Game_Manager.Instance.AddLootObhjectList(poolObje);
                    }
                }
            }
            Game_Manager.Instance.AddBoxAmount();
            EnterHavuz();
        }
    }
    public override void ObjeHavuzEnter()
    {
        base.ObjeHavuzEnter();
        hasMagicStone = false;
        myLife = life;
    }
    public override void SetObject()
    {
        myLife = life;
    }
}