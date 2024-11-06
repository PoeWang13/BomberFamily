using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class LootData
{
    public int lootChance;
    public Pooler lootObject;
}
public class Loot_Controller : ScriptableObject
{
    [Header("Büyükten küçüğe sıralama yapmayı unutmayın.")]
    [SerializeField] private List<LootData> allLoot = new List<LootData>();

    public List<LootData> AllLoot {  get { return allLoot; } }

    [ContextMenu("Short Loot List")]
    private void ShortLootList()
    {
        allLoot.Sort(ShortLoot);
    }
    private int ShortLoot(LootData a, LootData b)
    {
        if (a.lootChance > b.lootChance)
        {
            return -1;
        }
        else if (b.lootChance > a.lootChance)
        {
            return 1;
        }
        return 0;
    }
}