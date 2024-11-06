using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Loot/Multi Loot Controller", fileName = "Multi Loot Controller")]
public class Loot_Controller_Multi : Loot_Controller
{
    public List<Pooler> GetLootItem()
    {
        List<Pooler> allLoot = new List<Pooler>();
        for (int e = 0; e < AllLoot.Count; e++)
        {
            if (Random.value * 100 > AllLoot[e].lootChance)
            {
                allLoot.Add(AllLoot[e].lootObject);
            }
        }
        return allLoot;
    }
}