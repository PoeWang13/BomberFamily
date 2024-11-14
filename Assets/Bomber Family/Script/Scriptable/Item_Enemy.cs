using UnityEngine;

[CreateAssetMenu(menuName = "Item/Item Enemy")]
public class Item_Enemy : Item_Character
{
    [Header("Character Base")]
    [SerializeField] private CharacterStat myStartingStat;
    [SerializeField] private CharacterStat myUpgradeStat;


    [Header("Enemy Loot")]
    [SerializeField] private int lootChance = 5;
    [SerializeField] private Vector2Int myExp = new Vector2Int(2, 5);
    [SerializeField] private Loot_Controller_Multi loot_Controller;

    public CharacterStat MyStartingStat { get { return myStartingStat; } }
    public CharacterStat MyUpgradeStat { get { return myUpgradeStat; } }
    public int MyLootChance { get { return lootChance; } }
    public Loot_Controller_Multi MyLootController { get { return loot_Controller; } }

    public int LearnExp()
    {
        return Random.Range(myExp.x, myExp.y);
    }
}