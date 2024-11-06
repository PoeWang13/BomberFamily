using UnityEngine;

[CreateAssetMenu(menuName = "Loot/Single Loot Controller", fileName = "Single Loot Controller")]
public class Loot_Controller_Single : Loot_Controller
{
    public Pooler GetLootItem()
    {
        int allChance = 0;
        for (int e = 0; e < AllLoot.Count; e++)
        {
            allChance += AllLoot[e].lootChance;
        }
        int lootChance = Random.Range(0, allChance);
        for (int e = 0; e < AllLoot.Count; e++)
        {
            if (lootChance <= AllLoot[e].lootChance)
            {
                return AllLoot[e].lootObject;
            }
            else
            {
                lootChance -= AllLoot[e].lootChance;
            }
        }
        return null;
    }
}